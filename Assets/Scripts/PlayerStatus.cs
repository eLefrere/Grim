using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// @Author : Veli-Matti Vuoti
/// 
/// Stores player status effects and changes them through events.
/// Attached on Player!
/// 
/// current statuses:
/// 
/// -player hide
/// 
/// </summary>
public class PlayerStatus : MonoBehaviour
{

    public static bool playerIsHiding;

    private void OnEnable()
    {
        EventManager.onPlayerHide += PlayerIsHidden;
    }

    private void OnDisable()
    {
        EventManager.onPlayerHide -= PlayerNotHidden;
    }

    public void PlayerIsHidden()
    {
        playerIsHiding = true;
    }

    public void PlayerNotHidden()
    {
        playerIsHiding = false;
    }
}
