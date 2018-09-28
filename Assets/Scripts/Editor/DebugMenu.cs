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
        EditorPrefs.SetInt("StartNode", nodeID);
        EditorPrefs.SetBool("ForcingAllowed", forcingAllowed);
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical("Box");

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Enable Debug?");
        enableDebug = EditorGUILayout.Toggle(enableDebug);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Forcing Allowed?");
        forcingAllowed = EditorGUILayout.Toggle(forcingAllowed);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Start At Drone");
        startAtDrone = EditorGUILayout.Toggle(startAtDrone);
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

        Debug.Log("pop");
        if (!dialogues.Contains(dialogueNodeToStart))
        {
            Debug.Log("returning");
            return;
        }

        TextAsset file = Resources.Load<TextAsset>("Dialogues/"+dialogueNodeToStart);
        //json it into object

        object obj = MiniJSON_VIDE.DiagJson.Deserialize(file.text);
        Dictionary<string, object> dict = obj as Dictionary<string, object>;

        int actionnodes = ToInt(dict["actionNodes"]);
        int playernodes = ToInt(dict["playerDiags"]);
        int totalNodes = actionnodes + playernodes;
        defaultStartNode = ToInt(dict["startPoint"]);
        

        Dictionary<int, string> unsortednodeTexts = new Dictionary<int, string>();


        for (int i = 0; i < playernodes; i++)
        {
            bool player = (bool)dict["pd_isp_" + i];

            string p = player ? "Player" : "NPC";

            int idx = ToInt(dict["pd_ID_" + i]);

            string text = idx + " " + p + ": " + (string)dict["pd_" + i + "_com_0text"];
            unsortednodeTexts.Add(idx, text );
        }

        for (int i = 0; i < actionnodes; i++)
        {
            int idx = ToInt(dict["ac_ID_" + i]);
            string text = idx + " ACTION: " + (string)dict["ac_meth_" + i];
            unsortednodeTexts.Add(idx, text);
        }


        List<int> ids = unsortednodeTexts.Keys.ToList();

        ids.Sort();

        for (int i = 0; i < ids.Count; i++)
        {
            nodeTexts.Add(ids[i], unsortednodeTexts[ids[i]]);
        }
        


        //read objects node counts

        nodeID = defaultStartNode;

    }


    int ToInt(object obj)
    {
        return (int)((System.Int64)obj);
    }


    public void LoadFiles()
    {
        AssetDatabase.Refresh();


        TextAsset[] files = Resources.LoadAll<TextAsset>("Dialogues");
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
