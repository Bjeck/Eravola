using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Sound))]
public class SoundEditor : Editor
{

    Sound t;
    SerializedObject GetTarget;
    SerializedProperty SFX;
    SerializedProperty SFXList;
    SerializedProperty Ambiences;

    SerializedProperty masterMixer;
    SerializedProperty masterMixerGroup;
    SerializedProperty glitchMixer;

    SerializedProperty source;
    SerializedProperty glitchSounds;

    int ListSize;

    void OnEnable()
    {
        t = (Sound)target;
        GetTarget = new SerializedObject(t);
        SFX = GetTarget.FindProperty("serialSFX");
        SFXList = GetTarget.FindProperty("serialSFXList");
        Ambiences = GetTarget.FindProperty("serialAmbience");

        masterMixerGroup = GetTarget.FindProperty("masterMixerGroup");
        masterMixer = GetTarget.FindProperty("masterMixer");
        glitchMixer = GetTarget.FindProperty("glitchMixer");
        source = GetTarget.FindProperty("sources");
        glitchSounds = GetTarget.FindProperty("glitchSounds");


    }

    public override void OnInspectorGUI()
    {
        //Update our list
        GetTarget.Update();

        EditorGUILayout.PropertyField(masterMixer);
        EditorGUILayout.PropertyField(masterMixerGroup);
        EditorGUILayout.PropertyField(glitchMixer);
        EditorGUILayout.PropertyField(source);


        EditorGUIUtility.labelWidth = 60;

        




        EditorGUILayout.PropertyField(glitchSounds, true);




        EditorGUILayout.LabelField(" -------------- SFX ---------------- ");
        //Resize our list
        ListSize = SFX.arraySize;

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
        

        EditorGUILayout.BeginHorizontal();
        ListSize = EditorGUILayout.IntField("List Size", ListSize);
        if (GUILayout.Button("Add New"))
        {
            t.serialSFX.Add(new SFXInfo());
        }
        EditorGUILayout.EndHorizontal();


        EditorList.Show(SFX);


        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();


        EditorGUILayout.LabelField(" ------------ SFX LIST -------------- ");
        //Resize our list
        ListSize = SFXList.arraySize;


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

        EditorGUILayout.BeginHorizontal();
        ListSize = EditorGUILayout.IntField("List Size", ListSize);
        if (GUILayout.Button("Add New"))
        {
            t.serialSFXList.Add(new SFXListInfo());
        }
        EditorGUILayout.EndHorizontal();

        EditorList.ShowListInList(SFXList);



        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();


        EditorGUILayout.LabelField(" ----------- AMBIENCES ----------- ");
        //Resize our list
        ListSize = Ambiences.arraySize;

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
        
        EditorGUILayout.BeginHorizontal();
        ListSize = EditorGUILayout.IntField("List Size", ListSize);
        if (GUILayout.Button("Add New"))
        {
            t.serialAmbience.Add(new AmbienceInfo());
        }
        EditorGUILayout.EndHorizontal();



        EditorList.Show(Ambiences);





        //Apply the changes to our list
        GetTarget.ApplyModifiedProperties();
    }
}