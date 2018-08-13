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

    public enum CanvasType { Main, Boot, Database, Drone, World }

    public TextRoll roll;

    public EventSystem eventSys;

    public CanvasType currentCanvas;

    public Dictionary<CanvasType, Canvas> canvases = new Dictionary<CanvasType, Canvas>();

    [SerializeField] Canvas mainCanvas;
    [SerializeField] Canvas bootCanvas;
    [SerializeField] Canvas databaseCanvas;
    [SerializeField] Canvas worldCanvas;
    [SerializeField] Canvas droneCanvas;

    public Database database;
    

	// Use this for initialization
	void Start () {

        canvases.Add(CanvasType.Main, mainCanvas);
        canvases.Add(CanvasType.Boot, bootCanvas);
        canvases.Add(CanvasType.Database, databaseCanvas);
        canvases.Add(CanvasType.Drone, droneCanvas);
        canvases.Add(CanvasType.World, worldCanvas);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if(currentCanvas == CanvasType.World)
            {
                SetSoleCanvas(CanvasType.Main);
                Glitch.instance.EnableAllEffects();
                Sound.instance.SetMasterVolume(0);
            }
            else
            {
                SetSoleCanvas(CanvasType.World);
                Glitch.instance.DisableAllEffects();
                Sound.instance.SetMasterVolume(-80);
            }
            
        }
    }

    public void OpenDatabase()
    {
        EnableCanvas(CanvasType.Database);
        eventSys.SetSelectedGameObject(null);
        database.ShowDatabase();
    }
	


    public void SetSoleCanvas(CanvasType c)
    {
        currentCanvas = c;
        foreach(CanvasType ct in canvases.Keys)
        {
            if(ct != c)
            {
                canvases[ct].enabled = false;
            }
        }
        canvases[c].enabled = true;
    }

    public void EnableCanvas(CanvasType c)
    {
        canvases[c].enabled = true;
    }

    public void DisableCanvas(CanvasType c)
    {
        canvases[c].enabled = false;
    }

}
