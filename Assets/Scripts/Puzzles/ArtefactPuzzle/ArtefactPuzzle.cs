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

    public int[] NodeValues = new int[] { 2, 0, 3, 0 };
}
