using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class NodeBehaviour : MonoBehaviour
{
    public Node nodeIam;

    public TextMeshProUGUI text;

    public TextMeshProUGUI toBindToText;


    private void OnEnable()
    {
        if(nodeIam != null)
        {
            text.text = nodeIam.name;
            toBindToText.text = nodeIam.nodeToBindTo.name;
        }
    }



}
