using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidespot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            EventManager.OnPlayerHide();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            EventManager.OnPlayerUnHide();
        }
    }
}
