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
	[SerializeField, Range(0, 10)] public float doorOpenCloseTime = 1f;
	[SerializeField, Range(2, 10)] public float shaftTravelTime = 5f;
#pragma warning disable 0649
	[SerializeField] private CircularDriveLocker circularDriveLocker;
#pragma warning restore 0649

	private SkinnedMeshRenderer skinnedMesh;

	public bool isLocked = false;
	public bool IsLocked { get => isLocked; set => isLocked = value; }

	private void Start()
	{
		skinnedMesh = GetComponent<SkinnedMeshRenderer>();
	}

	/// <summary>
	/// TODO: Change this function to activate door open animation.
	/// </summary>
	public void OperateDoors()
	{
		if (isLocked) return;

		StopAllCoroutines();
		StartCoroutine(LerpMesh());
	}


	IEnumerator LerpMesh()
	{
		// Lock inner door
		circularDriveLocker.Lock();


		// Doors close
		float startTime = Time.unscaledTime;
		float endTime = startTime + doorOpenCloseTime;

		while(true)
		{
			float time = Time.unscaledTime;
			skinnedMesh.SetBlendShapeWeight(0, 100*Mathf.InverseLerp(startTime, endTime, time));

			if (time >= endTime)
				break;

			yield return null;
		}


		// Shaft travel
		startTime = Time.unscaledTime;
		endTime = startTime + shaftTravelTime;

		bool goingUp = (skinnedMesh.GetBlendShapeWeight(1) == 0);

		while (true)
		{
			float time = Time.unscaledTime;
			if (goingUp) skinnedMesh.SetBlendShapeWeight(1, 100 * Mathf.InverseLerp(startTime, endTime, time));
			else skinnedMesh.SetBlendShapeWeight(1, 100 * (1 - Mathf.InverseLerp(startTime, endTime, time)));

			if (time >= endTime)
				break;

			yield return null;
		}


		// Doors open
		startTime = Time.unscaledTime;
		endTime = startTime + doorOpenCloseTime;

		while (true)
		{
			float time = Time.unscaledTime;
			skinnedMesh.SetBlendShapeWeight(0, 100 * (1 - Mathf.InverseLerp(startTime, endTime, time)));

			if (time >= endTime)
				break;

			yield return null;
		}


		// Unlock inner door
		circularDriveLocker.Unlock();
	}

}
