using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;


public class Sound : MonoBehaviour {

    public static Sound instance { get; private set; }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //ONLY EVER ADD TO THE END OF THESE BECAUSE ENUMS :(
    public enum SFXIDS { Click, Boot, Text, Password, Message, StartProcess, Shutdown, Scanning, Warning, Hover, Work, TextGlitch, TextNoise, Bleep }

    public enum SFXLISTS { Keyboards, Glitches }
    
    public enum AMBIENCES { Computer, Room, Inn, Pond, Village, DeadVillage, Wind, Drone }


    public enum UNIQUESOURCES { Text }


    public enum MasterMixerVars { masterVolume, inWorldVolume, computerVolume }
    public Dictionary<MasterMixerVars, float> masterMixerDefaultValues = new Dictionary<MasterMixerVars, float>()
    {
        { MasterMixerVars.masterVolume, 0 },
        { MasterMixerVars.inWorldVolume, 0 },
        { MasterMixerVars.computerVolume, 0 },
    };

    //FOR SERIALIZATION
    public List<SFXInfo> serialSFX = new List<SFXInfo>(0);
    public List<SFXListInfo> serialSFXList = new List<SFXListInfo>(0);
    public List<AmbienceInfo> serialAmbience = new List<AmbienceInfo>(0);

    [SerializeField] AudioMixer masterMixer;
    [SerializeField] AudioMixerGroup masterMixerGroup;
    [SerializeField] AudioMixerGroup glitchMixer;

    public Dictionary<SFXIDS, SFXInfo> sfx = new Dictionary<SFXIDS, SFXInfo>();
    public Dictionary<SFXLISTS, SFXListInfo> sfxLists = new Dictionary<SFXLISTS, SFXListInfo>();
    public Dictionary<AMBIENCES, AmbienceInfo> ambiences = new Dictionary<AMBIENCES, AmbienceInfo>();

    public Dictionary<SFXInfo, AudioSource> sfxPlayed = new Dictionary<SFXInfo, AudioSource>(); //i dunno. this needs to be remved from again, dunno how to do that.


    public List<AudioSource> glitchSounds = new List<AudioSource>(3);

    float masterVolume = 0f;

    [SerializeField] Transform sources;

    [SerializeField] int audiosourcePoolAmount = 60;

    int poolidx = 0;
    public List<AudioSource> audiosources = new List<AudioSource>();
    public Dictionary<UNIQUESOURCES, AudioSource> sacredsources = new Dictionary<UNIQUESOURCES, AudioSource>();

    // Use this for initialization
    void Start () {

        foreach(SFXInfo s in serialSFX)
        {
            if (sfx.ContainsKey(s.ID))
            {
                Debug.LogError("SFX DICTIONARY ALREADY CONTAINS KEY "+ s.ID.ToString() + " CHECK SOUNDMANAGER. Sound: " + s.audio.ToString());
            }
            sfx.Add(s.ID, s);
        }

        foreach(SFXListInfo s in serialSFXList)
        {
            if (sfxLists.ContainsKey(s.ID))
            {
                Debug.LogError("SFXLIST DICTIONARY ALREADY CONTAINS KEY " + s.ID.ToString() + " CHECK SOUNDMANAGER.");
            }

            sfxLists.Add(s.ID, s);
        }

        foreach (AmbienceInfo s in serialAmbience)
        {
            if (ambiences.ContainsKey(s.ID))
            {
                Debug.LogError("AMBIENCE DICTIONARY ALREADY CONTAINS KEY " + s.ID.ToString() + " CHECK SOUNDMANAGER. Sound: " + s.audio.ToString());
            }

            ambiences.Add(s.ID, s);
        }


        for (int i = 0; i < audiosourcePoolAmount; i++)
        {
            AddNewAudiosourceToPool();
        }

        foreach(KeyValuePair<MasterMixerVars,float> kvp in masterMixerDefaultValues)
        {
            float val = 0;
            masterMixer.GetFloat(kvp.Key.ToString(), out val);
            masterMixerDefaultValues[kvp.Key] = val;
        }

    }
	


    public void Play(string sound)
    {
        try
        {
            SFXIDS sfx = (SFXIDS)System.Enum.Parse(typeof(SFXIDS), sound);

            Play(sfx);
        }
        catch
        {
            Debug.Log("sound not found. Did you mispell? " + sound);
        }
    }

    public void PlayAmbient(string sound)
    {
        try
        {
            AMBIENCES sfx = (AMBIENCES)System.Enum.Parse(typeof(AMBIENCES), sound);

            PlayAmbient(sfx);
        }
        catch
        {
            Debug.Log("sound not found. Did you mispell? " + sound);
        }
    }

    void Play(SFXInfo sound)
    {
        if (audiosources[poolidx].isPlaying)
        {
            FindNextUnusedSource();
        }

        SetAudioSourceToInfoSettings(audiosources[poolidx], sound);
        audiosources[poolidx].Play();
    }

    public void Play(SFXIDS sound) //play directly
    {
        Play(sfx[sound]);
    }

    /// <summary>
    /// NOT IMPLEMENTED!
    /// </summary>
    /// <param name="sound"></param>
    /// <param name="pitch"></param>
    public void Play(SFXIDS sound, float pitch) //play with a pitch ???
    {

    }

    public void Play(SFXIDS sound, bool randomPitch) //This shouldn't be a separate play!!! That's Bad!
    {
        SFXInfo s = sfx[sound];
        if (audiosources[poolidx].isPlaying)
        {
            FindNextUnusedSource();
        }

        SetAudioSourceToInfoSettings(audiosources[poolidx], s);
        if (randomPitch)
        {
            audiosources[poolidx].pitch = Random.Range(0.98f, 1.02f);
        }

        audiosources[poolidx].Play();
    }

    /// <summary>
    /// Playing Sacred means that it plays without switching which audio source plays the file. This is good for sound effects that play a lot and never at the same time. For example, text beeping.
    /// </summary>
    /// <param name="sound"></param>
    /// <param name="uniqueID"></param>
    /// <param name="randomPitch"></param>
    public void PlaySacred(SFXIDS sound, UNIQUESOURCES uniqueID, bool randomPitch = false)
    {
        AudioSource source;
        SFXInfo s = sfx[sound];

        if (!sacredsources.ContainsKey(uniqueID))
        {
            //add it
            source = AddNewAudiosourceToPool();
            sacredsources.Add(uniqueID, source);
        }


        SetAudioSourceToInfoSettings(sacredsources[uniqueID], s);

        if (randomPitch)
        {
            sacredsources[uniqueID].pitch = Random.Range(0.98f, 1.02f);
        }

        sacredsources[uniqueID].Play();
    }

    /// <summary>
    /// NOT IMPLEMENTED!
    /// </summary>
    /// <param name="sound"></param>
    /// <param name="delay"></param>
    public void PlayDelayed(string sound, float delay)
    {

    }

    public void Stop(SFXIDS id)
    {
        
    }




    public void PlayRandomFromList(SFXLISTS list)
    {
        SFXInfo s = sfxLists[list].list[Random.Range(0, sfxLists[list].list.Count)];
        Play(s);
    }

    public void PlayAmbient(AMBIENCES sound)
    {
        AmbienceInfo s = ambiences[sound];

        if (audiosources[poolidx].isPlaying)
        {
            FindNextUnusedSource();
        }

        SetAudioSourceToInfoSettings(audiosources[poolidx], s);
        audiosources[poolidx].Play();

    }

    public void StopAmbient(AMBIENCES sound)
    {
        AmbienceInfo s = ambiences[sound];
        for (int i = 0; i < audiosources.Count; i++)
        {
            if(audiosources[i].clip == s.audio)
            {
                audiosources[i].Stop();
            }
        }
    }



    public void MuteMixer(MasterMixerVars mixer)
    {
        masterMixer.SetFloat(mixer.ToString(), -80);

    }

    public void UnMuteMixer(MasterMixerVars mixer)
    {
        masterMixer.SetFloat(mixer.ToString(), masterMixerDefaultValues[mixer]);
    }



    public void PlayGlitched(SFXIDS sound)  //the idea being I can play any sound glitched -- but dunno how it'll work yet.
    {
        //int soundChooser = 0;
        //switch (c)
        //{
        //    case 0:
        //        soundChooser = Random.Range(0, noiseSounds.Length + 1);
        //        if (soundChooser == 2)
        //        {
        //            soundChooser += Random.Range(-2, 1);
        //        }
        //        mixer.SetFloat("drymix", Random.Range(0, 1f));
        //        mixer.SetFloat("wetmix", Random.Range(0, 1f));
        //        mixer.SetFloat("rate", Random.Range(0, 20));
        //        mixer.SetFloat("lowpassCut", 22000f);
        //        if (soundChooser < noiseSounds.Length)
        //        {
        //            noiseSounds[soundChooser].Play();
        //        }
        //        break;
        //    case 1:
        //        soundChooser = Random.Range(0, noiseSounds.Length + 1);
        //        if (soundChooser == 2)
        //        {
        //            soundChooser += Random.Range(-1, 1);
        //        }
        //        mixer.SetFloat("lowpassCut", Random.Range(100f, 20000f));
        //        if (soundChooser < noiseSounds.Length)
        //        {
        //            noiseSounds[soundChooser].Play();
        //        }
        //        break;
        //    case 2:
        //        mixer.SetFloat("drymix", Random.Range(0, 1f));
        //        mixer.SetFloat("wetmix", Random.Range(0, 1f));
        //        mixer.SetFloat("rate", Random.Range(0, 20));
        //        mixer.SetFloat("lowpassCut", 22000f);
        //        noiseSounds[2].Play();
        //        break;
        //}
        //if (soundChooser == noiseSounds.Length)
        //{
        //    foreach (AudioSource a in noiseSounds)
        //    {
        //        a.Stop();
        //    }
        //}
        //SoundManager.instance.master.SetFloat("volume", -80);
        
    }

    public void PlayGlitch(int kind)
    {
        int soundChooser = 0;
        switch (kind)
        {
            case 0:
                soundChooser = Random.Range(0, glitchSounds.Count + 1);
                if (soundChooser == 2)
                {
                    soundChooser += Random.Range(-2, 1);
                }
                glitchMixer.audioMixer.SetFloat("drymix", Random.Range(0, 1f));
                glitchMixer.audioMixer.SetFloat("wetmix", Random.Range(0, 1f));
                glitchMixer.audioMixer.SetFloat("rate", Random.Range(0, 20));
                glitchMixer.audioMixer.SetFloat("cutoff", 22000f);
                if (soundChooser < glitchSounds.Count)
                {
                    glitchSounds[soundChooser].Play();
                }
                break;
            case 1:
                soundChooser = Random.Range(0, glitchSounds.Count + 1);
                if (soundChooser == 2)
                {
                    soundChooser += Random.Range(-1, 1);
                }
                glitchMixer.audioMixer.SetFloat("cutoff", Random.Range(100f, 20000f));
                if (soundChooser < glitchSounds.Count)
                {
                    glitchSounds[soundChooser].Play();
                }
                break;
            case 2:
                glitchMixer.audioMixer.SetFloat("drymix", Random.Range(0, 1f));
                glitchMixer.audioMixer.SetFloat("wetmix", Random.Range(0, 1f));
                glitchMixer.audioMixer.SetFloat("rate", Random.Range(0, 20));
                glitchMixer.audioMixer.SetFloat("cutoff", 22000f);
                glitchSounds[2].Play();
                break;
        }
        if (soundChooser == glitchSounds.Count)
        {
            foreach (AudioSource a in glitchSounds)
            {
                a.Stop();
            }
        }
        masterMixer.SetFloat("mastervolume", -80);

    }

    public void EndGlitch()
    {
        foreach (AudioSource a in glitchSounds)
        {
            a.Stop();
        }
        masterMixer.SetFloat("mastervolume", masterVolume);
    }


    /// <summary>
    /// Kinda don't use this.
    /// </summary>
    /// <param name="vol"></param>
    public void SetMasterVolume(float vol)
    {
        masterMixer.SetFloat("mastervolume", vol);

    }





    void SetAudioSourceToInfoSettings(AudioSource source, SFXInfo info)
    {
        source.clip = info.audio;
        source.volume = info.volume;
        source.loop = info.loop;
        source.pitch = 1;
        if(info.mixer != null)
        {
            source.outputAudioMixerGroup = info.mixer;
        }
    }

    void SetAudioSourceToInfoSettings(AudioSource source, AmbienceInfo info)
    {
        source.clip = info.audio;
        source.volume = info.volume;
        source.loop = info.loop;
        if (info.mixer != null)
        {
            source.outputAudioMixerGroup = info.mixer;
        }
    }


    void FindNextUnusedSource()
    {
        int tester = audiosourcePoolAmount;
        while (audiosources[poolidx].isPlaying)
        {
            IncrementPool();

            tester--;
            if (tester <= 0)
            {
                AddNewAudiosourceToPool();
                audiosourcePoolAmount++;
                poolidx = audiosourcePoolAmount - 1;
            }
        }
    }

    void IncrementPool()
    {
        poolidx++;
        if(poolidx >= audiosources.Count)
        {
            poolidx = 0;
        }
    }

    AudioSource AddNewAudiosourceToPool()
    {

        AudioSource a = sources.gameObject.AddComponent<AudioSource>();
        a.outputAudioMixerGroup = masterMixerGroup;
        a.spatialBlend = 0f;
        a.playOnAwake = false;
        a.loop = false;
        audiosources.Add(a);
        return a;
    }


}


[System.Serializable]
public class SFXInfo
{
    public Sound.SFXIDS ID;

    public AudioClip audio;

    [Range(0,1)]
    public float volume;
    public bool loop = false;
    public AudioMixerGroup mixer;
}

[System.Serializable]
public class SFXListInfo
{
    public Sound.SFXLISTS ID;
    public AudioMixer topmixer;

    public List<SFXInfo> list = new List<SFXInfo>();
}

[System.Serializable]
public class AmbienceInfo
{
    public Sound.AMBIENCES ID;

    public AudioClip audio;

    [Range(0, 1)]
    public float volume;
    public bool loop = false;
    public AudioMixerGroup mixer;

}