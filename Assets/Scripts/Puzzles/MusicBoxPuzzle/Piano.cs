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
	[SerializeField] private Melody melody = null;

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



	//-----------------------------------------------------------------------
	public void AddNoteToMelody(Note note)
	{
		melody.Add(note);

		//TODO: Play notes sound

		//if(DebugTable.PuzzleDebug)	Debug.Log("AddNoteToMelody called!");
	}



	//---------------------------------------------------------------------
	public void ClearMelody()
	{
		melody.Clear();

		//if (DebugTable.PuzzleDebug) Debug.Log("Pianos melody cleared!");
	}



	//-------------------------------------------------------------------
	public static string NoteToString(Note note)
	{
		return note.ToString().Replace("Sharp", "#");
	}



	//------------------------------------------------------------------
	private void OnValidate()
	{
		if(melody == null)
		{
			var result = UnityEditor.AssetDatabase.FindAssets("t:Melody", new[] { "Assets/Scripts/Puzzles/MusicBoxPuzzle" });

			if(!result.IsEmpty())
			{
				melody = (Melody)UnityEditor.AssetDatabase.LoadAssetAtPath(UnityEditor.AssetDatabase.GUIDToAssetPath(result[0]), typeof(Melody));
			}
			else
			{
				melody = ScriptableObject.CreateInstance<Melody>();
				UnityEditor.AssetDatabase.CreateAsset(melody, "Assets/Scripts/Puzzles/MusicBoxPuzzle/DefaultMelody.asset");
			}
		}
	}



	//----------------------------------------------------------------
	//----------------------------------------------------------------
#if UNITY_EDITOR

	[UnityEditor.CustomEditor(typeof(Piano))]
	public class PianoEditor : UnityEditor.Editor
	{



		//----------------------------------------------------
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			Piano piano = (Piano)target;

			GUILayout.Space(20);
			UnityEditor.EditorGUILayout.LabelField("Add Notes To Melody", UnityEditor.EditorStyles.boldLabel);
			UnityEditor.EditorGUILayout.BeginHorizontal();

			foreach (Note note in Enum.GetValues(typeof(Note)))
			{
				if(GUILayout.Button(NoteToString(note)))
				{
					//Debug.Log($"GUI button {NoteToString(note)} pressed.");
					piano.AddNoteToMelody(note);
				}
			}

			UnityEditor.EditorGUILayout.EndHorizontal();



			GUILayout.Box((piano.melody != null) ? piano.melody.ToString() : "No melody found?!");

			if (GUILayout.Button("Clear Notes"))
			{
				Melody.ConfirmationPopupWindow window = CreateInstance<Melody.ConfirmationPopupWindow>();
				window.Description = "You sure you want to clear this melody?";
				window.onResult += HandlePopup;
				window.ToPosition();
				window.ShowPopup();
			}

		}



		//----------------------------------------------------
		private void HandlePopup(bool isYes)
		{
			if (isYes) (target as Piano).melody.Clear();
		}
	}
#endif
}
