using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Dialogues : MonoBehaviour {

    public enum DialogueMode { Dialogue, Ambient }

    public UIManager ui;
    public TextRoll roll;

    public TextMeshProUGUI mainText;
    public TextMeshProUGUI thoughtsText;
    public TextMeshProUGUI ambientText;
    public TextMeshProUGUI ambientThoughts;
    public GameObject buttonParent;
    public Button[] buttons;
    public TextMeshProUGUI[] buttonTexts;
    public Button nextButton;

    public VD.NodeData curNode;

    public DialogueMode mode;


    public TextMeshProUGUI MainText
    {
        get
        {
            return mode == DialogueMode.Dialogue ? mainText : ambientText;
        }
    }

    public TextMeshProUGUI ThoughtsText
    {
        get
        {
            return mode == DialogueMode.Dialogue ? thoughtsText : ambientThoughts;
        }
    }
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

        nextButton.onClick.AddListener(() => NextNode());

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

    public void ForceNextNode()
    {
        if (!Story.forcingAllowed)
        {
            return;
        }

        //stop rolling text somehow??
        //and display all text
        print("forcing stop");
        roll.FinishRollForced(MainText);
        roll.FinishRollForced(ThoughtsText);

       // NextNode();

    }

    public void NextNode()
    {
        print("next is called");
        if (VD.isActive && !curNode.isPlayer)
        {
            VD.Next();  //Gonna need some checks on this to see if its possible when we do text roll and things.  Would also be nice to have a click to reveal all and stop roll - for speeding stuff.
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


    void Setup()
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
        nextButton.gameObject.SetActive(false);

    }

    //NTS: you can never put an action node between a player and an npc node. it will break.
    public void LoadDialogue(string dialogueName)
    {
        Setup();
        VD.BeginDialogue(dialogueName);
    }

    public void LoadDialogue(string dialogueName, int overrideStartNode)
    {
        Setup();
        VIDE_Assign assign = new VIDE_Assign();
        assign.overrideStartNode = overrideStartNode;
        assign.AssignNew(dialogueName);

        VD.BeginDialogue(assign);

    }


    void UpdateUI(VD.NodeData data)
    {
        curNode = data;
        StopCoroutine("GoNextAfterDelay");
        ParseAndSetModeFromNode(data);

        ui.eventSys.SetSelectedGameObject(null);

       // if(mode == DialogueMode.Dialogue)
       // {
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
            ambientThoughts.text = string.Empty;
            ambientText.text = string.Empty;


            System.Action callback = null;
            if((data.commentIndex + 1) < data.comments.Length)
            {
                callback = ShowNextButton;
            }
            else if ((data.commentIndex + 1) >= data.comments.Length && VD.GetNext(false, false).isPlayer)
            {
                callback = ShowNextPlayerOptions;
            }



            roll.StartRoll(ConvertCommentToTextInfo(data), MainText, callback);
            roll.StartRoll(ConvertCommentToTextInfo(data, true), ThoughtsText);

        }
       // }
    }

    void ShowNextButton()
    {
        nextButton.gameObject.SetActive(true);
    }

    void ShowNextPlayerOptions()
    {
        NextNode();
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
        print("end"); 
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
        //some checks here for the dialogue for parameters maybe (how?) Why isn't this called? Doesn't it progress to next node now??
        print("handle switch " + newDialogue);
        if (!VD.GetDialogues().Contains(newDialogue))
        {
            Debug.LogError("Dialogues didn't contain " + newDialogue + "Make sure we're in the right spot and everything is spelled right.");
            return;
        }

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


    void ParseAndSetModeFromNode(VD.NodeData node)
    {
        if (node.extraVars.ContainsKey("mode"))
        {
            object obj = node.extraVars["mode"];
            string s = (string)obj;

            try {
                mode = (DialogueMode)System.Enum.Parse(typeof(DialogueMode), s);

            }
            catch
            {
                Debug.LogError("Dialogue Mode couldn't be parsed. Spelling error? " + s + " in node "+node.nodeID);
            }
        }
        else
        {
            return; //no change. keep as is.
        }
    }

    
    //TODO: Would like a more elegant solution than two functions that do essentially the same thing but I can't WAIT I KNOW NOW GAAH maybe store variable in start and set to that ? that should be fine?
    TextInfo ConvertCommentToTextInfo(VD.NodeData node)
    {
        TextInfo text = new TextInfo();

        text.text = node.comments[node.commentIndex];

        if (node.extraData[node.commentIndex].Contains("dd="))
        {
            //contains extradata delay. use that.
            string[] extradata = node.extraData[node.commentIndex].Split(',');
            foreach(string s in extradata)
            {
                if (s.Contains("dd="))
                {
                    text.startdelay = float.Parse(s.Split('=')[1]); //should get the number after = !
                }
            }
        }
        else if (node.extraVars.ContainsKey("delay"))
        {
            //use that
            object obj = node.extraVars["delay"];
            float d = 0;
            if (obj is float)
            {
                d = (float)obj;
            }
            else if (obj is int)
            {
                d = (float)((int)obj);
            }
            else
            {
                Debug.LogError("Delay was not a number. using default");
                d = GlobalVariables.TextStartDelay;
            }

            text.startdelay = d;
        }
        else
        {
            text.startdelay = GlobalVariables.TextStartDelay;
        }


        //SPEED
        if (node.extraData[node.commentIndex].Contains("ds="))
        {
            //contains extradata delay. use that.
            string[] extradata = node.extraData[node.commentIndex].Split(',');
            foreach (string s in extradata)
            {
                if (s.Contains("ds="))
                {
                    text.rolldelay = float.Parse(s.Split('=')[1]); //should get the number after = !
                }
            }
        }
        else if (node.extraVars.ContainsKey("speed"))
        {
            //use that

            object obj = node.extraVars["speed"];
            float s = 0;
            if (obj is float)
            {
                s = (float)obj;
            }
            else if (obj is int)
            {
                s = (float)((int)obj);
            }
            else
            {
                Debug.LogError("Delay was not a number. using default");
                s = GlobalVariables.TextRollDelay;
            }


            text.rolldelay = s;
        }
        else
        {
            text.rolldelay = GlobalVariables.TextRollDelay;
        }



        return text;
    }



    TextInfo ConvertCommentToTextInfo(VD.NodeData node, bool secondary)
    {
        TextInfo text = new TextInfo();

        text.text = node.comments_secondaries[node.commentIndex];

        if (node.extraData[node.commentIndex].Contains("td="))
        {
            //contains extradata delay. use that.
            string[] extradata = node.extraData[node.commentIndex].Split(',');
            foreach (string s in extradata)
            {
                if (s.Contains("td="))
                {
                    text.startdelay = float.Parse(s.Split('=')[1]); //should get the number after = !
                }
            }
        }
        else if (node.extraVars.ContainsKey("Tdelay"))
        {
            //use that
            object obj = node.extraVars["Tdelay"];
            float d = 0;
            if (obj is float)
            {
                d = (float)obj;
            }
            else if (obj is int)
            {
                d = (float)((int)obj);
            }
            else
            {
                Debug.LogError("Delay was not a number. using default");
                d = GlobalVariables.TextStartDelay;
            }

            text.startdelay = d;
        }
        else
        {
            text.startdelay = GlobalVariables.TextStartDelay;
        }


        //SPEED
        if (node.extraData[node.commentIndex].Contains("ts="))
        {
            //contains extradata delay. use that.
            string[] extradata = node.extraData[node.commentIndex].Split(',');
            foreach (string s in extradata)
            {
                if (s.Contains("ts="))
                {
                    text.rolldelay = float.Parse(s.Split('=')[1]); //should get the number after = !
                }
            }
        }
        else if (node.extraVars.ContainsKey("Tspeed"))
        {
            object obj = node.extraVars["Tspeed"];
            float s = 0;
            if (obj is float)
            {
                s = (float)obj;
            }
            else if (obj is int)
            {
                s = (float)((int)obj);
            }
            else
            {
                Debug.LogError("Delay was not a number. using default");
                s = GlobalVariables.TextRollDelay;
            }


            text.rolldelay = s;
        }
        else
        {
            text.rolldelay = GlobalVariables.TextRollDelay;
        }



        return text;
    }





    //How to deal with secondaries?? that's also in the same node. hm.
}
