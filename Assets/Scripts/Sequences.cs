using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public enum SequenceName { FirstBoot, FirstCrash, LoadToStoryFromDrone, LoadToDrone, Dummy };

public class Sequences : MonoBehaviour
{

    Dictionary<SequenceName, IEnumerable> sequences = new Dictionary<SequenceName, IEnumerable>();

    public TextRoll Roller;

    public Story story;

    public Image bootUpStatic;
    public TextMeshProUGUI bootUpText;
    public TextMeshProUGUI simulationText;
    public TMP_InputField passwordInput;
    public Button bootButton;
    public Button passwordButton;
    bool passwordsubmitted = false;

    IEnumerable currentRoutine = null;
    Action callbackToCurrentSequence;

    
    // Use this for initialization
    void Start ()
    {
        sequences.Add(SequenceName.FirstBoot, FirstBoot());
        sequences.Add(SequenceName.FirstCrash, FirstCrash());
        sequences.Add(SequenceName.LoadToStoryFromDrone, LoadToStoryFromDrone());
        sequences.Add(SequenceName.LoadToDrone, LoadToDroneFromStory());
        sequences.Add(SequenceName.Dummy, Dummy());
	}
	
    public void RunSequence(SequenceName sequenceToPlay, Action callback = null)
    {
        StopAllCoroutines();
        currentRoutine = sequences[sequenceToPlay];
        callbackToCurrentSequence = callback;
        StartCoroutine(Runner(sequenceToPlay,callback));
    }

    IEnumerator Runner(SequenceName sequenceToPlay, Action callback = null)
    {
        yield return currentRoutine.GetEnumerator();
        if (callbackToCurrentSequence != null)
        {
            callbackToCurrentSequence();
        }
    }

    IEnumerable Dummy()
    {
        yield return null;
    }


    #region Sequence Coroutines

    IEnumerable FirstBoot()
    {
        bootUpText.text = "";
        passwordInput.gameObject.SetActive(false);
        bootButton.gameObject.SetActive(false);

        Sound.instance.PlayRandomFromList(Sound.SFXLISTS.Keyboards);

        bootUpStatic.gameObject.SetActive(true);
        Glitch.instance.GlitchScreenOnCommand(1f,1.2f);
        yield return new WaitForSeconds(1f);
        bootUpStatic.gameObject.SetActive(false);

        yield return BootUp(GlobalStrings.FirstBootUpString).GetEnumerator();
    }



    IEnumerable BootUp(TextInfo text)
    {
        Sound.instance.Play(Sound.SFXIDS.Boot);
        Sound.instance.PlayAmbient(Sound.AMBIENCES.Computer);

        IEnumerator roll = Roller.StartRoll(text, bootUpText);

        while (roll.MoveNext())
        {
            yield return roll.Current;
        }

        passwordInput.gameObject.SetActive(true);
        passwordInput.onSubmit.AddListener((fieldText) => PasswordSubmitted(fieldText));
        passwordInput.onValueChanged.AddListener((t) => KeyPressed());
        passwordButton.gameObject.SetActive(true);
        passwordsubmitted = false;

        passwordInput.Select();

        while (!passwordsubmitted)  //Waiting for password
        {
            yield return new WaitForEndOfFrame();
        }

        passwordInput.text = "";
        passwordButton.gameObject.SetActive(false);

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

        yield return new WaitForSeconds(0.35f);
        Glitch.instance.GlitchScreenOCWithVerticalJump(0.15f, 1f);
        yield return new WaitForSeconds(0.15f);
        bootUpText.text = "";

        yield return new WaitForSeconds(0.2f);
    }
    


    IEnumerable FirstCrash()
    {
        Glitch.instance.GlitchScreenOCWithDigital(3f, 1.5f);
        Sound.instance.MuteMixer(Sound.MasterMixerVars.inWorldVolume);
        Sound.instance.Play(Sound.SFXIDS.Warning);
        yield return new WaitForSeconds(3);
        story.ui.SetSoleCanvas(UIManager.CanvasType.Boot);
        Sound.instance.Stop(Sound.SFXIDS.Warning);
        bootUpStatic.gameObject.SetActive(false);

        Sound.instance.PlayAmbient(Sound.AMBIENCES.Work);
        bootUpText.text = "";
        bootButton.gameObject.SetActive(true);
        bootButton.onClick.RemoveAllListeners();


        TextMeshProUGUI buttonText = bootButton.GetComponentInChildren<TextMeshProUGUI>();
        IEnumerator roll = Roller.StartRoll(GlobalStrings.BrokenNextButton, buttonText);

        yield return roll;

        yield return WaitForButton();

        bootButton.gameObject.SetActive(false);

        buttonText.text = "";

        roll = Roller.StartRoll(GlobalStrings.Crash, bootUpText);

        yield return roll;

        yield return new WaitForSeconds(1);
        bootUpText.text += "\n";

        roll = Roller.StartRoll(GlobalStrings.CrashLoadingDrone, bootUpText);

        yield return roll;

        yield return new WaitForSeconds(1);

        Sound.instance.PlayRandomFromList(Sound.SFXLISTS.Keyboards);
        bootUpText.text = "";

        Sound.instance.StopAmbient(Sound.AMBIENCES.Computer);

        yield return new WaitForSeconds(4);

        IEnumerator boot = BootUp(GlobalStrings.BootToDroneString).GetEnumerator();

        yield return boot;

        Sound.instance.StopAmbient(Sound.AMBIENCES.Work);


        yield return new WaitForEndOfFrame();

    }






    IEnumerable LoadToStoryFromDrone()
    {
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
    }

    IEnumerable LoadToDroneFromStory()
    {
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
    }

    #endregion


    #region Utility Things

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

    IEnumerator WaitForButton()
    {
        bootButton.onClick.RemoveAllListeners();
        bool clicked = false;

        bootButton.onClick.AddListener(() => { clicked = true; });

        while (!clicked)
        {
            yield return new WaitForEndOfFrame();
        }

        bootButton.onClick.RemoveAllListeners();
    }


    void KeyPressed()
    {
        Sound.instance.PlayRandomFromList(Sound.SFXLISTS.Keyboards);
    }

    void PasswordSubmitted(string fieldText)
    {
        if (fieldText.Length > 0)
        {
            passwordsubmitted = true;
        }
        else
        {
            passwordsubmitted = false;
        }
    }


    #endregion


}
