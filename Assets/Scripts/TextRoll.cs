using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class TextRoll : MonoBehaviour
{

    Dictionary<TextMeshProUGUI, RollInfo> rollings = new Dictionary<TextMeshProUGUI, RollInfo>();

    public IEnumerator StartRoll(string text, TextMeshProUGUI UI, Action callback = null, bool start = true)
    {
        //text = @text.Replace("¤", System.Environment.NewLine);
        TextInfo textInfo = new TextInfo(text, GlobalVariables.TextRollDelay, GlobalVariables.TextStartDelay);
        return StartRoll(textInfo, UI, callback, start);
    }


    public IEnumerator StartRoll(TextInfo text, TextMeshProUGUI UI, Action callback = null, bool start = true, bool overrideCurrent = false)
    {
        return StartRoll(text, new RollInfo(UI, text, callback), callback, start, overrideCurrent);
    }


    public IEnumerator StartRoll(TextInfo text, RollInfo rollInfo, Action callback = null, bool start = true, bool overrideCurrent = false)
    {
        TextInfo txt = new TextInfo(text.text, text.rolldelay, text.startdelay);
        IEnumerator enumerator = null;

        print("Roll: Text: " + text.text + " Delay: " + text.startdelay + " Speed: " + text.rolldelay);

        if (!rollings.ContainsKey(rollInfo.ui))
        {
            rollings.Add(rollInfo.ui, rollInfo);
        }
        else
        {
            rollings[rollInfo.ui].textQueue.Enqueue(text);
        }

        if (rollings[rollInfo.ui].isRunning)
        {
            if (overrideCurrent) //if it is running and it should override stop
            {
                rollings[rollInfo.ui].shouldStop = true;
            }
        }
        else
        {
            rollings[rollInfo.ui].shouldStop = false;
            enumerator = Roll(rollings[rollInfo.ui].textQueue.Dequeue(), rollings[rollInfo.ui]);
            rollings[rollInfo.ui].currentEnumerator = enumerator;
            if (start)
            {
                StartCoroutine(enumerator);
            }
        }

        return enumerator;
    }




    //so the idea: when we get a new textinfo, we check the rolling dictionary to see if we have anything that rolls into that UI already (hence why UI is key). if not, we create a new one. then we add it to the queue of that list.
    //after we've added to the queue we check if that's already running and if not we run it at the top of the queue
    //at the end of the roll function (or in a top func?) we check if there are more things in the queue, and if so, we run those. each can call a callback?



    private IEnumerator Roll(TextInfo text, RollInfo roll)
    {
        bool isColored = false;

        roll.currentlyWritingTextInfo = text;
        roll.isRunning = true;

        if(text.startdelay > 0)
        {
            yield return new WaitForSeconds(text.startdelay);
        }


        int i = 0;
        while (i < text.text.Length)
        {

            if (roll.shouldStop)
            {
                EndRoll(roll);
                yield break;
            }
            //	if(shouldStopRolling){
            //		Debug.Log("STOP");
            //		return true;
            //	}

            if(text.text[i] == '|')
            {
                string time = "";
                int tIdx = i+1;
                bool timeRecorded = false;
                while (!timeRecorded)
                {
                    if(text.text[tIdx] == '|')
                    {
                        timeRecorded = true;
                        tIdx++;
                        break;
                    }
                    time += text.text[tIdx];
                    tIdx++;
                }
                print(time + " " + tIdx);
                float t = float.Parse(time);
                yield return new WaitForSeconds(t);
                i = i + (tIdx-i);
            }

            if (text.text[i] == '>')
            {
                //default Escape Note: Color text.
                roll.ui.text += "<color=#6f6f6fff>" + "</color>"; //wow that hardcoding :P
                isColored = true;
                Debug.Log("Set to isColored True");
            }
            else if (isColored)
            {
                roll.ui.text = roll.ui.text.Substring(0, roll.ui.text.Length - 8); //?? deleting the </color> stuff i think?
                if (text.text[i] == '<')
                {
                    isColored = false;
                    Debug.Log("Set to isColored False");
                    roll.ui.text += "</color>";
                }
                else
                {
                    roll.ui.text += text.text[i] + "</color>";
                }
            }
            else
            {
                roll.ui.text += text.text[i];
            }
            if (text.text[i] != ' ')
            {
                Sound.instance.PlaySacred(Sound.SFXIDS.Text,Sound.UNIQUESOURCES.Text, true);
                //Sound.instance.Play(Sound.SFXIDS.Text, true);
            }

            i++;
            yield return new WaitForSeconds(text.rolldelay);
        }



        if (rollings[roll.ui].textQueue.Count > 0)
        {
            rollings[roll.ui].currentEnumerator = Roll(rollings[roll.ui].textQueue.Dequeue(), roll);
            StartCoroutine(rollings[roll.ui].currentEnumerator);
        }
        else
        {
            roll.isRunning = false;
            if (roll.callback != null)
            {
                roll.callback();
            }
        }

    }

    public void FinishRollForced(TextMeshProUGUI ui)
    {
        Debug.Log("force stopping " + ui.name);
        if (rollings.ContainsKey(ui))
        {
            rollings[ui].shouldStop = true;
        }
    }

    private void EndRoll(RollInfo roll)
    {

        roll.ui.text = roll.currentlyWritingTextInfo.text;

        int count = roll.textQueue.Count;
        for (int i = 0; i < count; i++)
        {
            //if(roll.textQueue.Peek() != null)
            //{
                roll.ui.text += roll.textQueue.Dequeue().text;
            //}
        }
        

        roll.isRunning = false;

        if (roll.callback != null)
        {
            roll.callback();
        }
    }

    public void EmptyRoll(TextMeshProUGUI ui)
    {
        rollings[ui].textQueue.Clear();
        StopCoroutine(rollings[ui].currentEnumerator);
    }

}

public class RollInfo
{
    public TextMeshProUGUI ui;
    public TextInfo currentlyWritingTextInfo;
    public Queue<TextInfo> textQueue = new Queue<TextInfo>();
    public Action callback;
    public IEnumerator currentEnumerator;
    public bool isRunning = false;
    public bool shouldStop = false;
    
    public RollInfo(TextMeshProUGUI u, TextInfo t, Action c)
    {
        ui = u;
        textQueue.Enqueue(t);
        callback = c;
    }

}