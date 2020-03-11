using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    Animator doorAnimator;

    private void Start()
    {
        doorAnimator = gameObject.GetComponent<Animator>();
        EventManager.onAllPuzzlesComplete += OpenDoor;
    }

    private void OnDestroy()
    {
        EventManager.onAllPuzzlesComplete -= OpenDoor;
    }

    public void OpenDoor(string message)
    {

        doorAnimator.SetBool("IsOpen", true);

    }
}
