using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
#pragma warning disable CS0649
	[SerializeField, Tooltip("Should enter be triggered only once?")]
	private bool triggerEnterOnce = false;
	private bool enterTriggered = false;
	[SerializeField]
	private UnityEvent onTriggerEnterEvent;

	[SerializeField, Tooltip("Should exit be triggered only once?")]
	private bool triggerExitOnce = false;
	private bool exitTriggered = false;
	[SerializeField]
	private UnityEvent onTriggerExitEvent;

	[SerializeField]
	private UnityEvent onTriggerStayEvent;
#pragma warning restore CS0649

	[HideInInspector]
	public string tagToInteractWith = "";

	private void Awake()
	{
		Collider[] colliders = GetComponents<Collider>();

		if (colliders.Length == 0)
		{
			if (DebugTable.EventDebug)
				Debug.LogWarning($"No collider set for TriggerEvent!");
			return;
		}

		foreach (var collider in colliders)
		{
			if (collider.isTrigger)
				return; // all ok, atleast one collider is trigger
		}

		if (DebugTable.EventDebug)
			Debug.LogWarning($"No collider set as trigger for TriggerEvent!");

	}

	private void OnTriggerEnter(Collider other)
	{
		if (triggerEnterOnce && enterTriggered)
			return;

		if (tagToInteractWith != "" && !other.CompareTag(tagToInteractWith))
			return; //tag was set and other had different tag

		onTriggerEnterEvent?.Invoke();
		enterTriggered = true;
	}

	private void OnTriggerExit(Collider other)
	{
		if (triggerExitOnce && exitTriggered)
			return;

		if (tagToInteractWith != "" && !other.CompareTag(tagToInteractWith))
			return; //tag was set and other had different tag

		onTriggerExitEvent?.Invoke();
		exitTriggered = true;
	}

	private void OnTriggerStay(Collider other)
	{
		onTriggerStayEvent?.Invoke();
	}


#if UNITY_EDITOR

	[UnityEditor.CustomEditor(typeof(TriggerEvent))]
	public class TriggerEventEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			TriggerEvent triggerEvent = (TriggerEvent)target;

			triggerEvent.tagToInteractWith = UnityEditor.EditorGUILayout.TagField("Tag to interact with:", triggerEvent.tagToInteractWith);

		}
	}
#endif
}