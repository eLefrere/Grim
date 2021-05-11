using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandInv : MonoBehaviour
{
    private Socket socket = null;
    private SteamVR_Behaviour_Pose pose = null;

    public List<InteractableInv> contactInteractables; // Contains all Interactables that are in contact with teh hand.

    private void Awake()
    {
        socket = GetComponent<Socket>();
        pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
            AddInteractable(other.gameObject);
    }

    private void AddInteractable(GameObject newObject)
    {
        InteractableInv newInteractable = newObject.GetComponent<InteractableInv>();
        //if (newInteractable.gameObject.layer == 6)
        contactInteractables.Add(newInteractable);
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveInteractable(other.gameObject);
    }

    private void RemoveInteractable(GameObject newObject)
    {
        InteractableInv existingInteractable = newObject.GetComponent<InteractableInv>();
        contactInteractables.Remove(existingInteractable);
    }

    public void TryInteraction()
    {
        if (NearestInteraction())
            return;

        HeldInteraction();
    }

    private bool NearestInteraction()
    {
        contactInteractables.Remove(socket.GetStoredObject());
        InteractableInv nearestObject = Utility.GetNearestInteractable(transform.position, contactInteractables);

        if (nearestObject)
            nearestObject.StartInteraction(this);
        return nearestObject;
    }

    private void HeldInteraction()
    {


    }

    public void StopInteraction()
    {

    }

    public void Testing()
    {
        Debug.Log("Testing");
    }

}
