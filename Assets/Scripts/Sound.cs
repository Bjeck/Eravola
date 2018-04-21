using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

///stuff like click sounds and keyboard sounds -- let's deal with them separately. the clicks themselves should take care of that. don't really mess with them here - or at least, leave them outside the general system.


//keyboards

public class Sound : MonoBehaviour {

    public enum SFXIDS { Click, Boot, Text, Password, Message, StartProcess, Shutdown, Scanning, Warning, Hover, Work, TextGlitch, TextNoise }

    public enum SFXLISTS { Keyboards, Glitches }

    //glitch sounds

    // ambiences

    public enum AMBIENCES { Inn, Pond, Village, DeadVillage, Wind }

    public List<SFXInfo> serialSFX = new List<SFXInfo>(0);
    public List<SFXListInfo> serialSFXList = new List<SFXListInfo>(0);
    public List<AmbienceInfo> serialAmbience = new List<AmbienceInfo>(0);


    public Dictionary<SFXIDS, AudioSource> sfx = new Dictionary<SFXIDS, AudioSource>();
    public Dictionary<SFXLISTS, AudioSource> sfxLists = new Dictionary<SFXLISTS, AudioSource>();
    public Dictionary<AMBIENCES, AudioSource> ambiences = new Dictionary<AMBIENCES, AudioSource>();


    public float masterVolume = 1f;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}




//SINGLE FIRE SOUNDS

    public void Play(string sound) //play directly
    {

    }

    public void Play(string sound, float pitch) //play with a pitch ???
    {

    }

    public void Play(string sound, bool randomPitch)
    {

    }

    public void PlayRandomFromList(string list)
    {

    }

    public void PlayDelayed(string sound, float delay)
    {

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
}

[System.Serializable]
public class SFXListInfo
{
    public Sound.SFXLISTS ID;

    public List<AudioClip> audio;

    [Range(0, 1)]
    public float volume;
    public bool loop = false;
}

[System.Serializable]
public class AmbienceInfo
{
    public Sound.AMBIENCES ID;

    public AudioClip audio;

    [Range(0, 1)]
    public float volume;
    public bool loop = false;
}