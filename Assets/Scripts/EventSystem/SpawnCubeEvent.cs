using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class for testing trigger events and event listener
/// </summary>
public class SpawnCubeEvent : GameEvent
{
    GameObject cube;
    public override void Launch(Vector3 pos)
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = transform.position;
        cube.transform.position += Vector3.up * 2f;
        cube.AddComponent<Rigidbody>();
    }
}
