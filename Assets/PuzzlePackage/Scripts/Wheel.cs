using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// REMEMBER TO INHERIT PUZZLEPART
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
