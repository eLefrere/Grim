using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(CircularDrive))]
public class CircularDriveLocker : MonoBehaviour
{
	private CircularDrive cd;
	private Vector2 angles;

	[SerializeField]
	private bool isLocked = false;


	public UnityEvent OnLock;
	public UnityEvent OnUnlock;
	public UnityEvent OnToggleLock;


	private bool limitByDefault = false;

	private void Start()
	{
		cd = GetComponent<CircularDrive>();
		angles = new Vector2(cd.minAngle, cd.maxAngle);
		limitByDefault = cd.limited;

		if(isLocked)
		{
			cd.minAngle = cd.outAngle;
			cd.maxAngle = cd.outAngle;
			cd.limited = true;
		}

	}


	private void Lock(bool isLocked)
	{
		this.isLocked = isLocked;

		OnToggleLock?.Invoke();

		if(isLocked)
		{
			angles = new Vector2(cd.minAngle, cd.maxAngle);

			cd.minAngle = cd.outAngle;
			cd.maxAngle = cd.outAngle;
			cd.limited = true;

			OnLock?.Invoke();
		}
		else
		{
			cd.minAngle = angles.x;
			cd.maxAngle = angles.y;
			cd.limited = limitByDefault;

			OnUnlock?.Invoke();
		}
	}


	//Public methods-----------------------------------------------------
	public void Lock()
	{
		Lock(true);
	}


	public void Unlock()
	{
		Lock(false);
	}


	public void ToggleLock()
	{
		Lock(!isLocked);
	}
}
