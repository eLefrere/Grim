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
	private float halfTraverseTime = 7f;

    bool moving = false;
    bool top = false;

#pragma warning disable 0649
    [SerializeField] private ElevatorDoorHandler doorHandler;
    [SerializeField] private CircularDrive innerDoorCircularDrive;
#pragma warning restore 0649

    private void Start()
	{
        halfTraverseTime = doorHandler.doorOpenCloseTime + doorHandler.shaftTravelTime/2;
	}


	public void LiftElevator()
    {
        if (!moving && !top && InnerDoorClosed())
        {
            top = true;
            moving = true;
            doorHandler.OperateDoors();
            StartCoroutine(ReachedLevel(topHeight));
        }
    }


    public void DropElevator()
    {
        if (!moving && top && InnerDoorClosed())
        {
            top = false;
            moving = true;
            doorHandler.OperateDoors();
            StartCoroutine(ReachedLevel(startHeight));
        }
    }


    public IEnumerator ReachedLevel(float newHeight)
    {
        yield return new WaitForSeconds(halfTraverseTime);

        // move halfway the trip to desguise the sudden move
		Player.instance.transform.position = new Vector3(Player.instance.transform.position.x,
														 Player.instance.transform.position.y - transform.position.y + newHeight,
														 Player.instance.transform.position.z);

		transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);

        yield return new WaitForSeconds(halfTraverseTime);
        moving = false;
    }

    private bool InnerDoorClosed()
	{
        if (Mathf.Approximately(innerDoorCircularDrive.outAngle, innerDoorCircularDrive.minAngle)) return true;
        return false;
	}
}
