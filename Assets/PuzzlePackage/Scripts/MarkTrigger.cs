using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkTrigger : MonoBehaviour
{
    public bool debugOn = false;

    public delegate void CollisionDelegate(GameObject source, GameObject targetGameObject);
    public static event CollisionDelegate OnTriggerInEvent;
    public static event CollisionDelegate OnTriggerOutEvent;

    public string hitLayerName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(hitLayerName))
        {
            if (debugOn)
                Debug.Log("Hits target");

            OnTriggerInEvent?.Invoke(gameObject, other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (debugOn)
            Debug.Log("Left target");

        OnTriggerOutEvent?.Invoke(gameObject, other.gameObject);

    }

}
