using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public enum SequenceName { BootUp, FirstCrash, LoadToStoryFromDrone, LoadToDrone };



public class Sequences : MonoBehaviour {

    Dictionary<SequenceName, IEnumerator> sequences = new Dictionary<SequenceName, IEnumerator>();

    public TextRoll Roller;

    public Story story;

    public Image bootUpStatic;
    public TextMeshProUGUI bootUpText;
    public TextMeshProUGUI simulationText;
    public TMP_InputField passwordInput;
    bool passwordsubmitted = false;

    Action callbackToCurrentSequence;

    
    // Use this for initialization
    void Start () {
        
        sequences.Add(SequenceName.BootUp, BootUpSequence());
        sequences.Add(SequenceName.FirstCrash, FirstCrash());
        sequences.Add(SequenceName.LoadToStoryFromDrone, LoadToStoryFromDrone());
        sequences.Add(SequenceName.LoadToDrone, LoadToDroneFromStory());



        //RunSequence(SequenceName.BootUp);
	}
	
    public void RunSequence(SequenceName sequenceToPlay, Action callback = null)
    {
        Debug.Log("Run Sequence " + sequenceToPlay);
        StopAllCoroutines();
        IEnumerator x = sequences[sequenceToPlay];
        callbackToCurrentSequence = callback;
        Debug.Log(x);
        StartCoroutine(x);
    }




    #region Sequence Coroutines

    IEnumerator BootUpSequence()
    {
        bootUpText.text = "";
        passwordInput.gameObject.SetActive(false);

        Sound.instance.PlayRandomFromList(Sound.SFXLISTS.Keyboards);

        //  print("BOOT UP");
        bootUpStatic.gameObject.SetActive(true);
        Glitch.instance.GlitchScreenOnCommand(1f,1.2f);
        yield return new WaitForSeconds(1f);
        bootUpStatic.gameObject.SetActive(false);

        Sound.instance.Play(Sound.SFXIDS.Boot);
        Sound.instance.PlayAmbient(Sound.AMBIENCES.Computer);

        TextInfo txt = new TextInfo(GlobalStrings.BootUpString.text.Replace("¤", System.Environment.NewLine), GlobalStrings.BootUpString.rolldelay, GlobalStrings.BootUpString.startdelay);

        RollInfo rollInfo = new RollInfo(bootUpText, txt, null);
        IEnumerator roll = Roller.StartRoll(txt, rollInfo.ui, null, false);

        while (roll.MoveNext())
        {
            yield return roll.Current;
        }

        passwordInput.gameObject.SetActive(true);
        passwordInput.onSubmit.AddListener((fieldText) => PasswordSubmitted(fieldText));
        passwordInput.onValueChanged.AddListener((text) => KeyPressed());

        passwordInput.Select();

        while (!passwordsubmitted)  //Waiting for password
        {
            yield return new WaitForEndOfFrame();
        }

        passwordInput.text = "";

        List<int> foundIndexes = new List<int>();
        for (int i = 0; i < bootUpText.text.Length; i++)
        {
            if (bootUpText.text[i] == System.Environment.NewLine.ToCharArray()[0])
                foundIndexes.Add(i);
        }

        int j = foundIndexes.Count - 1;
        while (bootUpText.text.Length > 100)
        {
            yield return new WaitForSeconds(0.075f);
            
            bootUpText.text = bootUpText.text.Remove(foundIndexes[j], (bootUpText.text.Length - foundIndexes[j]));
            j--;
        }

        //yield return new WaitForSeconds(1f);    //THIS NEEDS TO BE THERE?? dunno why. it doesn't actually wait 1 second, but making it 0 doesn't work either. apparently not anymore...?????? wth
        yield return new WaitForSeconds(0.35f);
        Glitch.instance.GlitchScreenOCBoot(0.15f, 1f);
        yield return new WaitForSeconds(0.15f);
        bootUpText.text = "";

        yield return new WaitForSeconds(0.2f);


        print("BOOT DONE");
        if (callbackToCurrentSequence != null)
        {
            callbackToCurrentSequence();
        }
    }
    
    void KeyPressed()
    {
        Sound.instance.PlayRandomFromList(Sound.SFXLISTS.Keyboards);
    }

    void PasswordSubmitted(string fieldText)
    {
        if(fieldText.Length > 0)
        {
            passwordsubmitted = true;
        }
        else
        {
            passwordsubmitted = false;
        }
    }
    

    IEnumerator FirstCrash()
    {
        yield return new WaitForEndOfFrame();



        if (callbackToCurrentSequence != null)
        {
            callbackToCurrentSequence();
        }
    }


    IEnumerator LoadToStoryFromDrone()
    {
        Debug.Log("Load To Story START");
        Sound.instance.PlayRandomFromList(Sound.SFXLISTS.Keyboards);
        Glitch.instance.GlitchScreenOnCommand(0.5f, 0.7f);
        yield return new WaitForSeconds(0.5f);
        bootUpStatic.gameObject.SetActive(false);



        Glitch.instance.DisableDroneEffects();
        Sound.instance.StopAmbient(Sound.AMBIENCES.Drone);
        story.ui.SetSoleCanvas(UIManager.CanvasType.Boot);
        Sound.instance.StopAmbient(Sound.AMBIENCES.Computer);

        yield return new WaitForSeconds(0.5f);

        yield return LoadingBlip();

        Sound.instance.Play(Sound.SFXIDS.Boot);
        Sound.instance.PlayAmbient(Sound.AMBIENCES.Computer);

        if (callbackToCurrentSequence != null)
        {
            callbackToCurrentSequence();
        }
        Debug.Log("Load To Start END");

    }

    IEnumerator LoadToDroneFromStory()
    {
        Debug.Log("Load To Drone START");

        Sound.instance.PlayRandomFromList(Sound.SFXLISTS.Keyboards);
        Glitch.instance.GlitchScreenOnCommand(0.5f, 0.7f);
        yield return new WaitForSeconds(0.5f);

        Sound.instance.StopAmbient(Sound.AMBIENCES.Computer);
        story.ui.SetSoleCanvas(UIManager.CanvasType.Boot);
        bootUpStatic.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        yield return LoadingBlip();

        Sound.instance.Play(Sound.SFXIDS.Boot);
        Sound.instance.PlayAmbient(Sound.AMBIENCES.Drone);
        Glitch.instance.EnableDroneEffects();

        if (callbackToCurrentSequence != null)
        {
            callbackToCurrentSequence();
        }
        Debug.Log("Load To Drone END");

    }


    IEnumerator LoadingBlip()
    {
        for (int i = 0; i < 3; i++)
        {
            simulationText.gameObject.SetActive(true);
            Sound.instance.Play(Sound.SFXIDS.Bleep);
            yield return new WaitForSeconds(0.7f);
            simulationText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.7f);
        }
    }


    #endregion


}
