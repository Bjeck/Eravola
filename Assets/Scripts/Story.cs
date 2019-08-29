using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// This script manages the story on a large scale - which part of the story we're in. who we're following, and has links to stored data, so we know what has happened before this.
/// </summary>
public class Story : MonoBehaviour
{
    [SerializeField] private Flags defaultflags; //This is editortime

    public List<Flag> flags; //This is runtime

    public Sequences sequences;
    public UIManager ui;
    public Dialogues dia;
    public InkDialogues inkDia;
    public NodeUI nodemap;

    public bool startStoryOnStartup = true;
    public bool startAtDrone = false;
    public bool debugSkipToStory = true;

    public static bool forcingAllowed = true;

    private List<string> characterNames = new List<string>();

    string startDialogue = "";
    string startNode = "START";

#if UNITY_EDITOR

    public void LoadEditorValues()
    {
        print("loading editorvalues");
        debugSkipToStory = EditorPrefs.GetBool("EnableDebug");
        startDialogue = EditorPrefs.GetString("StartDialogue");
        startNode = EditorPrefs.GetString("StartNode");
        forcingAllowed = EditorPrefs.GetBool("ForcingAllowed");
        startAtDrone = EditorPrefs.GetBool("StartAtDrone");
    }
    
#endif

    void LoadFlagsOnStartUp()
    {
        foreach(Flag f in defaultflags.flags)
        {
            if (f.LoadOnStartup)
            {
                flags.Add(f);
            }
        }
    }

    void LoadCharacterNamesOnStartUp()
    {
        List<string> strings = new List<string>();

        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var subclasses = from assembly in assemblies
                            from type in assembly.GetTypes()
                            where type.Name == "CharacterNames"
                            select type;

        foreach (Type subclass in subclasses)
        {
            var IDs = subclass.GetFields().Where((f) => f.IsLiteral && !f.IsInitOnly);

            foreach (var property in IDs)
            {
                strings.Add(property.GetRawConstantValue() as string);
            }
        }
        characterNames = strings;
    }


    // Use this for initialization
    void Start()
    {
        LoadFlagsOnStartUp();
        LoadCharacterNamesOnStartUp();

        Sound.instance.PlayAmbient(Sound.AMBIENCES.Room);

#if UNITY_EDITOR
        LoadEditorValues();
#endif
        // 
        if (startStoryOnStartup)
        {
            if (debugSkipToStory)
            {
                Sound.instance.Play(Sound.SFXIDS.Boot);
                Sound.instance.PlayAmbient(Sound.AMBIENCES.Computer);

                if (startAtDrone)
                {
                    ChangeStoryPoint(GlobalVariables.DRONE, false);
                }
                else
                {
                    ChangeStoryPoint(startDialogue, false);
                }
            }
            else
            {
                ui.SetSoleCanvas(UIManager.CanvasType.Boot);
                sequences.RunSequence(SequenceName.BootUp, () => { ChangeStoryPoint(GlobalVariables.DefaultStartDialogue); ui.SetSoleCanvas(UIManager.CanvasType.Main); });
            }
        }
    }


    public void ChangeStoryPoint(string newStoryPoint, bool overrideSequencing = false) //??
    {
        string previousStoryPoint = Storage.CurrentStoryPoint;
        Storage.CurrentStoryPoint = newStoryPoint;

        Debug.Log("Loading new story point: " + newStoryPoint);


        //If it is an intro and we are coming from drone, then load character intro.
        //if (newStoryPoint.Contains("Intro"))
        //{
        //    string storypoint = newStoryPoint.Replace("_Intro", "");
        //    if (characterNames.Exists(x => x == storypoint))
        //    {
        //        //Story point is a character name! That means we should probably find the intro story for that character and run that! First check if we're coming from Drone to handle through sequence.
        //        if (overrideSequencing)
        //        {
        //            ui.SetSoleCanvas(UIManager.CanvasType.Main);
        //            inkDia.StartDialogue(newStoryPoint, startNode);
        //        }
        //        else
        //        {
        //            sequences.RunSequence(SequenceName.LoadToStoryFromDrone, () =>
        //            {
        //                ui.SetSoleCanvas(UIManager.CanvasType.Main);
        //                inkDia.StartDialogue(newStoryPoint, startNode);
        //            });
        //        }
        //        return;
        //    }
        //}


        //load story point normally if it exists in database.
        if (InkDatabase.Contains(newStoryPoint))
        {
            //it's a dialogue. let's assume we should start this dialogue
            if (overrideSequencing)
            {
                ui.SetSoleCanvas(UIManager.CanvasType.Main);        //TODO: This is ugly.
                inkDia.StartDialogue(newStoryPoint, startNode);
            }
            else
            {
                sequences.RunSequence(SequenceName.LoadToStoryFromDrone, () =>
                {
                    ui.SetSoleCanvas(UIManager.CanvasType.Main);
                    inkDia.StartDialogue(newStoryPoint, startNode);
                });
            }
            return;
        }


        //DO DRONE SHIT
        if (newStoryPoint == GlobalVariables.DRONE)
        {
            if (overrideSequencing)
            {
                ui.SetSoleCanvas(UIManager.CanvasType.Drone);
                nodemap.LoadNodeSpace();
                Sound.instance.PlayAmbient(Sound.AMBIENCES.Drone);
                Glitch.instance.EnableDroneEffects();
            }
            else
            {
                sequences.RunSequence(SequenceName.LoadToDrone, () =>
                {
                    ui.SetSoleCanvas(UIManager.CanvasType.Drone);
                    nodemap.LoadNodeSpace();
                });
            }
            return;
        }


        //Throw error if the change didn't actually work.
        Debug.LogError(newStoryPoint + " Didn't result in any story point. Soemthing went wrong.");
    }

}
