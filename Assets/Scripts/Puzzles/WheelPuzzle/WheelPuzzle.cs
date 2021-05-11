using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WheelPuzzle : Puzzle
{
	public UnityEvent OnComplete;

	private bool fireOnce = false;

	// TODO: rewrite whole thing
	private void Update()
	{
		if(!fireOnce && finished)
		{
			fireOnce = true;
			OnComplete?.Invoke();
		}
	}
}
