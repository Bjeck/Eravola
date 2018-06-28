using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NodeGraph : MonoBehaviour {

    [SerializeField] Flags flags;

    public List<Node> allNodes = new List<Node>();

    public List<Node> currentNodes = new List<Node>();





    public void LoadNodeGraph()
    {
        foreach(Flag f in flags.flags)
        {
            if(currentNodes.Exists(x=>x.requiredFlag.Name == f.Name))
            {
                Node n = currentNodes.Find(x => x.requiredFlag.Name == f.Name);
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
            else
            {
                //okay, node doesn't exist. Should it?
                if(allNodes.Exists(x=>x.requiredFlag == f))
                {
                    //if we have a flag that's identical, then we should place it!
                    PlaceNode(allNodes.Find(x => x.requiredFlag == f));
                }
            }
        }
    }

    public void PlaceNode(Node n)
    {
        //will take care of binding!
    }

    //another thign I'm not doing yet is handling the optinos for the nodes we're binding to. When we bind to a node we need to add myself to its options!

}
