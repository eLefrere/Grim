using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtefactGlower : MonoBehaviour
{
	static List<GameObject> artefacts = new List<GameObject>();
	

	private Color baseEmissionColor = Color.white;

	[SerializeField] private Material material;
	[Range(0, 1)] public float minGlow = 0;
	[Range(0, 1)] public float maxGlow = 1;
	public float maxGlowDistance = 5;
	public float glowStrength = 1;

	private void Start()
	{
		artefacts.Add(this.gameObject);
		if (!material) material = GetComponent<Renderer>().material;
		baseEmissionColor = material.GetColor("_EmissionColor");
	}

	private void OnDestroy()
	{
		artefacts.Remove(this.gameObject);
	}

	private void Update()
	{
		float distance = Mathf.Infinity;

		foreach (GameObject go in artefacts)
		{
			if (go == this.gameObject) continue;

			float temp = Vector3.Distance(this.gameObject.transform.position, go.transform.position);
			distance = Mathf.Min(temp, distance);
		}

		Debug.Log($"Shortest distance to another glower: {distance}");

		float val01 = Mathf.Clamp01(Mathf.InverseLerp(0, maxGlowDistance, distance));

		float glowVal = Mathf.Lerp(minGlow, maxGlow, val01);

		Color col = baseEmissionColor * Mathf.LinearToGammaSpace(glowVal);

		material.SetColor("_EmissionColor", col);
	}
}
