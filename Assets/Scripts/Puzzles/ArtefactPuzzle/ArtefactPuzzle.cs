using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtefactPuzzle : MonoBehaviour
{
    [Tooltip("Artefact puzzle nodes/parts here in order.")]
    [SerializeField]
    public MeshRenderer[] Nodes = new MeshRenderer[4];
    

    [Tooltip("Artefact puzzle materials to change on click.")]
    [SerializeField]
    private Material[] NodeMats = new Material[4];

    public int[] NodeValues = new int[] { 2, 0, 3, 0 };

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                //Debug.Log(hit.transform.gameObject.name);

                if (hit.transform.gameObject.name == Nodes[0].name)
                {
                    NodeOne();
                }
                if (hit.transform.gameObject.name == Nodes[1].name)
                {
                    NodeTwo();
                }
                if (hit.transform.gameObject.name == Nodes[2].name)
                {
                    NodeThree();
                }
                if (hit.transform.gameObject.name == Nodes[3].name)
                {
                    NodeFour();
                }
            }
        }
    }

    void NodeOne()
    {

        for (int i = 0; i <= 3; i++)
        {
            if (i == 1 || i == 2)
                continue;
            NodeValues[i] += 1;
            if (NodeValues[i] > 3)
                NodeValues[i] = 0;
            Nodes[i].material = NodeMats[NodeValues[i]];
        }
    }
    void NodeTwo()
    {

        for (int i = 0; i <= 3; i++)
        {
            NodeValues[i] += 1;
            if (NodeValues[i] > 3)
                NodeValues[i] = 0;

            Nodes[i].material = NodeMats[NodeValues[i]];
        }
    }
    void NodeThree()
    {

        for (int i = 0; i <= 3; i++)
        {
            if (i == 1)
                continue;
            NodeValues[i] += 1;
            if (NodeValues[i] > 3)
                NodeValues[i] = 0;
            Nodes[i].material = NodeMats[NodeValues[i]]; 
        }

    }
    void NodeFour()
    {

        for (int i = 0; i <= 3; i++)
        {
            if (i == 1 || i == 2 || i == 0)
                continue;
            NodeValues[i] += 1;
            if (NodeValues[i] > 3)
                NodeValues[i] = 0;
            Nodes[i].material = NodeMats[NodeValues[i]];
        }
    }
}
