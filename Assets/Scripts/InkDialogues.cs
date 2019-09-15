using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ink.Runtime;


/// <summary>
/// Alright, now I think this will display the text. let's test (need to set it up in editor)
/// </summary>
public class InkDialogues : MonoBehaviour
{
    public enum DialogueMode { Dialogue, Ambient }
    public enum Place { Main, Secondary }
    enum Id { Speed, Delay, GlitchTiming, GlitchCommand, SoundPlay, SoundStop }


    [SerializeField] TextAsset inkJSONAsset;

    //Ink.Runtime.Story inkStory;

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

    public Ink.Runtime.Story CurrentDialogue { get; private set; }

    public DialogueMode mode;

    int buttonEnableCounter;

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



    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(() =>
        {
            RefreshView();
        }); 
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        //{
        //    RefreshView();
        //}
    }

    public void StartDialogue(string dialName, string overrideStart = "START")
    {
        CurrentDialogue = new Ink.Runtime.Story(InkDatabase.Get(dialName).text); //currently doesn't care about name. need to load from resources.
        if(overrideStart != "START")
        {
            CurrentDialogue.ChoosePathString(overrideStart);
        }
        Setup();
        RefreshView();
    }


    // This is the main function called every time the story changes. It does a few things:
    // Destroys all the old content and choices.
    // Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
    void RefreshView()
    {
        // Remove all the UI on screen
        ClearText();
        buttonEnableCounter = 0;
        addedtoMain = false;
        addedToSecondary = false;

        bool first = true;
        while (CurrentDialogue.canContinue)
        {
            string text = CurrentDialogue.Continue();
            CreateContentView(text, CurrentDialogue);
        }
        

        if (CurrentDialogue.currentChoices.Count <= 0) // END. HANDLE THIS IN STORY
        {
            HandleSwitch(CurrentDialogue.currentText.Trim(new char[] { '@', '\n' }));
        }
    }

    public void HandleSwitch(string newStory)
    {
        if (!InkDatabase.Contains(newStory))
        {
            Debug.LogError("Dialogues didn't contain " + newStory + "! Make sure we're in the right spot and everything is spelled right.");
            return;
        }
        StartDialogue(newStory);
    }


    void OnClickChoiceButton(Choice choice)
    {
        CurrentDialogue.ChooseChoiceIndex(choice.index);
        RefreshView();
    }

    // Creates a button showing the choice text
    void CreateContentView(string text, Ink.Runtime.Story inkStory)
    {
        if (text.Contains("@@")) //@@ is for changing to new story, is handled by RefreshView
        {
            return;
        }
        else if (text.Contains("%%")) 
        {
            //mode switch!
            ParseAndSetModeFromNode(text);
        }
        else
        {
            System.Action callback = CreateChoiceView;

            if (text.StartsWith("§§"))
            {
                roll.StartRoll(ConvertCommentToTextInfo(text.Substring(2, text.Length - 2), inkStory, Place.Secondary), ThoughtsText, callback);
                AddToCounter(Place.Secondary);
            }
            else
            {
                roll.StartRoll(ConvertCommentToTextInfo(text, inkStory, Place.Main), MainText, callback);
                AddToCounter(Place.Main);
            }
        }
    }


    void ParseAndSetModeFromNode(string modeChange)
    {
        if (modeChange.Contains("%%Ambient%%") || modeChange.Contains("%%Dialogue%%"))
        {
            string s = modeChange.Trim(new char[] { '%', '\n' });

            try
            {
                DialogueMode prevmode = mode;

                mode = (DialogueMode)System.Enum.Parse(typeof(DialogueMode), s);

                if (prevmode == DialogueMode.Ambient && mode == DialogueMode.Dialogue)
                {
                    nextButton.gameObject.SetActive(false);
                }
            }
            catch
            {
                Debug.LogError("Dialogue Mode couldn't be parsed. Spelling error? " + modeChange);
            }
        }
    }


    bool addedtoMain = false;
    bool addedToSecondary = false;

    void AddToCounter(Place place)
    {
        if (place == Place.Main && !addedtoMain)
        {
            buttonEnableCounter++;
            addedtoMain = true;
        }
        if(place == Place.Secondary && !addedToSecondary)
        {
            buttonEnableCounter++;
            addedToSecondary = true;
        }
    }

    //I guess the way to write it is like, starting a roll on one of the things adds a waiter token (int or something)
    //and the callback retracts -1 from it. 
    //and checks if its 0 and if it is, then do Choice View

    // Creates a button showing the choice text
    void CreateChoiceView()
    {
        buttonEnableCounter--;
        if(buttonEnableCounter > 0)
        {
            return;
        }


        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < CurrentDialogue.currentChoices.Count)
            {
                buttons[i].gameObject.SetActive(true);
                buttonTexts[i].text = CurrentDialogue.currentChoices[i].text.Trim();
                int istore = i;
                buttons[i].onClick.AddListener(delegate 
                {
                    OnClickChoiceButton(CurrentDialogue.currentChoices[istore]);
                });
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
    }


    TextInfo ConvertCommentToTextInfo(string txt, Ink.Runtime.Story inkStory, Place place)
    {
        TextInfo text = new TextInfo();

        text.rolldelay = GlobalVariables.TextRollDelay; //needs to be setup! :D
        text.startdelay = GlobalVariables.TextStartDelay;

        if(inkStory.currentTags.Count > 0) //getting delay and speed from tags in ink story
        {
            for (int i = 0; i < inkStory.currentTags.Count; i++)
            {
                string[] split = inkStory.currentTags[i].Split('=');
                if (inkStory.currentTags[i].Contains(variableIdentifiers[Id.Speed]))
                {
                    text.rolldelay = float.Parse(split[1]);
                }
                if (inkStory.currentTags[i].Contains(variableIdentifiers[Id.Delay]))
                {
                    text.startdelay = float.Parse(split[1]);
                }
                if(inkStory.currentTags[i].Contains(variableIdentifiers[Id.GlitchTiming]))
                {
                    Glitch.instance.ChangeTiming(split[1]);
                }
                if (inkStory.currentTags[i].Contains(variableIdentifiers[Id.GlitchCommand]))
                {
                    Glitch.instance.GlitchScreenOnCommand(float.Parse(split[1]));
                }
                if (inkStory.currentTags[i].Contains(variableIdentifiers[Id.SoundPlay]))
                {
                    Sound.instance.HandleSoundPlayFromScript(split[1]);
                }
                if (inkStory.currentTags[i].Contains(variableIdentifiers[Id.SoundStop]))
                {
                    Sound.instance.HandleSoundStopFromScript(split[1]);
                }
            }
        }
        
        text.text = txt;

        return text;
    }


    Dictionary<Id, string> variableIdentifiers = new Dictionary<Id, string>()
    {
        {
            Id.Speed, "S="
        },
        {
            Id.Delay, "D="
        },
        {
            Id.GlitchTiming, "GT="
        },
        {
            Id.GlitchCommand, "GC="
        },
        {
            Id.SoundPlay, "SP="
        },
        {
            Id.SoundStop, "ST="
        }

    };
    


    void ClearText()
    {
        mainText.text = string.Empty;
        thoughtsText.text = string.Empty;
        ambientThoughts.text = string.Empty;
        ambientText.text = string.Empty;
        

        for (int i = 0; i < buttons.Length; i++)
        {
            buttonTexts[i].text = string.Empty;

            buttons[i].onClick.RemoveAllListeners();
            buttons[i].gameObject.SetActive(false);
        }
    }

    void Setup()
    {
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

}
