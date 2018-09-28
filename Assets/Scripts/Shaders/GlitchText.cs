using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using System;

/// <summary>
/// Is placed on every text thing that should glitch.
/// </summary>
public class GlitchText : MonoBehaviour
{

    float timeToGlitch;
    TextMeshProUGUI text;

    string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZqwertyuiopåasdfghjklæøzxcvbnm,.-<1234567890+½§!#%&/()=?*<>@£$€{[]}'";
    List<char> listOfChars = new List<char>();

    // Use this for initialization
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        listOfChars.AddRange(chars.ToCharArray());
        timeToGlitch = UnityEngine.Random.Range(Glitch.instance.curTiming.textTimeMin, Glitch.instance.curTiming.textTimeMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToGlitch > 0)
        {
            timeToGlitch -= Time.deltaTime;
        }
        else
        {
            StartCoroutine(GlitchCo());
        }

    }

    public IEnumerator GlitchCo()
    {
        if(text.text.Length == 0)
        {
            yield break;
        }
        timeToGlitch = UnityEngine.Random.Range(Glitch.instance.curTiming.textTimeMin, Glitch.instance.curTiming.textTimeMax);
        char origChar = '&';
        int charToGlitch = UnityEngine.Random.Range(0, text.text.Length);
        int i = 0;

        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            //SoundManager.instance.PlayTextGlitchSound ();
        }

        foreach (char c in text.text)
        {
            if (i == charToGlitch)
            {

                try
                {
                    char newChar = listOfChars[UnityEngine.Random.Range(0, listOfChars.Count)];

                    //Swap!
                    origChar = text.text[i];
                    StringBuilder sb = new StringBuilder(text.text);
                    sb[charToGlitch] = newChar;
                    text.text = sb.ToString();
                }
                catch (Exception e)
                {
                    Debug.Log("string glitch error" + e);
                };

            }
            i++;
        }
        
        float time = UnityEngine.Random.Range(Glitch.instance.curTiming.textSustainMin, Glitch.instance.curTiming.textSustainMin);
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return 0;
        }

        //Text has changed in the time. Abort.
        if (text.text.Length == 0)
        {
            yield break;
        }

        if (text.text.Length != 0)
        {
            StringBuilder sbo = new StringBuilder(text.text);
            sbo[charToGlitch] = origChar;
            text.text = sbo.ToString();
        }

        yield return 0;
    }
}
