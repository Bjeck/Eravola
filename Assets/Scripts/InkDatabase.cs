using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to manage and get ink story sheets from the database.
/// </summary>
public static class InkDatabase
{
    private static Dictionary<string, TextAsset> stories = new Dictionary<string, TextAsset>();

    private static void Awake()
    {
        TextAsset[] textAssets = Resources.LoadAll<TextAsset>("Ink/Stories/");

        for (int i = 0; i < textAssets.Length; i++)
        {
            stories.Add(textAssets[i].name, textAssets[i]);
        }
    }


    public static bool Contains(string name)
    {
        if (stories.Count == 0)
        {
            Awake();
        }

        return stories.ContainsKey(name);
    }

    public static TextAsset Get(string name)
    {


        if (Contains(name))
        {
            return stories[name];
        }
        else
        {
            Debug.LogError("Story " + name + " wasn't found. You made a mistake or something?");
            return null;
        }
    }
}
