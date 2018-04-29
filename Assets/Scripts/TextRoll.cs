using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class TextRoll : MonoBehaviour {
    

    public void StartRoll(string text, TextMeshProUGUI UI, Action callback = null)
    {
        text = @text.Replace("¤", System.Environment.NewLine);
        TextInfo textInfo = new TextInfo(text, GlobalVariables.TextRollDelay, 0);
        StartCoroutine(Roll(textInfo, UI, callback));
    }

    public void StartRoll(TextInfo text, TextMeshProUGUI UI, Action callback = null)
    {
        TextInfo txt = new TextInfo(@text.text.Replace("¤", System.Environment.NewLine),text.rolldelay,text.startdelay);

        StartCoroutine(Roll(txt, UI, callback));
    }


    public IEnumerator Roll(TextInfo text, TextMeshProUGUI UI, Action callback = null)
    {
        bool isColored = false;

        int i = 0;
        while (i < text.text.Length)
        {
            //	if(shouldStopRolling){
            //		Debug.Log("STOP");
            //		return true;
            //	}
            if (text.text[i] == '<')
            {
                UI.text += "<color=#1f1f1fff>" + "</color>";
                isColored = true;
            }
            else if (isColored)
            {
                UI.text = UI.text.Substring(0, UI.text.Length - 8);
                if (text.text[i] == '>')
                {
                    isColored = false;
                    UI.text += "</color>";
                }
                else
                {
                    UI.text += text.text[i] + "</color>";
                }
            }
            else
            {
                UI.text += text.text[i];
            }
            if (text.text[i] != ' ')
            {
                Sound.instance.Play(Sound.SFXIDS.Text,true);
            }

            i++;
            yield return new WaitForSeconds(text.rolldelay);
        }


        if(callback != null)
        {
            callback();
        }
    }


}
