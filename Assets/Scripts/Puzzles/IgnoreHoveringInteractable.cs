using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreHoveringInteractable : MonoBehaviour
{
	[Tooltip("Should IgnoreHovering be applied for child objects as well.")]
	[SerializeField] private bool includeChild = true;

	[Tooltip("Should IgnoreHovering be applied at Start.")]
	[SerializeField] private bool startIgnoring = false;

#pragma warning disable 0649
	[SerializeField] private UnityEngine.Events.UnityEvent onIgnoreHovering;
	[SerializeField] private UnityEngine.Events.UnityEvent onToggleHovering;
	[SerializeField] private UnityEngine.Events.UnityEvent onEnableHovering;
#pragma warning restore 0649

	private List<GameObject> gameObjects = new List<GameObject>();
	private bool ignoring = false;

	private void Start()
	{
		Invoke("LateStart", 0);
	}


	private void LateStart()
	{
		gameObjects.Add(gameObject);

		if (includeChild && gameObject.transform.childCount > 0)
		{
			gameObjects.AddRange(GetChild(gameObject));
		}

		if (startIgnoring) IgnoreHovering();
	}

	public void ToggleHovering()
	{
		if (ignoring) EnableHovering();
		else IgnoreHovering();
	}


	public void IgnoreHovering()
	{
		if (ignoring) return;

		foreach (GameObject go in gameObjects) 
			go.AddComponent<Valve.VR.InteractionSystem.IgnoreHovering>();

		ignoring = true;
		onIgnoreHovering?.Invoke();
		onToggleHovering?.Invoke();
	}


	public void EnableHovering()
	{
		if (!ignoring) return;

		foreach (GameObject go in gameObjects) 
			Destroy(go.GetComponent<Valve.VR.InteractionSystem.IgnoreHovering>());

		ignoring = false;
		onEnableHovering?.Invoke();
		onToggleHovering?.Invoke();
	}

	/// <summary>
	/// Get GameObjects all child GameObjects recursively
	/// </summary>
	static private List<GameObject> GetChild(in GameObject go)
	{
		List<GameObject> gos = new List<GameObject>();

		for(int i = 0; i < go.transform.childCount; i++)
		{
			GameObject child = go.transform.GetChild(i).gameObject;

			gos.Add(child);

			if(child.transform.childCount > 0)
			{
				gos.AddRange(GetChild(child));
			}
		}

		return gos;
	}
}
