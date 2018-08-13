using UnityEngine;
using UnityEditor;

public static class EditorList {

    public static void Show(SerializedProperty list)
    {
        EditorGUILayout.PropertyField(list);
        EditorGUI.indentLevel += 1;
        EditorGUIUtility.labelWidth = 60;
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
                SerializedProperty mixer = MyListRef.FindPropertyRelative("mixer");

                //SerializedProperty MyArray = MyListRef.FindPropertyRelative("AnIntArray");

                EditorGUILayout.PropertyField(audio);


                EditorGUILayout.BeginHorizontal("BOX");
                EditorGUILayout.PropertyField(ID);
                EditorGUILayout.PropertyField(volume);
                EditorGUILayout.PropertyField(loop);
                EditorGUILayout.PropertyField(mixer);

                if (GUILayout.Button("X"))
                {
                    list.DeleteArrayElementAtIndex(i);
                }

                EditorGUILayout.EndHorizontal();
               
                EditorGUILayout.Space();
            }
        }
        EditorGUI.indentLevel -= 1;
    }

    public static void ShowListInList(SerializedProperty toplist)
    {
        EditorGUILayout.PropertyField(toplist);
        EditorGUI.indentLevel += 1;
        if (toplist.isExpanded)
        {

            for (int i = 0; i < toplist.arraySize; i++)
            {
                
                SerializedProperty MyListRef = toplist.GetArrayElementAtIndex(i);

                SerializedProperty ID = MyListRef.FindPropertyRelative("ID");
                EditorGUILayout.PropertyField(ID);


                SerializedProperty list = MyListRef.FindPropertyRelative("list");
                
                int innerListSize = EditorGUILayout.IntField("List Size", list.arraySize);
                if (innerListSize != list.arraySize)
                {
                    while (innerListSize > list.arraySize)
                    {
                        list.InsertArrayElementAtIndex(list.arraySize);
                    }
                    while (innerListSize < list.arraySize)
                    {
                        list.DeleteArrayElementAtIndex(list.arraySize - 1);
                    }
                }
                
                EditorGUILayout.PropertyField(list);

                EditorGUI.indentLevel += 1;
                if (list.isExpanded)
                {
                    for (int j = 0; j < list.arraySize; j++)
                    {
                        EditorGUIUtility.labelWidth = 80;

                        SerializedProperty InnerListRef = list.GetArrayElementAtIndex(j);
                        SerializedProperty audio = InnerListRef.FindPropertyRelative("audio");
                        SerializedProperty volume = InnerListRef.FindPropertyRelative("volume");
                        SerializedProperty loop = InnerListRef.FindPropertyRelative("loop");

                        EditorGUILayout.PropertyField(audio);
                        EditorGUILayout.BeginHorizontal("BOX");
                        EditorGUILayout.PropertyField(volume);
                        EditorGUILayout.PropertyField(loop);

                        if (GUILayout.Button("X"))
                        {
                            list.DeleteArrayElementAtIndex(j);
                        }

                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.Space();

                    }
                }
                EditorGUI.indentLevel -= 1;
                

                if (GUILayout.Button("Remove This Index (" + i.ToString() + ")"))
                {
                    toplist.DeleteArrayElementAtIndex(i);
                }
                EditorGUILayout.Space();
            }
        }
              EditorGUI.indentLevel -= 1;
    }
}
