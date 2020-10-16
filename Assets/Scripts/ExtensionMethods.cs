using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Author : Veli-Matti Vuoti
/// 
/// This class contains extension Methods and other global methods
/// </summary>
public static class ExtensionMethods
{


    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }

    public static bool IsEmpty<T>(this T[] array)
    {
        if (array == null || array.Length == 0)
            return true;

        return false;
    }

}
