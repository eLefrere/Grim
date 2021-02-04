using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node3 : ArtefactPuzzle
{
    ArtefactPuzzle node = new ArtefactPuzzle();
    private void OnMouseUp()
    {
        for (int i = 0; i <= 3; i++)
        {
            NodeValues[i] += 1;
            if (NodeValues[i] > 3)
                NodeValues[i] = 0;

            

            Debug.Log(NodeValues[i]);
        }
        GetComponent<MeshRenderer>().material.color = Color.green;
    }
}
