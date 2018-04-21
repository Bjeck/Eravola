using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Glitch : MonoBehaviour {

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

    public Dictionary<string, GlitchTiming> timings = new Dictionary<string, GlitchTiming>();

    GlitchTiming curTiming;

    [SerializeField] float timeToGlitch = 0f;
    float glitchSustain = 0f;
    float glitchIntensity = 1f;


    // Use this for initialization
    void Start () {

        SetupTimings();

        curTiming = timings["default"]; //dEbug for now

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

        //PlayGlitchSound ();
        print("begin glitch");
        while (glitchSustain > 0)
        {
            glitchSustain -= Time.deltaTime;
            yield return null;
        }
        print("end glitch");
        glEf.intensity = 0;
        glEf.enabled = false;
        //SoundManager.instance.master.SetFloat("volume", SoundManager.instance.curMasterVolume);
        //foreach (AudioSource a in noiseSounds)
        //{
        //    a.Stop();
        //}
        yield return 0;
    }



    public void GlitchScreenOnCommand(float t)
    {
        StartCoroutine(GlitchScreenOC(t));
    }

    public void GlitchScreenOnCommand(float time, float inten = -1f)
    {
        StartCoroutine(GlitchScreenOC(time, inten));
    }

    IEnumerator GlitchScreenOC(float time, float inten = -1f)
    {
        if (inten == -1f)
        {
            inten = glitchIntensity;
        }

        glEf.enabled = true;
        glEf.intensity = inten * Random.Range(0.8f, 1.2f);

        //PlayGlitchSound ();

        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return 0;
        }
        glEf.intensity = 0;
        glEf.enabled = false;
        //SoundManager.instance.master.SetFloat("volume", SoundManager.instance.curMasterVolume);
        //foreach (AudioSource a in noiseSounds)
        //{
        //    a.Stop();
        //}
        yield return 0;
    }















    void SetupTimings()
    {
        GlitchTiming t = new GlitchTiming();
        t.timeToMin = 20f;
        t.timeToMax = 70f;
        t.sustainMin = 0.1f;
        t.sustainMax = 0.4f;

        timings.Add("default", t);
    }
}




public class GlitchTiming
{
    public float timeToMin = 1f;
    public float timeToMax = 5f;
    public float sustainMin = 0.03f;
    public float sustainMax = 0.05f;
}

