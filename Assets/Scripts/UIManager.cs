using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour {

    public TextRoll roll;

    public EventSystem eventSys;

    public Canvas mainCanvas;
    public Canvas bootCanvas;
    public Canvas databaseCanvas;

    public Database database;
    

	// Use this for initialization
	void Start () {


	}



    public void OpenDatabase()
    {
        EnableCanvas(databaseCanvas);
        eventSys.SetSelectedGameObject(null);
        database.ShowDatabase();
    }
	


    public void SetSoleCanvas(Canvas c)
    {
        mainCanvas.enabled = false;
        bootCanvas.enabled = false;
        databaseCanvas.enabled = false;

        if (mainCanvas == c)
        {
            mainCanvas.enabled = true;
        }
        if (bootCanvas == c)
        {
            bootCanvas.enabled = true;
        }
        if (databaseCanvas == c)
        {
            databaseCanvas.enabled = true;
        }
    }

    public void EnableCanvas(Canvas c)
    {
        if (mainCanvas == c)
        {
            mainCanvas.enabled = true;
        }
        if (bootCanvas == c)
        {
            bootCanvas.enabled = true;
        }
        if (databaseCanvas == c)
        {
            databaseCanvas.enabled = true;
        }
    }

    public void DisableCanvas(Canvas c)
    {
        if (mainCanvas == c)
        {
            mainCanvas.enabled = false;
        }
        if (bootCanvas == c)
        {
            bootCanvas.enabled = false;
        }
        if (databaseCanvas == c)
        {
            databaseCanvas.enabled = false;
        }
    }

}
