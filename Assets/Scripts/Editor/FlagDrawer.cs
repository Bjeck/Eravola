using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;
using System;

[CustomPropertyDrawer(typeof(Flag))]
public class FlagDrawer : PropertyDrawer
{

    int stringID;
    List<string> strings;
    bool val;

    SerializedProperty value;
    SerializedProperty name;
    //SerializedProperty loadOnStart;

    public void OnEnable()
    {
        strings = GetFlagNames();
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        strings = GetFlagNames();
        // base.OnGUI(position, property, label);
        EditorGUI.BeginProperty(position, label, property);
        property.serializedObject.Update();

        value = property.FindPropertyRelative("Value");
        name = property.FindPropertyRelative("Name");
       // loadOnStart = property.FindPropertyRelative("LoadOnStartup");

        position = EditorGUI.PrefixLabel(position, label);
        var valuerect = new Rect(position.x - 100, position.y, 30, position.height);
        //var loadrect = new Rect(position.x - 200, position.y, 20, position.height);
        var popuprect = new Rect(position.x + 150, position.y, 100, position.height);

        //val = EditorGUI.PropertyField(valuerect,value);

        value.boolValue = EditorGUI.Toggle(valuerect, value.boolValue);

       // loadOnStart.boolValue = EditorGUI.Toggle(valuerect, value.boolValue);

        if (!string.IsNullOrEmpty(name.stringValue))
        {
            stringID = strings.IndexOf(name.stringValue);
        }
        stringID = EditorGUI.Popup(popuprect, stringID, strings.ToArray());
        name.stringValue = strings[stringID];


        property.serializedObject.ApplyModifiedProperties();
        EditorGUI.EndProperty();
    }

    private List<string> GetFlagNames()
    {
        List<string> strings = new List<string>();

        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var subclasses = from assembly in assemblies
                         from type in assembly.GetTypes()
                         where type.Name == "FlagNames"
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
	

