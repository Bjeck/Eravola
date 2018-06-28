using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Node for the Drone Sections
/// </summary>
public class Node : MonoBehaviour {

    public Flag requiredFlag;
    public bool isBound = false;
    public Node nodeToBindTo; //used for setup.
    public List<Node> optionsFromMe = new List<Node>();

    public System.Action OnEnter;
    public System.Action OnExit;

}
