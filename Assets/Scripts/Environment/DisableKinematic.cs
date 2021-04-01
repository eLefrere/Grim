using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Author: Veli-Matti Vuoti
/// 
/// Makes objects fall when hit with object
/// Attach on object with Rigidbody
/// </summary>
public class DisableKinematic : MonoBehaviour
{
    public bool disableOnCollision = true;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        MakeNonKinematic();
    }

    public void MakeNonKinematic()
    {
        if (rb.isKinematic)
            rb.isKinematic = false;
    }

}
