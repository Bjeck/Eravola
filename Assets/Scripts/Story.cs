using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// This script manages the story on a large scale - which part of the story we're in. who we're following, and has links to stored data, so we know what has happened before this.
/// </summary>
public class Story : MonoBehaviour {

    public Sequences sequences;
    public UIManager ui;
    public Dialogues dia;

    public bool startStoryOnStartup = true;

    public static bool debugSkipToStory = true;
    public static bool forcingAllowed = true;

    string startDialogue = "";
    int startNode = 0;

#if UNITY_EDITOR

    public void LoadEditorValues()
    {
        print("loading editorvalues");
        debugSkipToStory = EditorPrefs.GetBool("EnableDebug");
        startDialogue = EditorPrefs.GetString("StartDialogue");
        startNode = EditorPrefs.GetInt("StartNode");
        forcingAllowed = EditorPrefs.GetBool("ForcingAllowed");
    }
    
#endif



    // Use this for initialization
    void Start() {

#if UNITY_EDITOR
        LoadEditorValues();
#endif
        // 
        if (startStoryOnStartup)
        {
            if (debugSkipToStory)
            {
                dia.LoadDialogue(startDialogue,startNode);
                ui.SetSoleCanvas(ui.mainCanvas);
            }
            else
            {
                ui.SetSoleCanvas(ui.bootCanvas);
                sequences.RunSequence(SequenceName.BootUp, () => { ChangeStoryPoint(GlobalVariables.DefaultStartDialogue); ui.SetSoleCanvas(ui.mainCanvas); });
            }
        }
    }


    public void ChangeStoryPoint(string newStoryPoint) //??
    {

        if (dia.DoesDialogueExist(newStoryPoint))
        {
            //it's a dialogue. let's assume we should start this dialogue

            //might have to enable/disable canvases but dunno how to check that yet?
            dia.LoadDialogue(newStoryPoint);
            return;
        }
        else
        {
            Debug.LogError(newStoryPoint + "Doesn't exist in dialogue database.");
        }


        //this needs to be able to take any point (presumably, an end point) and proceed from there, and find out how to continue. Either jump out to drone part or skip to other person or something. yes?
        //don't mind if it's a little jumpy. glitches make that work wonderfully.

        //need to store the node we got to and reload from that (I'm preeetty sure we can do that in VIDE). DialogueName & nodeID should be enough.
    }







}
