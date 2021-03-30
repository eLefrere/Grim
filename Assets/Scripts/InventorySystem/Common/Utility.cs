using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class contains various utility methods.
/// </summary>
public static class Utility
{
    /// <summary>
    /// Returns nearest Interactable object, or any object that's derived from Interactable class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="origin"></param>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static T GetNearestInteractable<T>(Vector3 origin, List<T> collection)
        where T: InteractableInv
    {
        T nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (T entity in collection)
        {
            if (!entity)
                continue;

            distance = (entity.gameObject.transform.position - origin).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = entity;
            }
        }


        return nearest;
    }
}


