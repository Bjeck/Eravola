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

    public Canvas currentCanvas;

    public Canvas mainCanvas;
    public Canvas bootCanvas;
    public Canvas databaseCanvas;
    public Canvas worldCanvas;

    public Database database;
    

	// Use this for initialization
	void Start () {


	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if(currentCanvas == worldCanvas)
            {
                SetSoleCanvas(mainCanvas);
                Glitch.instance.EnableAllEffects();
                Sound.instance.SetMasterVolume(0);
            }
            else
            {
                SetSoleCanvas(worldCanvas);
                Glitch.instance.DisableAllEffects();
                Sound.instance.SetMasterVolume(-80);
            }
            
        }
    }

    public void OpenDatabase()
    {
        EnableCanvas(databaseCanvas);
        eventSys.SetSelectedGameObject(null);
        database.ShowDatabase();
    }
	


    public void SetSoleCanvas(Canvas c)
    {
        currentCanvas = c;
        mainCanvas.enabled = false;
        bootCanvas.enabled = false;
        databaseCanvas.enabled = false;
        worldCanvas.enabled = false;

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
        if (worldCanvas == c)
        {
            worldCanvas.enabled = true;
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
        if (worldCanvas == c)
        {
            worldCanvas.enabled = true;
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
        if (worldCanvas == c)
        {
            worldCanvas.enabled = false;
        }
    }

}
