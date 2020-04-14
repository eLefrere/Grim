using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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