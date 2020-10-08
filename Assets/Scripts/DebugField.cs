using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Veli-Matti Vuoti
/// Scriptable Object Debug Field.
/// </summary>
[CreateAssetMenu(fileName ="DefaultDebug", menuName ="Debug/DebugField")]
public class DebugField : ScriptableObject
{
    public string debugFieldName;
    public bool enableDebug;
}
