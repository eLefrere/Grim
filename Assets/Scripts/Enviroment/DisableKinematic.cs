using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
