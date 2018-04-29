using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalStrings {

    public static TextInfo BootUpString = new TextInfo("                                      [ERAVOLA INVESTIGATION UNIT]                      ¤¤Boot initiated. Checking Status...Done.                ¤Transmission OK. Sequence found.                 ¤Loading... Done.         ¤Accessing Data... ... ...                          ¤Done.                                       ¤¤¤Password:"
                                                        ,0.05f,0f);




}


public class TextInfo
{
    public string text;
    public float rolldelay;
    public float startdelay;

    public TextInfo(string t, float roll, float start)
    {
        text = t;
        rolldelay = roll;
        startdelay = start;
    }
}
