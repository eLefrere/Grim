using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node3 : MonoBehaviour
{
    ArtefactPuzzle node = new ArtefactPuzzle();
    private void OnMouseUp()
    {
        node.NodeThree();
    }
}
