using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Object Debug Field.
/// </summary>
[CreateAssetMenu(fileName ="DefaultDebug", menuName ="Debug/DebugField")]
public class DebugField : ScriptableObject
{
    public string debugFieldName;
    public bool enableDebug;
}
