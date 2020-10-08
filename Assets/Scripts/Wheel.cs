using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// @Author : Veli-Matti Vuoti
/// 
/// -----------OLD----------
/// Wheel inherits puzzlepart class sets complete when correct mark triggers.
/// </summary>
public class Wheel : Puzzlepart
{
    public Transform rotatingPart;
    public GameObject myTrigger;

    private void OnEnable()
    {
        MarkTrigger.OnTriggerInEvent += CheckTarget;
        MarkTrigger.OnTriggerOutEvent += CheckTarget;
    }

    private void OnDisable()
    {
        MarkTrigger.OnTriggerInEvent -= CheckTarget;
        MarkTrigger.OnTriggerOutEvent -= CheckTarget;
    }

    /// <summary>
    /// Checks if triggering source is correct mark and either finishes or resets the part.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public void CheckTarget(GameObject source, GameObject target)
    {

        if (target == null)
        {
            ResetPart();
            return;
        }

        if (source != myTrigger)
        {
            return;
        }

        Mark mark = target.GetComponent<Mark>();

        if (mark == null)
        {
            ResetPart();
            return;
        }

        if (mark.IsCorrectMark())
        {
            SetFinished();
        }


        if (!mark.IsCorrectMark())
        {
            ResetPart();
        }

    }
}
