using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;
using System;

[CustomEditor(typeof(Node))]
public class NodeEditor : Editor
{

    Node node;
    SerializedObject GetTarget;
    //    SerializedProperty editorNodes;

    List<string> characterNames;
    SerializedProperty flag;
    SerializedProperty nodeToBindTo;
    SerializedProperty text;
    SerializedProperty simulationTarget;

    int stringID;

    private void OnEnable()
    {
        node = (Node)target;
        GetTarget = new SerializedObject(node);
        characterNames = GetCharacterNames();
        flag = GetTarget.FindProperty("requiredFlag");
        nodeToBindTo = GetTarget.FindProperty("nodeToBindTo");
        text = GetTarget.FindProperty("text");
        simulationTarget = GetTarget.FindProperty("simulationTarget");


    }

    public override void OnInspectorGUI()
    {
        //GetTarget.Update();
        characterNames = GetCharacterNames();
        EditorGUILayout.PropertyField(flag);

        EditorGUILayout.Space();

        EditorGUILayout.Vector2Field("Coordinates: ",node.coordinates);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(nodeToBindTo);

        text.stringValue = EditorGUILayout.TextArea(text.stringValue);

        if (!string.IsNullOrEmpty(simulationTarget.stringValue))
        {
            stringID = characterNames.IndexOf(simulationTarget.stringValue);
        }
        stringID = EditorGUILayout.Popup(stringID, characterNames.ToArray());
        simulationTarget.stringValue = characterNames[stringID];

        GetTarget.ApplyModifiedProperties();

    }


    private List<string> GetCharacterNames()
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
        return strings;
    }

}
