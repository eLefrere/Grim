using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
