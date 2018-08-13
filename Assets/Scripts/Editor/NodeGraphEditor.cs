using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(EditorNodeGraph))]
public class NodeGraphEditor : Editor
{    
    EditorNodeGraph nodegraph;
    SerializedObject GetTarget;
    SerializedProperty editorNodes;
    [SerializeField] NodeGraphObject savedNodes;

    int listSize;

    private void OnEnable()
    {
        nodegraph = (EditorNodeGraph)target;
        GetTarget = new SerializedObject(nodegraph);
        editorNodes = GetTarget.FindProperty("allNodesInEditor");

        //nodegraph.allNodesInEditor
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Save Nodes"))
        {
            //save data on nodes
            nodegraph.allNodesInEditor = nodegraph.GetComponentsInChildren<NodeBehaviour>().ToList();
            foreach(NodeBehaviour nb in nodegraph.allNodesInEditor)
            {
                nb.nodeIam.coordinates = nb.GetComponent<RectTransform>().anchoredPosition;
                nb.nodeIam.ID = nb.nodeIam.name;
            }
        }

    }
}

