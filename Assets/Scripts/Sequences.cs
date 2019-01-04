using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public enum SequenceName { BootUp, FirstCrash, LoadToStoryFromDrone };



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



        //RunSequence(SequenceName.BootUp);
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void RunSequence(SequenceName sequenceToPlay, Action callback = null)
    {
        IEnumerator x = sequences[sequenceToPlay];
        callbackToCurrentSequence = callback;
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
        IEnumerator roll = Roller.Roll(txt, rollInfo); //i don't like bypassing the start like this...
      //  StartCoroutine()
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
        Sound.instance.PlayRandomFromList(Sound.SFXLISTS.Keyboards);
        Glitch.instance.GlitchScreenOnCommand(0.5f, 0.7f);
        yield return new WaitForSeconds(0.5f);
        //bootUpStatic.gameObject.SetActive(false);



        Glitch.instance.DisableDroneEffects();
        Sound.instance.StopAmbient(Sound.AMBIENCES.Drone);
        story.ui.SetSoleCanvas(UIManager.CanvasType.Boot);
        Sound.instance.StopAmbient(Sound.AMBIENCES.Computer);
        bootUpStatic.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 3; i++)
        {
            simulationText.gameObject.SetActive(true);
            Sound.instance.Play(Sound.SFXIDS.Bleep);
            yield return new WaitForSeconds(0.7f);
            simulationText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.7f);
        }

        //bootUpStatic.gameObject.SetActive(true);
        //  Glitch.instance.GlitchScreenOnCommand(0.6f, 1.2f);

        Sound.instance.Play(Sound.SFXIDS.Boot);
        Sound.instance.PlayAmbient(Sound.AMBIENCES.Computer);


        //TextInfo txt = new TextInfo(GlobalStrings.LoadingCharacterString.text.Replace("¤", System.Environment.NewLine), GlobalStrings.LoadingCharacterString.rolldelay, GlobalStrings.LoadingCharacterString.startdelay);

        //IEnumerator roll = Roller.Roll(txt, bootUpText);
        ////  StartCoroutine()
        //while (roll.MoveNext())
        //{
        //    yield return roll.Current;
        //}


        //List<int> foundIndexes = new List<int>();
        //for (int i = 0; i < bootUpText.text.Length; i++)
        //{
        //    if (bootUpText.text[i] == System.Environment.NewLine.ToCharArray()[0])
        //        foundIndexes.Add(i);
        //}

        //int j = foundIndexes.Count - 1;
        //while (bootUpText.text.Length > 100)
        //{
        //    yield return new WaitForSeconds(0.075f);

        //    bootUpText.text = bootUpText.text.Remove(foundIndexes[j], (bootUpText.text.Length - foundIndexes[j]));
        //    j--;
        //}

        //yield return new WaitForSeconds(0.35f);
        //Glitch.instance.GlitchScreenOnCommand(0.15f, 1f);
        //yield return new WaitForSeconds(0.15f);
        //bootUpText.text = "";



        //yield return new WaitForSeconds(0.6f);

        if (callbackToCurrentSequence != null)
        {
            callbackToCurrentSequence();
        }
    }


    #endregion


}
