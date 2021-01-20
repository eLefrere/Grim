using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Author : Sam Hemming
/// 
/// Checks if close enough to the designated target spot and snaps to it, but only after parent puzzle tells its your turn.
/// Once complete tells parent puzzle to move isUpNext tag along.
/// </summary>
public class ClockGear : Puzzlepart
{
	[SerializeField] private Transform targetSpot = null;
	[SerializeField] private float snapDistance = 0.05f;
	public bool inPosition = false;
	[SerializeField] private float snappingTime = 1f;
	public bool isUpNext = false;

	private void Awake()
	{
		if (DebugTable.PuzzleDebug && targetSpot == null)
			Debug.LogError($"{this.name}: Target Spot unassigned!");
	}


	/// <summary>
	/// Crude version!!!
	/// Disables VR grabbing once snapping.
	/// </summary>
	private void FixedUpdate()
	{
		if(isUpNext && CheckSnapping())
		{
			GetComponent<Rigidbody>().isKinematic = true;
			GetComponent<Collider>().enabled = false;
		}
	}

	/// <summary>
	/// Checks whether the gear is close enough the targetSpot to snap
	/// </summary>
	/// <returns>True if not in position yet but close enough to snap</returns>
	private bool CheckSnapping()
	{
		if(!inPosition && Vector3.Distance(this.transform.position, targetSpot.position) <= snapDistance)
		{
			StartCoroutine(SnapToPosition());
			return true;
		}

		return false;
	}

	/// <summary>
	/// Coroutine to move to target position
	/// </summary>
	private IEnumerator SnapToPosition()
	{
		Vector3 startPosition = this.transform.position;
		Quaternion startRotation = this.transform.rotation;
		Vector3 endPosition = targetSpot.position;
		Quaternion endRotation = targetSpot.rotation;
		float startTime = Time.time;
		float endTime = Time.time + snappingTime;

		while(true)
		{
			yield return null;

			float t = Mathf.InverseLerp(startTime, endTime, Time.time);

			this.transform.position = Vector3.Lerp(startPosition, endPosition, t);
			this.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

			if(Time.time >= endTime)
			{
				inPosition = true;
				SetFinished();
				break;
			}
		}
	}
}
