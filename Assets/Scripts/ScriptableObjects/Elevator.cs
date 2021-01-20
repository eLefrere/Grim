using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// @Author: Veli-Matti Vuoti
/// 
/// This class moves the elevator upstairs and player aswell.
/// If map changes change the heigh values.
/// 
/// This functions are called by the unity events on elevator lever
/// </summary>
public class Elevator : MonoBehaviour
{
    public float topHeight = 6f;
    public float startHeight = 0f;
    public float traverseTime = 5f;

    bool moving = false;
    bool top = false;

    public ElevatorDoorHandler doorHandler;

    /// <summary>
    /// Calls the coroutine for elevator movement upwards.
    /// </summary>
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

    /// <summary>
    /// Calls the coroutine for elevator movement downwards
    /// </summary>
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

    /// <summary>
    /// Opens the doors after the traverseTime have elapsed
    /// </summary>
    /// <param name="newHeight"></param>
    /// <returns></returns>
    public IEnumerator ReachedLevel(float newHeight)
    {
        yield return new WaitForSeconds(traverseTime);
        transform.position += Vector3.up * newHeight;       
        doorHandler.OpenDoors();
        moving = false;
        Player.instance.transform.position = Vector3.up * transform.position.y;
    }
}
