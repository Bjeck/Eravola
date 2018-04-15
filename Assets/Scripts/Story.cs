using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script manages the story on a large scale - which part of the story we're in. who we're following, and has links to stored data, so we know what has happened before this.
/// </summary>
public class Story : MonoBehaviour {

    public Sequences sequences;
    

	// Use this for initialization
	void Start () {
       // sequences.RunSequence(SequenceName.BootUp);
    }


    public void ChangeStoryPoint(string currentStoryPoint) //??
    {
        //this needs to be able to take any point (presumably, an end point) and proceed from there, and find out how to continue. Either jump out to drone part or skip to other person or something. yes?
        //don't mind if it's a little jumpy. glitches make that work wonderfully.

        //need to store the node we got to and reload from that (I'm preeetty sure we can do that in VIDE). DialogueName & nodeID should be enough.
    }







}
