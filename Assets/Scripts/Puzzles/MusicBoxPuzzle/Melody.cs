using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Melody", menuName = "Melody")]
public class Melody : ScriptableObject, IEnumerable
{
	public List<Note> Notes { get => notes; }
	public int Length { get => notes.Count; }

	private List<Note> notes = new List<Note>();
	private string toString = "";




	//----------------------------------------------------
	public void Clear()
	{
		notes.Clear();
		UpdateToString(true);
	}



	//----------------------------------------------------
	public void Add(Note note)
	{
		notes.Add(note);
		UpdateToString();
	}



	//----------------------------------------------------
	public IEnumerator GetEnumerator()
	{
		return ((IEnumerable)notes).GetEnumerator();
	}



	//----------------------------------------------------
	public override string ToString()
	{
		return toString;
	}



	//----------------------------------------------------
	private void UpdateToString(bool clear = false)
	{
		if(!clear)
		{
			toString += $"{notes[notes.Count-1].ToString().Replace("Sharp", "#")} ";
			return;
		}

		toString = "";

		foreach (Note note in notes)
		{
			toString += $"{note.ToString().Replace("Sharp", "#")} ";
		}
	}



	//----------------------------------------------------------------
	//----------------------------------------------------------------
#if UNITY_EDITOR

	[UnityEditor.CustomEditor(typeof(Melody))]
	public class MelodyEditor : UnityEditor.Editor
	{



		//----------------------------------------------------
		public override void OnInspectorGUI()
		{
			Melody melody = (Melody)target;

			UnityEditor.EditorGUILayout.LabelField("Add Notes To Melody", UnityEditor.EditorStyles.boldLabel);
			UnityEditor.EditorGUILayout.BeginHorizontal();

			foreach (Note note in Enum.GetValues(typeof(Note)))
			{
				if (GUILayout.Button(note.ToString().Replace("Sharp", "#")))
				{
					//Debug.Log($"GUI button {note.ToString().Replace("Sharp", "#")} pressed.");
					melody.Add(note);
				}
			}

			UnityEditor.EditorGUILayout.EndHorizontal();



			GUILayout.Box(melody.ToString());

			if (GUILayout.Button("Clear Notes"))
			{
				ConfirmationPopupWindow window = CreateInstance<ConfirmationPopupWindow>();
				window.Description = "You sure you want to clear this melody?";
				window.onResult += HandlePopup;
				window.ToPosition();
				window.ShowPopup();
			}

		}



		//----------------------------------------------------
		private void HandlePopup(bool isYes)
		{
			if (isYes) (target as Melody).Clear();
		}
	}



	//----------------------------------------------------
	//----------------------------------------------------
	public class ConfirmationPopupWindow : UnityEditor.EditorWindow
	{
		public string Description { get; set; } = "Default description...";

		public delegate void Result(bool isYes);

		public event Result onResult;



		//----------------------------------------------------
		public void ToPosition()
		{
			this.position = new Rect(GUIUtility.GUIToScreenPoint(Event.current.mousePosition) - new Vector2(125, 50), new Vector2(250, 100));
		}



		//----------------------------------------------------
		private void OnGUI()
		{
			UnityEditor.EditorGUILayout.LabelField(Description, UnityEditor.EditorStyles.wordWrappedLabel);
			GUILayout.Space(10);

			UnityEditor.EditorGUILayout.BeginHorizontal();

			if (GUILayout.Button("Yes!"))
			{
				onResult?.Invoke(true);
				this.Close();
			}

			if (GUILayout.Button("No!"))
			{
				onResult?.Invoke(false);
				this.Close(); 
			}

			UnityEditor.EditorGUILayout.EndHorizontal();

		}



		//----------------------------------------------------
		private void OnLostFocus()
		{
			onResult?.Invoke(false);
			this.Close();
		}
	}
#endif
}
