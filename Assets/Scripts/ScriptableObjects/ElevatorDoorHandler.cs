using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Author : Veli-Matti Vuoti
/// @Co-Author : Sam Hemming (Rewrote and bugfix)
/// 
/// This class is For Elevator
/// Some stypid representation of door opening, changed to run Animation of door open!
/// </summary>
public class ElevatorDoorHandler : MonoBehaviour
{
	[SerializeField, Range(0, 10)] private float closingTime = 1f;
	[SerializeField] private Transform doorRight;
	[SerializeField] private Transform doorLeft;

	[SerializeField] private Vector3 doorLeftClosed;
    [SerializeField] private Vector3 doorRightClosed;
    [SerializeField] private Vector3 doorLeftOpen;
	[SerializeField] private Vector3 doorRightOpen;

	private bool isLocked = false;
	public bool IsLocked { get => isLocked; set => isLocked = value; }

	private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player is near doors!");
            OpenDoors();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player moved away from doors!");
            CloseDoors();
        }
    }


	/// <summary>
	/// TODO: Change this function to activate door open animation.
	/// </summary>
	public void OpenDoors()
	{
		if (isLocked) return;

		StopAllCoroutines();
		StartCoroutine(LerpToPosition(doorLeft, doorLeftOpen));
		StartCoroutine(LerpToPosition(doorRight, doorRightOpen));
	}


	/// <summary>
	/// TODO: Change this function to activate door closing animation.
	/// </summary>
	public void CloseDoors()
	{
		if (isLocked) return;

		StopAllCoroutines();
		StartCoroutine(LerpToPosition(doorLeft, doorLeftClosed));
		StartCoroutine(LerpToPosition(doorRight, doorRightClosed));
	}

	/// <summary>
	/// Lerps given object from its current position to given endposition over closingTime.
	/// All in local worldspace.
	/// </summary>
	/// <param name="obj"></param>
	/// <param name="endPos"></param>
	/// <returns></returns>
	IEnumerator LerpToPosition(Transform obj, Vector3 endPos)
	{
		Vector3 startPos = obj.localPosition;
		float startTime = Time.unscaledTime;
		float endTime = startTime + closingTime;

		while(true)
		{
			float time = Time.unscaledTime;
			obj.localPosition = Vector3.Lerp(startPos, endPos, Mathf.InverseLerp(startTime, endTime, time));

			if (time >= endTime)
				break;

			yield return null;
		}
	}


	// Extra editor and convenience stuff----------------------


	/// <summary>
	/// trys to find doors from parent gameobject
	/// </summary>
	private void Reset()
	{
		var parentTransforms = gameObject.transform.root.GetComponentsInChildren<Transform>();

		foreach(Transform trans in parentTransforms)
		{
			if(trans.name.Equals("DoorLeft"))
			{
				doorLeft = trans;
			}

			if (trans.name.Equals("DoorRight"))
			{
				doorRight = trans;
			}
		}

		if (!doorLeft || !doorRight)
		{
			Debug.LogError("Can not find doors by name! (DoorLeft, DoorRight)");
			return;
		}
	}


	[ContextMenu("Record Doors Open Position")]
	private void RecordDoorsOpenPos()
	{
		if(!doorLeft || !doorRight)
		{
			Debug.LogError("Can not record door position when no door has been assigned!");
			return;
		}

		doorLeftOpen = doorLeft.localPosition;
		doorRightOpen = doorRight.localPosition;
	}


	[ContextMenu("Record Doors Closed Position")]
	private void RecordDoorsClosedPos()
	{
		if (!doorLeft || !doorRight)
		{
			Debug.LogError("Can not record door position when no door has been assigned!");
			return;
		}

		doorLeftClosed = doorLeft.localPosition;
		doorRightClosed = doorRight.localPosition;
	}


	[ContextMenu("Close Doors")]
	private void Close()
	{
		doorLeft.localPosition = doorLeftClosed;
		doorRight.localPosition = doorRightClosed;
	}


	[ContextMenu("Open Doors")]
	private void Open()
	{
		doorLeft.localPosition = doorLeftOpen;
		doorRight.localPosition = doorRightOpen;
	}


}
