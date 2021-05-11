using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Melody", menuName = "Melody")]
public class Melody : ScriptableObject//, IEnumerable
{
	public List<Note> Notes => notes;
	[SerializeField] private List<Note> notes = new List<Note>();
	//public List<Note> Notes => notes;

	public int Length => Notes.Count;


	public override string ToString()
	{
		string toString = "";

		foreach (Note note in Notes)
		{
			toString += $"{note.ToString().Replace("Sharp", "#")} ";
		}

		return toString;
	}


}
