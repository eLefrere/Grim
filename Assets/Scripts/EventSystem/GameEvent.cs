using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Class is base for game events
/// </summary>
public abstract class GameEvent : MonoBehaviour
{
    
    public string eventCode;

    public abstract void Launch(Vector3 pos);
}
