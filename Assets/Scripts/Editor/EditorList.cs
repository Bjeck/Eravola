using UnityEngine;
using UnityEditor;

public static class EditorList {

    public static void Show(SerializedProperty list)
    {
        EditorGUILayout.PropertyField(list);
        EditorGUI.indentLevel += 1;
        if (list.isExpanded)
        {
           //EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
            for (int i = 0; i < list.arraySize; i++)
            {
                SerializedProperty MyListRef = list.GetArrayElementAtIndex(i);
                SerializedProperty ID = MyListRef.FindPropertyRelative("ID");
                SerializedProperty audio = MyListRef.FindPropertyRelative("audio");
                SerializedProperty volume = MyListRef.FindPropertyRelative("volume");
                SerializedProperty loop = MyListRef.FindPropertyRelative("loop");
                //SerializedProperty MyArray = MyListRef.FindPropertyRelative("AnIntArray");


                // Display the property fields in two ways.

                // Choose to display automatic or custom field types. This is only for example to help display automatic and custom fields.
                //1. Automatic, No customization <-- Choose me I'm automatic and easy to setup

                EditorGUILayout.PropertyField(audio);

                EditorGUILayout.BeginHorizontal("BOX");
                EditorGUILayout.PropertyField(ID);
                EditorGUILayout.PropertyField(volume);
                EditorGUILayout.PropertyField(loop);
                EditorGUILayout.EndHorizontal();

                if (GUILayout.Button("Remove This Index (" + i.ToString() + ")"))
                {
                    list.DeleteArrayElementAtIndex(i);
                }
                EditorGUILayout.Space();
            }
        }
        EditorGUI.indentLevel -= 1;
    }
}
