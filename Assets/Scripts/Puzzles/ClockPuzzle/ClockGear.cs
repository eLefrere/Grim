using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Author : Sam Hemming
/// 
/// Checks if close enough to the designated target spot and snaps to it, but only after parent puzzle tells its your turn.
/// Once complete tells parent puzzle to move isUpNext tag along.
/// </summary>
[RequireComponent(typeof(Valve.VR.InteractionSystem.Throwable))]
public class ClockGear : Puzzlepart
{
	[Header("Part Specifics")]
	[Tooltip("The spot where this part will snap to.")]
	[SerializeField] private Transform targetSpot = null;

	[Tooltip("How close to the target spot part must be to snap to it.")]
	[SerializeField, Range(0.05f, 0.5f)] private float snapDistance = 0.05f;

	[Tooltip("How long it takes to snapping part to rotate and lock to its position.")]
	[SerializeField, Range(0f, 10f)] private float snappingTime = 1f;

	[SerializeField] private UnityEngine.Events.UnityEvent onPosition;

	[HideInInspector] public bool isUpNext = false;
	[HideInInspector] public bool inPosition = false;
	private Valve.VR.InteractionSystem.Interactable interactable;
	private bool snappingToPosition = false;

	private void Reset()
	{
		eventCodeComplete = "ClockPuzzlePartComplete";
		eventCodeReset = "ClockPuzzlePartReset";
	}

	private void Awake()
	{
		interactable = GetComponent<Valve.VR.InteractionSystem.Interactable>();
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
			interactable.attachedToHand.DetachObject(this.gameObject);
			interactable.highlightOnHover = false;
		}
	}

	/// <summary>
	/// Checks whether the gear is close enough the targetSpot to snap
	/// </summary>
	/// <returns>True if not in position yet but close enough to snap</returns>
	private bool CheckSnapping()
	{
		if(!inPosition && !snappingToPosition && Vector3.Distance(this.transform.position, targetSpot.position) <= snapDistance)
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
		snappingToPosition = true;

		while(true)
		{
			yield return new WaitForFixedUpdate();

			float t = Mathf.InverseLerp(startTime, endTime, Time.time);

			this.transform.position = Vector3.Lerp(startPosition, endPosition, t);
			this.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

			if(Time.time >= endTime)
			{
				inPosition = true;
				snappingToPosition = false;
				onPosition.Invoke();
				SetFinished();
				break;
			}
		}
	}
}
