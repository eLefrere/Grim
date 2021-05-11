using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToPosition : MonoBehaviour
{
	[Tooltip("The spot where this part will snap to.")]
	[SerializeField] private Transform targetSpot = null;

	[Tooltip("How close to the target spot part must be to snap to it.\n0 = disabled.")]
	[SerializeField, Range(0f, 0.5f)] private float snapDistance = 0.05f;

	[Tooltip("How long it takes to snapping part to rotate and lock to its position.")]
	[SerializeField, Range(0f, 10f)] private float lerppingTime = 1f;

	[Tooltip("Parent to targetspot after snapping to it.")]
	[SerializeField] private bool parentOnSnap = false;

#pragma warning disable 0649
	[SerializeField] private UnityEngine.Events.UnityEvent onPosition;
#pragma warning restore 0649


	private Valve.VR.InteractionSystem.Interactable interactable;
	private Rigidbody rb;

	private bool lerpping = false;
	private bool inPosition = false;


	private void Awake()
	{
		interactable = GetComponent<Valve.VR.InteractionSystem.Interactable>();
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if(!lerpping && InSnappingDistance() && !inPosition)
		{
			lerpping = true;
			interactable.attachedToHand?.DetachObject(this.gameObject);
			StartCoroutine(LerpToTargetSpot());
		}

		if(inPosition && !InSnappingDistance())
		{
			inPosition = false;
		}
	}

	private bool InSnappingDistance()
	{
		if (snapDistance == 0) return false;
		if(Vector3.Distance(transform.position, targetSpot.position) < snapDistance)
		{
			return true;
		}
		return false;
	}

	public void LerpToPos()
	{
		StartCoroutine(LerpToTargetSpot());
	}

	private IEnumerator LerpToTargetSpot()
	{
		var startPosition = transform.position;
		var startRotation = transform.rotation;
		var startTime = Time.unscaledTime;
		var endTime = startTime + lerppingTime;

		while(true)
		{
			yield return null;

			var t = Mathf.InverseLerp(startTime, endTime, Time.unscaledTime);

			transform.SetPositionAndRotation(Vector3.Lerp(startPosition, targetSpot.position, t), Quaternion.Lerp(startRotation, targetSpot.rotation, t));

			if (Time.unscaledTime >= endTime)
			{
				lerpping = false;
				rb.isKinematic = true;
				inPosition = true;

				if (parentOnSnap) transform.parent = targetSpot;

				onPosition?.Invoke();
				break;
			}
		}
	}
}
