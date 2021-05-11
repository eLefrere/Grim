using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
			if (GUILayout.Button(Piano.NoteToString(note)))
			{
				//Debug.Log($"GUI button {NoteToString(note)} pressed.");
				Add(note);
			}
		}

		UnityEditor.EditorGUILayout.EndHorizontal();



		GUILayout.Box((piano.melody != null) ? piano.melody.ToString() : "No melody found?!");

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
		if (isYes) Clear();
	}



	//------------------------------------------------------------------
	private void OnValidate()
	{
		if ((target as Piano).melody == null)
		{
			var result = UnityEditor.AssetDatabase.FindAssets("t:Melody", new[] { "Assets/Scripts/Puzzles/MusicBoxPuzzle" });

			if (!result.IsEmpty())
			{
				(target as Piano).melody = (Melody)UnityEditor.AssetDatabase.LoadAssetAtPath(UnityEditor.AssetDatabase.GUIDToAssetPath(result[0]), typeof(Melody));
			}
			else
			{
				(target as Piano).melody = ScriptableObject.CreateInstance<Melody>();
				UnityEditor.AssetDatabase.CreateAsset((target as Piano).melody, "Assets/Scripts/Puzzles/MusicBoxPuzzle/DefaultMelody.asset");
			}
		}
	}

	//---------------------------------------------------------------
	private void SaveChanges()
	{
		UnityEditor.EditorUtility.SetDirty((target as Piano).melody);
		//UnityEditor.AssetDatabase.SaveAssets();
	}



	//----------------------------------------------------
	public void Clear()
	{
		UnityEditor.Undo.RecordObject((target as Piano).melody, "Melody Cleared");
		(target as Piano).melody.Notes.Clear();
		//UpdateToString(true);
		SaveChanges();
	}



	//----------------------------------------------------
	public void Add(Note note)
	{
		UnityEditor.Undo.RecordObject((target as Piano).melody, "Note Added To Melody");
		(target as Piano).melody.Notes.Add(note);
		//UpdateToString();
		SaveChanges();
	}

}

