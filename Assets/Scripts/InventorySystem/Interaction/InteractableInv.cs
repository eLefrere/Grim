using UnityEngine;

public class InteractableInv : MonoBehaviour
{
    protected bool isAvailable = true;

    public virtual void StartInteraction(HandInv hand)
    {
        Debug.Log("Start");
    }

    public virtual void Interaction(HandInv hand)
    {
        Debug.Log("Interaction");
    }

    public virtual void EndInteraction(HandInv hand)
    {
        Debug.Log("EndInteraction");
    }

    public bool GetAvailability()
    {
        return isAvailable;
    }
}
