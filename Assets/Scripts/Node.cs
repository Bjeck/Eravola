using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Node for the Drone Sections
/// </summary>
[System.Serializable]
[CreateAssetMenu]
public class Node : ScriptableObject
{
    public string ID;
    public Flag requiredFlag;
    public bool isBound = false;
    public Vector2 coordinates;
    public Node nodeToBindTo;
    public List<Node> optionsFromMe = new List<Node>();

    public string text;

    public string simulationTarget;

    public System.Action OnEnter;
    public System.Action OnExit;
}
