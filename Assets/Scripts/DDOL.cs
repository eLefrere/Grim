using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @ Author : Veli-Matti Vuoti
/// 
/// Makes go persistent through scene changes
/// </summary>
public class DDOL : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
