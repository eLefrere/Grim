using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Author: Veli-Matti Vuoti
/// 
/// This script swaps the object renderer material 
/// when 
/// player gets too close 
/// and 
/// back to normal when player exits the area.
/// </summary>
public class Highlighter : MonoBehaviour
{

    public Material highlightMaterial;
    public Material normalMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<MeshRenderer>().material = highlightMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<MeshRenderer>().material = normalMaterial;
        }
    }
}
