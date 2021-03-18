using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// @Author: Veli-Matti Vuoti
/// 
/// Attached on object with renderer!
/// 
/// This class creates highlighter child for the object.
/// </summary>
public class HighlightScript : MonoBehaviour
{

    public Material normalMaterial;
    public Material highlightMaterial;

    public float radius = 2f;
    public Color color = Color.green;

    private SphereCollider col;
    private GameObject trigger;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Start()
    {
        trigger = new GameObject("trigger");
        trigger.transform.parent = transform;
        trigger.transform.position = transform.position;
        trigger.transform.rotation = transform.rotation;
        col = trigger.gameObject.AddComponent<SphereCollider>();
        col.radius = radius;
        col.isTrigger = true;
        Highlighter hl = trigger.AddComponent<Highlighter>();
        hl.highlightMaterial = highlightMaterial;
        hl.normalMaterial = normalMaterial;
    }

}
