using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Dialogues : MonoBehaviour {

    public UIManager ui;
    public TextRoll roll;

    public TextMeshProUGUI mainText;
    public TextMeshProUGUI thoughtsText;
    public GameObject buttonParent;
    public Button[] buttons;
    public TextMeshProUGUI[] buttonTexts;

    public VD.NodeData curNode;

    //This handles Dialogue directly, in the UI and the connection to VIDE (there's no reason to have a link there). 
    


    public string CurrentDialogue { get; private set; }

	// Use this for initialization
	void Start () {

        //    StartDialogues("Tari_Intro");

        for (int i = 0; i < buttons.Length; i++)
        {
            int istore = i;
            buttons[i].onClick.AddListener(() => SetPlayerChoice(istore));
        }

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            NextNode();
        }
    }


    public void StartDialogues(string startDial)
    {
        CurrentDialogue = startDial;
        LoadDialogue(startDial);
    }
    public void NextNode()
    {
        if (VD.isActive && !curNode.isPlayer)
        {
            VD.Next();  //Gonna need some checks on this to see if its possible when we do text roll and things. 
        }
    }

    public bool DoesDialogueExist(string dialogueName)
    {

        if (VD.GetDialogues().Contains(dialogueName))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    //NTS: you can never put an action node between a player and an npc node. it will break.
    public void LoadDialogue(string dialogueName)
    {
        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += End;

        mainText.gameObject.SetActive(true);
        mainText.text = "";
        thoughtsText.text = "";
        buttonParent.SetActive(true);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        VD.BeginDialogue(dialogueName);
    }


    void UpdateUI(VD.NodeData data)
    {
        curNode = data;
        StopCoroutine("GoNextAfterDelay");
        if (data.isPlayer)      // ------------------ PLAYER
        {

            for (int i = 0; i < buttons.Length; i++)
            {
                if (i < data.comments.Length)
                {
                    buttons[i].gameObject.SetActive(true);
                    buttonTexts[i].text = data.comments[i];
                }
                else
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }
        else                   // ------------------- NPC
        {
            mainText.text = string.Empty;
            thoughtsText.text = string.Empty;

            roll.StartRoll(data.comments[data.commentIndex], mainText);
            //mainText.text = data.comments[data.commentIndex];       //maybe I want them to be able to layer? so one thing goes, then another, then another? for line breaks. yeeah. gotta specify in extradata if should break or continue.
            roll.StartRoll(data.comments_secondaries[data.commentIndex], thoughtsText);

            //            thoughtsText.text = data.comments_secondaries[data.commentIndex];

            if ((data.commentIndex + 1) >= data.comments.Length && VD.GetNext(false, false).isPlayer)
            {
                float delay = 1; //SET TO A DEFAULT DELAY
                //we've reached the last comment, go to next after delay.
                if (data.extraVars.ContainsKey("delay"))
                {
                    object obj = data.extraVars["delay"];
                    if (obj is float)
                    {
                        delay = (float)obj;
                    }
                    else if (obj is int)
                    {
                        delay = (float)((int)obj);
                    }
                    else
                    {
                        Debug.LogError("Delay was not a number. using default");
                    }
                }
                StartCoroutine("GoNextAfterDelay", delay);
            }
        }
    }


    IEnumerator GoNextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        VD.Next();
    }


    void SetPlayerChoice(int choice)
    {
        VD.nodeData.commentIndex = choice;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        VD.Next();
    }

    public void End(VD.NodeData data)
    {
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        mainText.gameObject.SetActive(false);
        buttonParent.SetActive(false);
        VD.EndDialogue();
    }


    void OnDisable()
    {
        if (mainText != null)
        {
            End(null);
        }
    }



    public void HandleDialogueSwitch(string newDialogue)
    {
        //some checks here for the dialogue for parameters maybe (how?)

        End(null);

        LoadDialogue(newDialogue);
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
