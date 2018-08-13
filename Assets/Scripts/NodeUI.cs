using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeUI : MonoBehaviour {

    public Story story;

    public TextMeshProUGUI text;

    public Button mapButton;
    public Button goInButton;

    public List<Button> buttons;
    public List<TextMeshProUGUI> texts;

    public GameObject mapObject;

    public NodeGraph nodegraph;


	// Use this for initialization
	void Start () {
        mapButton.onClick.AddListener(() => ToggleMap());
	}
	

    public void LoadNodeSpace()
    {
        nodegraph.LoadNodeGraph();
        if (!string.IsNullOrEmpty(Storage.CurrentNodeID))
        {
            LoadNode(Storage.CurrentNodeID);
        }
        else
        {
            LoadNode(GlobalVariables.DefaultStartNode);
        }
    }


    public void LoadNode(string nodeID)
    {
        Node node = nodegraph.currentNodes.Find(x => x.nodeIam.ID == nodeID).nodeIam;

        Clear();
        if(node == null)
        {
            Debug.LogError("Couldn't Find Node " + nodeID + "... Spelling error?");
            return;
        }

        for (int i = 0; i < node.optionsFromMe.Count; i++)
        {
            int temp = i;
            buttons[temp].gameObject.SetActive(true);
            buttons[temp].onClick.AddListener(() => GoToNode(node.optionsFromMe[temp]));
            texts[temp].text = "Go To " + node.optionsFromMe[temp].name;
            text.text = node.text;
        }

        if(node.simulationTarget != CharacterNames.None)
        {
            goInButton.gameObject.SetActive(true);
            goInButton.onClick.AddListener(() => story.ChangeStoryPoint(node.simulationTarget));
        }



        if (mapObject.activeInHierarchy)
        {
            ToggleMap();
        }
    }

    void Clear()
    {
        text.text = "";

        foreach (Button b in buttons)
        {
            b.gameObject.SetActive(false);
            b.onClick.RemoveAllListeners();
        }
        foreach (TextMeshProUGUI t in texts)
        {
            t.text = "";
        }   
        goInButton.gameObject.SetActive(false);
        goInButton.onClick.RemoveAllListeners();
    }

    void GoToNode(Node node)
    {
        LoadNode(node.ID);
        Storage.CurrentNodeID = node.ID;
    }

    public void ToggleMap()
    {
        mapObject.SetActive(!mapObject.activeInHierarchy);
    }

}
