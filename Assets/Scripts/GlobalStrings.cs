using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalStrings {

    public static TextInfo FirstBootUpString = new TextInfo("                                      [ERAVOLA INVESTIGATION UNIT]                      ¤¤Boot initiated. Checking Status...Done.                ¤Transmission OK. Sequence found.                 ¤Loading... Done.         ¤Accessing Data... ... ...                          ¤Done.                                       ¤¤¤Password:"
                                                        ,0.05f,0f);

    public static TextInfo BootToDroneString = new TextInfo("[ERAVOLA INVESTIGATION UNIT]                      ¤¤Boot initiated. Checking Status...Done.                ¤Transmission OK. Sequence ... ... ... ... ... Not found.                                     ¤Initiating Survey Module ... ... ...                ¤Done          ¤Accessing Site Data     ¤Site: Sauddoc Village¤POI: Eravola Outbreak¤Access: Granted¤ID: JH43Q            ... ... ...                          ¤Ready for Login.                                       ¤¤¤Password:"
                                                        , 0.05f, 0f);
        
    public static TextInfo LoadingCharacterString = new TextInfo("Searching for relevant Data... ... ...         ¤¤Transmission OK. Sequence found.                 ¤Loading... ... ¤¤Done.            ", 0.04f, 0f);

    public static TextInfo BrokenNextButton = new TextInfo(": : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : :N: : : :X:T", 0.04f, 0f);

    public static TextInfo Crash = new TextInfo("----- SEQUENCE TERMINATED -----¤Invalid Argument Error: Query exceeds data string count. Exiting…¤…Done.", 0.05f, 0);

    public static TextInfo CrashLoadingDrone = new TextInfo("Attempting repair... ... ... ¤Failed.                    ¤Accessing system files           ¤3-8 Ready ¤2-7 Ready ¤1-9 Ready  ¤7-4 Ready     ¤Purging memory banks... ... ...Done       ¤Performing Clean Reboot  ... ... ... ", 0.05f, 0);

}


public class TextInfo
{
    public string text;
    public float rolldelay;
    public float startdelay;

    public TextInfo() { }

    public TextInfo(string t, float roll, float start)
    {
        text = t.Replace("¤", System.Environment.NewLine);
        rolldelay = roll;
        startdelay = start;
    }
}
