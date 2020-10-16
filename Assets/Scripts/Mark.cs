using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// @Author: Veli-Matti Vuoti
/// 
/// -------------OLD-----------
/// This class is for wheel puzzle marks, knows if it is the correct mark.
/// 
/// </summary>
public class Mark : MonoBehaviour
{
    public bool correctMark;

    public bool IsCorrectMark()
    {
        return correctMark;
    }
}
