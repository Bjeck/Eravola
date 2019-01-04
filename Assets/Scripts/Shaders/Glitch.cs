using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PostProcessing;
using Kino;

public class Glitch : MonoBehaviour {

    public enum TimingNames { Standard, Mystery, Tension, Cursed, Dangerous, Bad, Terrible, Insane }

    public static Glitch instance { get; private set; }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public GlitchEffect glEf;
    public CRTShaderScript crt;
    public VHSPostProcessEffect vhsEf;
    public AnalogGlitch analogGlitch;
    public DigitalGlitch digitalGlitch;

    public List<GlitchText> texts = new List<GlitchText>();

    public Dictionary<TimingNames, GlitchTiming> timings = new Dictionary<TimingNames, GlitchTiming>();

    public GlitchTiming curTiming;

    [SerializeField] float timeToGlitch = 0f;
    float glitchSustain = 0f;
    float glitchIntensity = 1f;


    // Use this for initialization
    void Start () {

        SetupTimings();

        texts.AddRange(GameObject.FindObjectsOfType<GlitchText>());

        curTiming = timings[TimingNames.Standard]; //dEbug for now

        timeToGlitch = UnityEngine.Random.Range(curTiming.timeToMin, curTiming.timeToMax);

    }

    // Update is called once per frame
    void Update () {
        if (timeToGlitch > 0)
        {
            timeToGlitch -= Time.deltaTime;
        }
        else
        {
            StartCoroutine(GlitchScreen());
        }
    }

    //Regular, timed glitch, that always just happens regardless of what else is going on. Timings dependent on Timing ofc.
    IEnumerator GlitchScreen()
    {
        glEf.enabled = true;
        timeToGlitch = UnityEngine.Random.Range(curTiming.timeToMin, curTiming.timeToMax);
        glitchSustain = UnityEngine.Random.Range(curTiming.sustainMin, curTiming.sustainMax);
        glEf.intensity = glitchIntensity * Random.Range(0.5f, 1.5f);
        analogGlitch.intensity = glitchIntensity;

        analogGlitch.enabled = true;
        analogGlitch.scanLineJitter = Random.Range(0f, 0.3f);
        analogGlitch.verticalJump = 0f;
        analogGlitch.colorDrift = Random.Range(0f, 0.8f);

        while (glitchSustain > 0)
        {
            glitchSustain -= Time.deltaTime;
            yield return null;
        }   
        glEf.intensity = 0;
        glEf.enabled = false;
        analogGlitch.enabled = false;
        Sound.instance.EndGlitch();

        yield return 0;
    }



    public void GlitchScreenOnCommand(float t, bool withVHS = false)
    {
        StartCoroutine(GlitchScreenOC(t, -1, withVHS));
    }

    public void GlitchScreenOnCommand(float time, float inten = -1f, bool withVHS = false)
    {
        StartCoroutine(GlitchScreenOC(time, inten, withVHS));
    }
    
    public void GltichScreenOnCommand(float time)
    {
        StartCoroutine(GlitchScreenOC(time, -1, false));
    }

    public void GlitchScreenOCBoot(float time, float inten = -1f)
    {
        analogGlitch.verticalJump = Random.Range(0.4f, 1f);
        StartCoroutine(GlitchScreenOC(time, -1, false));
    }

    IEnumerator GlitchScreenOC(float time, float inten = -1f, bool withVHS = false)
    {
        if (inten == -1f)
        {
            inten = glitchIntensity;
        }

        glEf.enabled = true;
        glEf.intensity = inten * Random.Range(0.8f, 1.2f);
        analogGlitch.intensity = glitchIntensity;

        analogGlitch.enabled = true;
        analogGlitch.scanLineJitter = Random.Range(0f, 0.6f);
        analogGlitch.colorDrift = Random.Range(0f, 0.8f);

        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return 0;
        }
        glEf.intensity = 0;
        analogGlitch.intensity = 0;
        glEf.enabled = false;
        analogGlitch.enabled = false;
        Sound.instance.EndGlitch();
        yield return 0;
    }




    public void DisableAllEffects()
    {
        // glEf.enabled = false;
        crt.enabled = false;
        vhsEf.enabled = false;
    }

    public void EnableAllEffects()
    {
        // glEf.enabled = true;
//        Sound.instance.vo
        crt.enabled = true;
        vhsEf.enabled = true;
    }

    public void EnableDroneEffects()
    {
        vhsEf.enabled = true;
    }

    public void DisableDroneEffects()
    {
        vhsEf.enabled = false;
    }


    public void ChangeTiming(string timingName)
    {
        try
        {
            TimingNames nam = (TimingNames) System.Enum.Parse(typeof(TimingNames), timingName);
            ChangeTiming(nam);
        }
        catch
        {
            Debug.LogWarning("Couldn't parse name: " + timingName + ". Did you mispell? Should be in the TimingNames enum.");
        }
    }

    public void ChangeTiming(TimingNames timingName)
    {
        if (timings.ContainsKey(timingName))
        {
            curTiming = timings[timingName];
        }
    }


    void SetupTimings()
    {
        GlitchTiming t = new GlitchTiming();
        t.timeToMin = 20f;
        t.timeToMax = 70f;
        t.sustainMin = 0.1f;
        t.sustainMax = 0.4f;

        t.textTimeMin = 2f;
        t.textTimeMax = 30f;
        t.textSustainMin = 0.03f;
        t.textSustainMax = 0.08f;
        t.name = TimingNames.Standard;
        timings.Add(Glitch.TimingNames.Standard, t);

        t = new GlitchTiming();

        t.timeToMin = 1f;
        t.timeToMax = 20f;
        t.sustainMin = 0.1f;
        t.sustainMax = 0.4f;

        t.textTimeMin = 2f;
        t.textTimeMax = 3f;
        t.textSustainMin = 0.13f;
        t.textSustainMax = 0.28f;
        t.name = TimingNames.Mystery;
        timings.Add(Glitch.TimingNames.Mystery, t); //THIS IS NOT DONE YET!


    }

}



[System.Serializable]
public class GlitchTiming
{
    public Glitch.TimingNames name;
    public float timeToMin = 1f;
    public float timeToMax = 5f;
    public float sustainMin = 0.03f;
    public float sustainMax = 0.05f;

    public float textTimeMin = 1;
    public float textTimeMax = 20;
    public float textSustainMin = 0.1f;
    public float textSustainMax = 0.2f;

}

