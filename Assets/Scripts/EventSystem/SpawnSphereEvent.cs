using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author : Veli-Matti Vuoti
/// 
/// this class for testing trigger events and event listener
/// </summary>
public class SpawnSphereEvent : GameEvent
{

    GameObject sphere;

    public override void Raise(Vector3 pos)
    {

        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = transform.position;
        sphere.transform.position += Vector3.up * 2f;
        sphere.AddComponent<Rigidbody>();

    }
}
