using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @ Author : Veli-Matti Vuoti
/// 
/// Makes gameobject persistent through scene changes.
/// </summary>
public class DDOL : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
