using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Author: Veli-Matti Vuoti
/// 
/// Adds a quick way to exit the game with Escape key
/// </summary>
public class BuildInput : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
