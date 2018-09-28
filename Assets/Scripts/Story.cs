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
    public NodeUI nodemap;

    public bool startStoryOnStartup = true;
    public bool startAtDrone = false;
    public bool debugSkipToStory = true;

    public static bool forcingAllowed = true;

    private List<string> characterNames = new List<string>();

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
                    ChangeStoryPoint(GlobalVariables.DRONE);
                }
                else
                {
                    ChangeStoryPoint(startDialogue);
                }
            }
            else
            {
                ui.SetSoleCanvas(UIManager.CanvasType.Boot);
                sequences.RunSequence(SequenceName.BootUp, () => { ChangeStoryPoint(GlobalVariables.DefaultStartDialogue); ui.SetSoleCanvas(UIManager.CanvasType.Main); });
            }
        }
    }


    public void ChangeStoryPoint(string newStoryPoint) //??
    {
        string previousStoryPoint = Storage.CurrentStoryPoint;
        Storage.CurrentStoryPoint = newStoryPoint;

        if (dia.DoesDialogueExist(newStoryPoint))
        {
            //it's a dialogue. let's assume we should start this dialogue

            ui.SetSoleCanvas(UIManager.CanvasType.Main);
            dia.LoadDialogue(newStoryPoint);
            return;
        }

        if(characterNames.Exists(x=>x == newStoryPoint))
        {
            //Story point is a character name! That means we should probably find the intro story for that character and run that! First check if we're coming from Drone to handle through sequence.
            if(previousStoryPoint == GlobalVariables.DRONE)
            {
                sequences.RunSequence(SequenceName.LoadToStoryFromDrone, () => { ChangeStoryPoint((newStoryPoint + "_Intro")); });
            }
            else
            {
                ChangeStoryPoint((newStoryPoint + "_Intro")); //I don't really see how this should happen, but it might!
                Sound.instance.StopAmbient(Sound.AMBIENCES.Drone);
            }
            return;
        }

        if(newStoryPoint == GlobalVariables.DRONE)
        {
            //DO DRONE SHIT
            ui.SetSoleCanvas(UIManager.CanvasType.Drone);
            nodemap.LoadNodeSpace();
            Sound.instance.PlayAmbient(Sound.AMBIENCES.Drone);
            Glitch.instance.EnableDroneEffects();
            return;
        }


        //this needs to be able to take any point (presumably, an end point) and proceed from there, and find out how to continue. Either jump out to drone part or skip to other person or something. yes?
        //don't mind if it's a little jumpy. glitches make that work wonderfully.

        //need to store the node we got to and reload from that (I'm preeetty sure we can do that in VIDE). DialogueName & nodeID should be enough.


        Storage.CurrentStoryPoint = previousStoryPoint; //revert back if the change didn't actually work.
        Debug.LogError(newStoryPoint + " Didn't result in any story point. Soemthing went wrong.");

    }







}
