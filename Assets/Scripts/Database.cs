using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class Database : MonoBehaviour {

    public const int COUNTERSTART = 11;

    string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZqwertyuiopåasdfghjklæøzxcvbnm,.<1234567890+½§!#%&/()=?*<>@£$€{[]}'*/";

    int length = 2200;
    int charStartPoint = 599;

    public RectTransform canvas;
    public TextMeshProUGUI rawText;
    public RectTransform panel;

    public string readString = "";
    public string displayedString = "";
    public int charCounter = 0;

    public int amtOfCounters = 5;
        
    List<int> counters = new List<int>();

    public float time = 0.01f;
    public int charsPerTickInText = 10;
    public bool isShowingDatabase = false;
    bool isShowingChar = false;
    bool isRollingChar = false;
    bool isRollingDatabaseIntro = false;
    bool isUnrollingDatabase = false;
    string testCharDesc = "\nTall, blond, with fists of made of something fierce.\n\nHad an idea what she wanted to be when she grew up.Now she’s not so sure anymore.\nOnce told me she would eat a dog if she was hungry enough.\nGot an penchant for telling people the wrong things at the wrong time.\n\nMight be my best friend.But I don't have a lot of other competitors, so it's an easy fight. She'd probably win a fight against whomever tried, anyway.\n";

    

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        CheckInput();

        if (Input.GetKeyDown(KeyCode.Return))
        {
         //   ShowChar();
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            ForgetCharacter();
        }
	}



    public void ShowDatabase()      //Interesting problem. Unity selects the button when i press it, so pressing Enter triggers this function again...
    {
        if (isUnrollingDatabase)
        {
            return;
        }

        isShowingDatabase = true;

        for (int i = 0; i < amtOfCounters; i++)
        {
            counters.Add(Random.Range(0, length));
        }

        StartCoroutine("PopulateDatabaseString");
    }

    public void HideDatabase()
    {
        if (isRollingDatabaseIntro)
        {
            return;
        }

        isShowingDatabase = false;

        StopCoroutine("RollDatabase");
        StopCoroutine("RollCharString");

        counters.Clear();

        StartCoroutine("UnrollDatabase");
    }

    IEnumerator PopulateDatabaseString()
    {
        isRollingDatabaseIntro = true;
        print(length + " - " + displayedString.Length);

        displayedString = "<mspace=10>";

        panel.sizeDelta = new Vector2 (canvas.sizeDelta.x,0);

        while (displayedString.Length < length)
        {
            for (int i = 0; i < charsPerTickInText * 2; i++)
            {
                displayedString += chars[Random.Range(0, chars.Length)];
                rawText.text = displayedString;
            }
            
            panel.sizeDelta = new Vector2(canvas.sizeDelta.x, (((float)displayedString.Length / (float)length) * canvas.sizeDelta.y));

            yield return new WaitForEndOfFrame();
        }

        isRollingDatabaseIntro = false;
        StartCoroutine("RollDatabase");
    }

    IEnumerator UnrollDatabase()
    {
        isUnrollingDatabase = true;

        panel.sizeDelta = new Vector2(canvas.sizeDelta.x, canvas.sizeDelta.y);

        while (displayedString.Length > (charsPerTickInText*2))
        {
            for (int i = 1; i < charsPerTickInText * 2; i++)
            {
                if(displayedString.Length > charsPerTickInText * 2) //failsafe if length is below that, then it will go under 
                {
                    displayedString = displayedString.Substring(0, displayedString.Length - i);
                    rawText.text = displayedString;
                }
            }

            // print("hello");
            panel.sizeDelta = new Vector2(canvas.sizeDelta.x, (((float)displayedString.Length / (float)length) * canvas.sizeDelta.y));
            
            yield return new WaitForEndOfFrame();
        }

        panel.sizeDelta = new Vector2(canvas.sizeDelta.x, 0);
        displayedString = "";
        rawText.text = "";


        isShowingDatabase = false;
        isUnrollingDatabase = false;
    }



    IEnumerator RollDatabase()
    {
        while (isShowingDatabase)
        {
            StringBuilder stringBuilder = new StringBuilder(displayedString);

            for (int i = 0; i < amtOfCounters; i++)
            {
                if (counters[i] < charStartPoint || counters[i] > (charStartPoint + charCounter))
                {
                    stringBuilder[counters[i]] = chars[Random.Range(0, chars.Length)];
                }

                counters[i]++;
                if (counters[i] >= length)
                {
                    counters[i] = COUNTERSTART;
                }
            }
            
            displayedString = stringBuilder.ToString();
            rawText.text = displayedString;

            yield return new WaitForSeconds(time);
        }
    }
    


    void CheckInput()
    {
        if (isRollingChar || !isShowingDatabase || isRollingDatabaseIntro)
        {
            return;
        }

        if (Input.inputString != "")
        {
            if (Input.GetKeyDown(KeyCode.Backspace))    //Deleting keys
            {
                if (readString == "")
                {
                    return;
                }

                print(readString + " " + readString.Length);

                if (readString.Length > 0)
                {
                    StringBuilder strb = new StringBuilder(displayedString);
                    strb[charStartPoint + charCounter] = readString[readString.Length - 1];
                    charCounter--;
                    displayedString = strb.ToString();
                    rawText.text = displayedString;

                    readString = readString.Remove(readString.Length - 1, 1);

                    if (isShowingChar)
                    {
                        ForgetCharacter();
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return))  //checking keys
            {
                if (readString == "")
                {
                    return;
                }
                //print("TO CHECK|" + readString[0] + "|");
                CheckName(readString);
                readString = "";
            }
            else                                        //adding keys
            {
                if (!Input.GetKey(KeyCode.Backspace))   //Seems redundant. Is here because You can HOLD backspace. which is a problem.
                {
                    readString += Input.inputString;

                    print(readString);


                    StringBuilder strb = new StringBuilder(displayedString);
                    strb[charStartPoint + charCounter] = readString[readString.Length - 1];
                    charCounter++;
                    displayedString = strb.ToString();
                    rawText.text = displayedString;
                }
            }
        }
    }

    void CheckName(string s)
    {
        if(s.ToLower() == "exit")
        {
            HideDatabase();
        }

        if (s == "Ceara")   //obsv should check against a database
        {
            ShowChar();
        }
    }


    public void ForgetCharacter()
    {
        isShowingChar = false;
        charCounter = 0;
    }



    void ShowChar()
    {
        print("SHOWING CHAR");
        isShowingChar = true;
        
        //        StopCoroutine("RollDatabase");
        StartCoroutine("RollCharString");

        //stop coroutine
        //replace parts of displayed text with testCharDesc

    }


    IEnumerator RollCharString()
    {
        isRollingChar = true;
        charCounter = 0;
            
        StringBuilder strb = new StringBuilder(displayedString);


        // string temp = displayedString.Substring(charStartPoint, displayedString.Length);
        
        int iterator = 0;
        for (int i = 0; i <= (testCharDesc.Length/ charsPerTickInText); i++)
        {
            print("----- I: " + i);
            for (int j = 0; j < charsPerTickInText; j++)
            {
                print(j + "   " + (iterator + j));
                if(charCounter >= testCharDesc.Length)  //this check seems redundant but needs to be there for the last >10 characters.
                {

                }
                else
                {
                    strb[charStartPoint + (iterator + j)] = testCharDesc[(iterator + j)];
                    charCounter++;
                }
            }

            displayedString = strb.ToString();
            rawText.text = displayedString;

            yield return new WaitForSeconds(time);

            iterator += 10;
        }

        //displayedString = displayedString.Replace(displayedString[charStartPoint + charCount], testCharDesc[charCount]);

        // strb.Insert(599, curCharString);    //should be something other than insert!

        displayedString = strb.ToString();
        rawText.text = displayedString;
        isRollingChar = false;
    }



}
