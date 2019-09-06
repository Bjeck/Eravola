using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using System.Linq;

public class DebugMenu : EditorWindow {
    [MenuItem ("Debug/Story Debug Menu")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(DebugMenu));
    }

    Story story;

    List<string> dialogues = new List<string>();

    public bool enableDebug = false;
    public bool forcingAllowed = false;
    public bool startAtDrone = false;

    SearchField searchField;
    [SerializeField]
    string searchFieldText = "";
    public bool startAtSpecificNode = false;
    public string dialogueNodeToStart = "";

    public int nodeID = 0;

    int[] nodeValuesForDiag;
    int defaultStartNode;
    Dictionary<int, string> nodeTexts = new Dictionary<int, string>();
    int nodeTextsIndex;

    bool showDialogueNameList = false;

    private void OnEnable()
    {
        if (GameObject.Find("Story"))
        {
            story = GameObject.Find("Story").GetComponent<Story>();
            LoadFiles();
            FocusSearchField();
        }
    }

    public void FocusSearchField()
    {
        searchField = searchField ?? new SearchField();
        searchField.SetFocus();
    }

    void SaveSettings()
    {
        EditorPrefs.SetBool("EnableDebug",enableDebug);
        EditorPrefs.SetBool("StartAtDrone", startAtDrone);
        EditorPrefs.SetString("StartDialogue", dialogueNodeToStart);
        if(nodeTexts.Values.ToArray().Length > 0)
        {
            EditorPrefs.SetString("StartNode", nodeTexts.Values.ToArray()[nodeID]);
        }
        else
        {
            EditorPrefs.SetString("StartNode", "START");
        }
        EditorPrefs.SetBool("ForcingAllowed", forcingAllowed);
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical("Box");

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Enable Debug?");
        enableDebug = EditorGUILayout.Toggle(enableDebug, GUILayout.Width(350));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Forcing Allowed?");
        forcingAllowed = EditorGUILayout.Toggle(forcingAllowed, GUILayout.Width(350));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Start At Drone");
        startAtDrone = EditorGUILayout.Toggle(startAtDrone, GUILayout.Width(350));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        
        bool prev = startAtSpecificNode;

        startAtSpecificNode = EditorGUILayout.Toggle("Begin at node: ", startAtSpecificNode);
        if(!enableDebug && startAtSpecificNode && !prev)
        {
            enableDebug = startAtSpecificNode;
        }
        //nodeID = EditorGUILayout.IntPopup(nodeID);

        int prevnod = nodeID;
        nodeID = EditorGUILayout.Popup(nodeID, nodeTexts.Values.ToArray());
       // nodeID = nodeTexts[nodeTextsIndex];

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Starting on node: " + dialogueNodeToStart);

        if (searchField == null) searchField = new SearchField();
        searchFieldText = searchField.OnGUI(searchFieldText);

        if (!string.IsNullOrEmpty(searchFieldText))
        {
            showDialogueNameList = true;
        }


        // EditorGUILayout.EndHorizontal();

        showDialogueNameList = EditorGUILayout.Foldout(showDialogueNameList,"Dialogues");

        if (showDialogueNameList)
        {
            EditorGUILayout.BeginVertical();
            foreach (string s in dialogues)
            {
                string lowerCaseDialogueName = s.ToLower();
                string lowercaseSearchTerm = searchFieldText.ToLower();

                if (lowerCaseDialogueName.Contains(lowercaseSearchTerm)) //This also works if the search field is empty.
                {
                    string displayName = s;

                    if (!string.IsNullOrEmpty(searchFieldText))
                    {
                        int searchTermIndex = lowerCaseDialogueName.IndexOf(lowercaseSearchTerm, System.StringComparison.CurrentCulture);
                        displayName = displayName.Insert(searchTermIndex, "<b>").Insert(searchTermIndex + 3 + searchFieldText.Length, "</b>");
                    }
                    
                    if (GUILayout.Button(s))
                    {
                        dialogueNodeToStart = s;
                        startAtSpecificNode = true;
                        SaveSettings();

                        if (!enableDebug)
                        {
                            enableDebug = true;
                        }
                        PopulateNodeIDs();
                    }
                }            }
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndVertical();

        SaveSettings();
    }


    public void PopulateNodeIDs()
    {
        nodeTexts.Clear();
        
        if (!dialogues.Contains(dialogueNodeToStart))
        {
            Debug.Log("returning");
            return;
        }

        TextAsset file = Resources.Load<TextAsset>("Ink/Stories/" + dialogueNodeToStart);
        //json it into object

        Dictionary<string, object> rootObject = Ink.Runtime.SimpleJson.TextToDictionary(file.text);


        var list = rootObject["root"] as List<object>;
        Dictionary<string, object> knots = (list)[2] as Dictionary<string, object>;
        
        List<string> texts = list[0] as List<string>;
        nodeTexts.Add(0, "START");

        Dictionary<int, string> sortedKnots = new Dictionary<int, string>();

        List<string> keys = knots.Keys.ToList();

        for (int i = 0; i < keys.Count - 1; i++) // -1 because the last knot in Ink is apparently always #f (????)
        {
            nodeTexts.Add(i + 1, keys[i]);
        }

        defaultStartNode = 0;
        nodeID = defaultStartNode;

    }


    int ToInt(object obj)
    {
        return (int)((System.Int64)obj);
    }


    public void LoadFiles()
    {
        AssetDatabase.Refresh();


        TextAsset[] files = Resources.LoadAll<TextAsset>("Ink/Stories");
        dialogues = new List<string>();

        if (files.Length < 1) return;

        foreach (TextAsset f in files)
        {
            dialogues.Add(f.name);
//            fullPaths.Add(AssetDatabase.GetAssetPath(f));
        }

        dialogues.Sort();
       


    }





    //bool HasUniqueID(int id, string[] saveNames, int currentDiag)
    //{
    //    //Retrieve all IDs
    //    foreach (string s in saveNames)
    //    {
    //        if (s == saveNames[currentDiag]) continue;

    //        if (File.Exists(Application.dataPath + "/../" + s))
    //        {
    //            Dictionary<string, object> dict = SerializeHelper.ReadFromFile(s) as Dictionary<string, object>;
    //            if (dict.ContainsKey("dID"))
    //                if (id == ((int)((long)dict["dID"])))
    //                    return false;
    //        }
    //    }
    //    return true;
    //}
}
