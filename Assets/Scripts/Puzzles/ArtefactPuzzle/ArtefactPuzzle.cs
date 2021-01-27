using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtefactPuzzle : MonoBehaviour
{
    [Tooltip("Artefact puzzle nodes/parts here in order.")]
    [SerializeField]
    public GameObject[] Nodes = new GameObject[4];

    [Tooltip("Artefact puzzle materials to change on click.")]
    [SerializeField]
    private Material[] NodeMats = new Material[4];

    private int[] NodeValues = new int[] { 2, 0, 3, 0 };

    private Renderer renderer;
    //private void Start()
    //{
    //    renderer = Nodes[2].GetComponent<Renderer>();
    //    renderer.material.color = Color.green;
    //}
    private void OnMouseUp()
    {
        renderer = Nodes[2].GetComponent<Renderer>();
        NodeThree();
    }
    public void NodeThree()
    {

        for (int i = 0; i <= 3; i++)
        {
            NodeValues[i] += 1;
            if (NodeValues[i] > 3)
                NodeValues[i] = 0;

            //renderer.GetComponent<MeshRenderer>().material.color = Color.green;

            Debug.Log(NodeValues[i]);
        }
        renderer.material.color = Color.green;

    }
}
