using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class Dialogues : MonoBehaviour {

    public UIManager ui;

    //This is the link between the Story and the UI - it handles which dialogues we load and where we are in the story.


    public string CurrentDialogue { get; private set; }

	// Use this for initialization
	void Start () {

    //    StartDialogues("Tari_Intro");

	}


    public void StartDialogues(string startDial)
    {
        CurrentDialogue = startDial;
        ui.LoadDialogue(startDial);
    }
	

    public void HandleDialogueSwitch(string newDialogue)
    {
        //some checks here for the dialogue for parameters maybe (how?)

        ui.End(null);

        ui.LoadDialogue(newDialogue);
    }

    public void HandleStorySwitch()
    {
        //Dummy for when that's in.
    }

    public void HandleGeneralSwitch()
    {
        //Dummy also. not sure what for yet. But want to keep open the possibility of other things than dialogue and story switches. maybe I want boots and computer things.
    }


}
