using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

[CustomPropertyDrawer(typeof(DRAWERTESTER))]
public class TestDrawer : PropertyDrawer
{
    SerializedProperty boo;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        property.serializedObject.Update();

        boo = property.FindPropertyRelative("boo");

        var popuprect = new Rect(position.x + 150, position.y, 100, position.height);

        boo.boolValue = EditorGUI.Toggle(popuprect, boo.boolValue);

        property.serializedObject.ApplyModifiedProperties();
        EditorGUI.EndProperty();

    }
}

