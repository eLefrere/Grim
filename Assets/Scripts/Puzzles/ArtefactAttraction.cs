using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ArtefactAttraction : MonoBehaviour
{
	static List<GameObject> artefacts = new List<GameObject>();
	static float attractionStrength = 100;

	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();

		artefacts.Add(this.gameObject);
	}

	private void OnDestroy()
	{
		artefacts.Remove(this.gameObject);
	}

	private void FixedUpdate()
	{
		foreach(GameObject go in artefacts)
		{
			if (go == this.gameObject) continue;

			Vector3 toTarget = go.transform.position - this.transform.position;
			rb.AddForce(toTarget.normalized * (1 / toTarget.magnitude) * attractionStrength * Time.fixedDeltaTime);
			//Debug.Log($"Direction: {toTarget.normalized}, Magnitude: {(1 / toTarget.magnitude)}, Force: {toTarget.normalized * (1 / toTarget.magnitude) * attractionStrength * Time.fixedDeltaTime}");
		}
	}
}
