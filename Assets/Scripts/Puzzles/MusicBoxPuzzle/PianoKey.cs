using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Valve.VR.InteractionSystem.HoverButton))]
public class PianoKey : MonoBehaviour
{
    public delegate void OnPressed(Note note);
    public event OnPressed onPressed;

#pragma warning disable 0649
    [SerializeField] private Note note;
#pragma warning restore 0649



    //----------------------------------------------------
    public void Pressed()
    {
        if(DebugTable.PuzzleDebug)  Debug.Log($"Key: {note.ToString().Replace("Sharp", "#")} was Pressed!");
        onPressed?.Invoke(note);
    }
}
