using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public TextMeshProUGUI main;
    public GameObject buttonParent;
    public Button[] buttons;
    public TextMeshProUGUI[] buttonTexts;

    public string dialogueToLoad = "";


	// Use this for initialization
	void Start () {

        for (int i = 0; i < buttons.Length; i++)
        {
            int istore = i;
            buttons[i].onClick.AddListener(() => SetPlayerChoice(istore));
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!VD.isActive)
            {
                LoadDialogue(dialogueToLoad);
            }
            else
            {
                VD.Next();  //Gonna need some checks on this to see if its possible when we do text roll and things. 
            }
        }

	}


    public void LoadDialogue(string dialogueName)
    {
        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += End;

        main.gameObject.SetActive(true);
        buttonParent.SetActive(true);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        VD.BeginDialogue(dialogueName);
    }

    
    void UpdateUI(VD.NodeData data)
    {
        StopCoroutine("GoNextAfterDelay");
        if (data.isPlayer)      // ------------------ PLAYER
        {

            for (int i = 0; i < buttons.Length; i++)
            {
                if(i < data.comments.Length)
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
            main.text = data.comments[data.commentIndex];

            if((data.commentIndex+1) >= data.comments.Length && VD.GetNext(false,false).isPlayer)
            {
                float delay = 1; //SET TO A DEFAULT DELAY
                //we've reached the last comment, go to next after delay.
                if (data.extraVars.ContainsKey("delay"))
                {
                    object obj = data.extraVars["delay"];
                    if(obj is float)
                    {
                        delay = (float)obj;
                    }
                    else if(obj is int)
                    {
                        delay = (float)((int)obj);
                    }
                    else
                    {
                        Debug.LogError("Delay was not a number. using default");
                    }
                }
                StartCoroutine("GoNextAfterDelay",delay);
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

    void End(VD.NodeData data)
    {
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        main.gameObject.SetActive(false);
        buttonParent.SetActive(false);
        VD.EndDialogue();
    }


    void OnDisable()
    {
        if(main != null)
        {
            End(null);
        }
    }

}
