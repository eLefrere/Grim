using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// @Author: Veli-Matti Vuoti
/// @Co-Author : Sam Hemming (Rewrote and bugfix)
/// 
/// This class moves the elevator upstairs and player aswell.
/// If map changes change the heigh values.
/// 
/// This functions are called by the unity events on elevator lever
/// </summary>
public class Elevator : MonoBehaviour
{
    [SerializeField] private float topHeight = 6f;
	[SerializeField] private float startHeight = 0f;
	[SerializeField] private float traverseTime = 5f;

    bool moving = false;
    bool top = false;

	[SerializeField] private ElevatorDoorHandler doorHandler;

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
            StartCoroutine(ReachedLevel(startHeight));
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
		moving = false;

		Player.instance.transform.position = new Vector3(Player.instance.transform.position.x,
														 Player.instance.transform.position.y - transform.position.y + newHeight,
														 Player.instance.transform.position.z);

		transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);

		doorHandler.OpenDoors();
    }
}
