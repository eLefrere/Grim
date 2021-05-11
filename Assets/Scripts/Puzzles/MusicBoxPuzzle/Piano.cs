using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[Serializable]
public enum Note { C4, F4, A4, ASharp4, C5 }



public class Piano : Puzzle
{

	[Header("Puzzle Specifics")]
	[Tooltip("Add all keys that are part of this piano here.")]
	[SerializeField] private List<PianoKey> keys = new List<PianoKey>();
	public Melody melody = null;

	private Queue<Note> lastNotes = new Queue<Note>();

#pragma warning disable 0649
	[SerializeField] private UnityEngine.Events.UnityEvent onComplete;
#pragma warning restore 0649
	private bool complete = false;



	//------------------------------------------------------------
	private void Reset()
	{
		completionEventCode = "MusicBoxPuzzleComplete";
		resetEventCode = "MusicBoxPuzzleReset";
	}



	//-----------------------------------------------------------
	private void Start()
	{
		if (DebugTable.PuzzleDebug && keys.Count == 0)
			Debug.LogWarning("Piano does not have any keys? You should add some in inspector.");
		
		foreach (PianoKey key in keys)
		{
			key.onPressed += KeyPress;
		}
	}



	//----------------------------------------------------------------
	private void OnDestroy()
	{
		foreach (PianoKey key in keys) key.onPressed -= KeyPress;
	}



	//----------------------------------------------------------------
	private void KeyPress(Note note)
	{
		//if(DebugTable.PuzzleDebug)	Debug.Log("Key press registered!");

		EnqueueNote(note);

		if (!complete && MelodyMatch())
		{
			if (DebugTable.PuzzleDebug) Debug.Log("Melody Matched!!!");
			onComplete?.Invoke();
			complete = true;
			CompletePuzzle();
		}
	}



	//------------------------------------------------------------------
	private bool MelodyMatch()
	{
		if (lastNotes.Count < melody.Length) return false;

		if(lastNotes.SequenceEqual(melody.Notes))
		{
			//Debug.Log("Notes match?");
			return true;
		}

		return false;
	}



	//----------------------------------------------------------------
	private void EnqueueNote(Note note)
	{
		lastNotes.Enqueue(note);

		if (lastNotes.Count > melody.Length)
		{
			lastNotes.Dequeue();
		}

		if (!DebugTable.PuzzleDebug) return; //Debug start

		string test = "Last Notes:";

		foreach(Note n in lastNotes)
		{
			test += $" {NoteToString(n)},";
		}

		Debug.Log(test); //Debug end
	}


	//-------------------------------------------------------------------
	public static string NoteToString(Note note)
	{
		return note.ToString().Replace("Sharp", "#");
	}

}
