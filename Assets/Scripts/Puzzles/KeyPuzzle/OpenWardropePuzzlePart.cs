using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWardropePuzzlePart : Puzzlepart
{
    Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Key"))
        {
            Debug.Log("Wardrobe Key Used");
            Collider col = GetComponent<Collider>();
         
            col.enabled = false;

            anim = GetComponentInParent<Animator>();
            anim.SetTrigger("Open");

            EventManager.OnMonsterSpawnTutorial();

            SetFinished();
        }
    }

    private void OnEnable()
    {
        EventManager.onPlayerHide += CloseDoors;
        EventManager.onPlayerUnHide += OpenDoors;
    }

    private void OnDisable()
    {
        EventManager.onPlayerHide -= CloseDoors;
        EventManager.onPlayerUnHide -= OpenDoors;
    }

    public void CloseDoors()
    {
        if (anim != null)
            anim.SetTrigger("Close");
    }

    public void OpenDoors()
    {
        if (anim != null)
            anim.SetTrigger("Open");
    }

}
