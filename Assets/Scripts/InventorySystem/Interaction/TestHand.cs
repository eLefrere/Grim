//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class TestHand : MonoBehaviour
//{



//    public List<InteractableInv> contactInteractables; // Contains all Interactables that are in contact with teh hand.

//    private void OnTriggerEnter(Collider other)
//    {
//        AddInteractable(other.gameObject);
//    }

//    private void AddInteractable(GameObject newObject)
//    {
//        InteractableInv newInteractable = newObject.GetComponent<InteractableInv>();
//        contactInteractables.Add(newInteractable);
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        RemoveInteractable(other.gameObject);
//    }

//    private void RemoveInteractable(GameObject newObject)
//    {
//        InteractableInv existingInteractable = newObject.GetComponent<InteractableInv>();
//        contactInteractables.Remove(existingInteractable);
//    }
//}
