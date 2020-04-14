using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Author : Veli-Matti Vuoti
/// 
/// This class represents the base values of all savedata objects must have
/// id, active, position, rotation and scale
/// </summary>
public class SaveData
{
    public int id;
    public bool active;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
}
