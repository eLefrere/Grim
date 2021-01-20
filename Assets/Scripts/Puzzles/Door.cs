using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Author: Veli-Matti Vuoti
/// 
/// This class activates the main door open animation, 
/// when all puzzles complete event is called.
/// </summary>
public class Door : MonoBehaviour
{

    Animator doorAnimator;
    
    private void OnEnable()
    {
        doorAnimator = gameObject.GetComponent<Animator>();

        EventManager.onAllPuzzlesComplete += OpenDoor;
    }

    private void OnDisable()
    {
        EventManager.onAllPuzzlesComplete -= OpenDoor;
    }

    public void OpenDoor(string message)
    {
       
        doorAnimator.SetBool("IsOpen", true);

    }
}
