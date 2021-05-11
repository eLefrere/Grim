using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Provides functionality for when an object is attached to belt or hand.
/// </summary>
public class Socket : MonoBehaviour
{
    private Moveable storedObject = null;
    private FixedJoint joint = null;


    private void Awake()
    {
        joint = GetComponent<FixedJoint>();
    }

    public void Attach(Moveable newObject)
    {
        if (storedObject)
            return;

        storedObject = newObject;
        storedObject.transform.position = transform.position;
        storedObject.transform.rotation = transform.rotation;

        Rigidbody targetBody = storedObject.gameObject.GetComponent<Rigidbody>();
        joint.connectedBody = targetBody;
    }

    public void Dettach(Moveable newObject)
    {
        if (!storedObject)
            return;

        joint.connectedBody = null;
        storedObject = null;
    }

    public Moveable GetStoredObject()
    {
        return storedObject;
    }
}