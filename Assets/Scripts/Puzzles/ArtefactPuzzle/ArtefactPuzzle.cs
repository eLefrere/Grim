using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtefactPuzzle : Puzzle
{
    [Tooltip("Artefact puzzle nodes/parts here in order.")]
    [SerializeField]
    public GameObject[] Nodes = new GameObject[4];

    [Tooltip("Artefact puzzle materials to change on click.")]
    [SerializeField]
    private Material[] NodeMats = new Material[4];

    private int[] NodeValues = new int[] { 2, 0, 3, 0 };


    private GameObject renderer;

    public void NodeThree()
    {
        
        for (int i = 0; i <= 3; i++)
        {
            NodeValues[i] += 1;
            if (NodeValues[i] > 3)
                NodeValues[i] = 0;

            renderer = GameObject.Find("Cubeee");
            renderer.GetComponent<MeshRenderer>().material.color = Color.green;

            Debug.Log(NodeValues[i]);
        }

    }
}
