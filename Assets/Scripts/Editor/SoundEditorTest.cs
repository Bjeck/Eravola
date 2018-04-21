using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Sound))]

    //RIIGHT. last thing that bugs now is the SFX List since it doesn't show the Audio Array properly. I'll deal with taht tomorroroow!!!!

public class SoundEditorTest : Editor
{

    Sound t;
    SerializedObject GetTarget;
    SerializedProperty SFX;
    SerializedProperty SFXList;
    SerializedProperty Ambiences;

    int ListSize;

    void OnEnable()
    {
        t = (Sound)target;
        GetTarget = new SerializedObject(t);
        SFX = GetTarget.FindProperty("serialSFX"); // Find the List in our script and create a refrence of it
        SFXList = GetTarget.FindProperty("serialSFXList"); // Find the List in our script and create a refrence of it
        Ambiences = GetTarget.FindProperty("serialAmbience"); // Find the List in our script and create a refrence of it

         
    }

    public override void OnInspectorGUI()
    {
        //Update our list

        GetTarget.Update();

        EditorGUILayout.LabelField("SFX");
        //Resize our list
        ListSize = SFX.arraySize;
        ListSize = EditorGUILayout.IntField("List Size", ListSize);

        if (ListSize != SFX.arraySize)
        {
            while (ListSize > SFX.arraySize)
            {
                SFX.InsertArrayElementAtIndex(SFX.arraySize);
            }
            while (ListSize < SFX.arraySize)
            {
                SFX.DeleteArrayElementAtIndex(SFX.arraySize - 1);
            }
        }
        //Or add a new item to the List<> with a button
        EditorGUILayout.LabelField("Add a new item with a button");

        if (GUILayout.Button("Add New"))
        {
            t.serialSFX.Add(new SFXInfo());
        }

        EditorList.Show(SFX);


        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();


        EditorGUILayout.LabelField("SFX LIST");
        //Resize our list
        ListSize = SFXList.arraySize;
        ListSize = EditorGUILayout.IntField("List Size", ListSize);

        if (ListSize != SFXList.arraySize)
        {
            while (ListSize > SFXList.arraySize)
            {
                SFXList.InsertArrayElementAtIndex(SFXList.arraySize);
            }
            while (ListSize < SFXList.arraySize)
            {
                SFXList.DeleteArrayElementAtIndex(SFXList.arraySize - 1);
            }
        }
        //Or add a new item to the List<> with a button
        EditorGUILayout.LabelField("Add a new item with a button");

        if (GUILayout.Button("Add New"))
        {
            t.serialSFX.Add(new SFXInfo());
        }

        EditorList.Show(SFXList);



        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();


        EditorGUILayout.LabelField("AMBIENCES");
        //Resize our list
        ListSize = Ambiences.arraySize;
        ListSize = EditorGUILayout.IntField("List Size", ListSize);

        if (ListSize != Ambiences.arraySize)
        {
            while (ListSize > Ambiences.arraySize)
            {
                Ambiences.InsertArrayElementAtIndex(Ambiences.arraySize);
            }
            while (ListSize < Ambiences.arraySize)
            {
                Ambiences.DeleteArrayElementAtIndex(Ambiences.arraySize - 1);
            }
        }
        //Or add a new item to the List<> with a button
        EditorGUILayout.LabelField("Add a new item with a button");

        if (GUILayout.Button("Add New"))
        {
            t.serialSFX.Add(new SFXInfo());
        }

        EditorList.Show(Ambiences);





        //Apply the changes to our list
        GetTarget.ApplyModifiedProperties();
    }
}