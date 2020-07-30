using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Elevator : MonoBehaviour
{
    public float topHeight = 6f;
    public float startHeight = 0f;
    public float traverseTime = 5f;

    bool moving = false;
    bool top = false;

    public ElevatorDoorHandler doorHandler;

    public void LiftElevator()
    {
        if (!moving && !top)
        {
            top = true;
            moving = true;
            doorHandler.CloseDoors();
            StartCoroutine(ReachedLevel(topHeight));
            //transform.Rotate(Vector3.up * 90);
        }
    }

    public void DropElevator()
    {
        if (!moving && top)
        {
            top = false;
            moving = true;
            doorHandler.CloseDoors();
            StartCoroutine(ReachedLevel(-topHeight));
            //transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public IEnumerator ReachedLevel(float newHeight)
    {
        yield return new WaitForSeconds(traverseTime);
        transform.position += Vector3.up * newHeight;       
        doorHandler.OpenDoors();
        moving = false;
        Player.instance.transform.position = Vector3.up * transform.position.y;
    }
}
