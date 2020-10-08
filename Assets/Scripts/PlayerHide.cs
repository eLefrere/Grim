using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Author : Veli-Matti Vuoti
/// 
/// For debugging if click boolean , playerhide event is triggered.
/// </summary>
public class PlayerHide : MonoBehaviour
{
    public bool isHide;
    public bool isHidePressed;

    public void Update()
    {
        if (isHidePressed)
        {
            isHidePressed = false;

            if (!isHide)
            {
                isHide = true;
                EventManager.OnPlayerHide();
            }
            else
            {
                isHide = false;
                EventManager.OnPlayerUnHide();
            }
        }
    }
}