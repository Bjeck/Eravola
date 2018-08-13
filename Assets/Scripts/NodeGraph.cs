using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class NodeGraph : MonoBehaviour
{
    public Story story;

    public NodeGraphObject nodegraph;

    public List<Node> allNodes = new List<Node>(); //should get loaded on startup

    public List<NodeBehaviour> currentNodes = new List<NodeBehaviour>();

    [SerializeField] private GameObject nodeObject;

    [SerializeField] Transform nodegraphParent;

    private void Awake()
    {
        allNodes = nodegraph.allNodes;
    }

    //LIVE NODE GRAPH

    public void LoadNodeGraph()
    {
        foreach(Flag f in story.flags)
        {
            if(currentNodes.Exists(x=>x.nodeIam.requiredFlag.Name == f.Name)) //if node already exists
            {
                List<NodeBehaviour> nn = currentNodes.FindAll(x => x.nodeIam.requiredFlag.Name == f.Name);

                foreach(NodeBehaviour nb in nn)
                {
                    Node n = nb.nodeIam;

                    if (n.isBound)
                    {
                        continue; //node is already bound, continue.
                    }

                    //node already here - check if it needs connection ( f.Value is the same as requiredFlag.value
                    if (n.requiredFlag.Value == f.Value)
                    {
                        //if so, we need to Place it!
                        PlaceNode(n);
                    }
                }
            }
            else
            {
                //okay, node doesn't exist. Should it?
                if(allNodes.Exists(x=>x.requiredFlag.Name == f.Name && x.requiredFlag.Value == f.Value))
                {
                    //if we have a flag that's identical, then we should place it!
                    List<Node> nodes = FindNodeInAll(f);
                    foreach(Node n in nodes)
                    {
                        PlaceNode(n);
                    }
                    
                }
            }
        }
    }

    public void PlaceNode(Node n)
    {
        GameObject g = Instantiate(nodeObject, nodegraphParent);
        NodeBehaviour objectNode = g.GetComponent<NodeBehaviour>();
        objectNode.nodeIam = n;
        objectNode.nodeIam.optionsFromMe.Clear();
        g.GetComponent<RectTransform>().anchoredPosition = objectNode.nodeIam.coordinates;

        currentNodes.Add(objectNode); //Do I need check that nodes can't be added twice? Hopefully that have happened earlier?

        CheckBindings();
    }


    public void CheckBindings()
    {
        //Foreach node. Check that their nodetobindto is now present in currentnodes
        //if it is, bind it
        //(and if it has been bound before?) hm.

        foreach(NodeBehaviour nb in currentNodes)
        {
            NodeBehaviour nodeToBindThisTo = currentNodes.Find(x => x.nodeIam.name == nb.nodeIam.nodeToBindTo.name);
            if (nodeToBindThisTo != null)
            {
                if(nb.nodeIam.optionsFromMe.Exists(x=>x.name == nodeToBindThisTo.nodeIam.name))
                {
                    // Already has this option! Ignore.
                }
                else
                {
                    NodeBindToNode(nodeToBindThisTo.nodeIam, nb.nodeIam);
                }
            }
        }
    }


    public void NodeBindToNode(Node nodeThatIsOption, Node nodeOptionWillBeAddedTo)
    {
        // Setup options for binded node etc.
        nodeOptionWillBeAddedTo.optionsFromMe.Add(nodeThatIsOption);

        nodeThatIsOption.optionsFromMe.Add(nodeOptionWillBeAddedTo); //add the reverse as well.

        nodeOptionWillBeAddedTo.isBound = true;

        print("Added option to " + nodeOptionWillBeAddedTo.name + ", option is: " + nodeThatIsOption);
    }


    public Node FindNodeInAll(string ID)
    {
        return allNodes.Find(x => x.name == ID);
    }

    public List<Node> FindNodeInAll(Flag flag)
    {
        if(!allNodes.Exists(x => x.requiredFlag.Name == flag.Name))
        {
            Debug.LogError("Flag " + flag.Name + "doesn't exist in all nodes! How did that happen?");
            return null;
        }
        return allNodes.FindAll(x => x.requiredFlag.Name == flag.Name);
    }
}
