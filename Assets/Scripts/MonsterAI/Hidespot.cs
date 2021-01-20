using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Author: Veli-Matti Vuoti
/// 
/// Activates player hide when player enters.
/// 
/// Attached on object with trigger "area" collider representing the hide area.
/// </summary>
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
