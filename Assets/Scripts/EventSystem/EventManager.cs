using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author : Veli-Matti Vuoti
/// 
/// Invokes the events, makes easier to have all delegates and events in same class instead of scattered all around classes.
/// Using regions to keep it cleaner.
/// </summary>
public class EventManager : MonoBehaviour
{
    /// <summary>
    /// Delegate for events
    /// </summary>
    /// <param name="eventCode">Pass eventcode as parameter to point subscribed event for specific objects</param>
    public delegate void EventTriggerDelegate(string eventCode, Vector3 pos);   
    public static event EventTriggerDelegate onTriggerEnter;
    public static event EventTriggerDelegate onTriggerExit;

    #region PUZZLE EVENTS
    /// <summary>
    /// Delegate for puzzles
    /// </summary>
    /// <param name="eventCode">Just in case for more complex listening</param>
    public delegate void PuzzleEventDelegate(string eventCode);
    public static event PuzzleEventDelegate onPuzzleComplete;
    public static event PuzzleEventDelegate onPuzzleReset;
    public static event PuzzleEventDelegate onPuzzlepartComplete;
    public static event PuzzleEventDelegate onPuzzlepartReset;

    public static event PuzzleEventDelegate onAllPuzzlesComplete;
    public static event PuzzleEventDelegate on25PercentCompletion;
    public static event PuzzleEventDelegate on50PercentCompletion;
    public static event PuzzleEventDelegate on75PercentCompletion;
    #endregion PUZZLE EVENTS

    /// <summary>
    /// Just normal event without parameters
    /// </summary>
    public delegate void BasicEventDelegate();
    public static event BasicEventDelegate onBasicEvent;

    public delegate void SaveDelegate();
    public static event SaveDelegate onSave;
    public static event SaveDelegate onLoad;

    /// <summary>
    /// Generic One Parameter Delegate, in the end of the event name there are first letter of each parameter
    /// </summary>
    /// <typeparam name="T">Generic T</typeparam>
    /// <param name="parameter">Generic parameter</param>
    /// <returns></returns>
    public delegate T GenericOneParameterDelegate<T>(T parameter);
    public static event GenericOneParameterDelegate<int> onEventI;
    public static event GenericOneParameterDelegate<float> onEventF;
    public static event GenericOneParameterDelegate<string> onEventS;
    public static event GenericOneParameterDelegate<Vector3> onEventV;
    public static event GenericOneParameterDelegate<GameObject> onEventG;
    public static event GenericOneParameterDelegate<Transform> onEventT;
    public static event GenericOneParameterDelegate<object> onEventO;

    /// <summary>
    /// Generic Two Parameter Delegate, in the end of the event name there are first letter of each parameter
    /// </summary>
    /// <typeparam name="T">Generic T</typeparam>
    /// <typeparam name="J">Generic J</typeparam>
    /// <param name="parameterOne">First parameter</param>
    /// <param name="parameterTwo">Second parameter</param>
    /// <returns></returns>
    public delegate T GenericTwoParameterDelegate<T,J>(T parameterOne, J parameterTwo);
    public static event GenericTwoParameterDelegate<int, float> onEventIF;
    public static event GenericTwoParameterDelegate<int, string> onEventIS;
    public static event GenericTwoParameterDelegate<int, Vector3> onEventIV;
    public static event GenericTwoParameterDelegate<int, GameObject> onEventIG;
    public static event GenericTwoParameterDelegate<int, Transform> onEventIT;
    public static event GenericTwoParameterDelegate<int, object> onEventIO;

    /// <summary>
    /// Generic Delegate with three parameter, in the end of the event name there are first letter of each parameter
    /// </summary>
    /// <typeparam name="T">Generic T </typeparam>
    /// <typeparam name="J">Generic J </typeparam>
    /// <typeparam name="K">Generic K </typeparam>
    /// <param name="parameterOne">First parameter</param>
    /// <param name="parameterTwo">Second parameter</param>
    /// <param name="parameterThree">Third parameter</param>
    /// <returns></returns>
    public delegate T GenericThreeParameterDelegate<T, J, K>(T parameterOne, J parameterTwo, K parameterThree);
    public static event GenericThreeParameterDelegate<int, string, float> onEventISF;
    public static event GenericThreeParameterDelegate<int, string, Vector3> onEventISV;
    public static event GenericThreeParameterDelegate<int, string, string> onEventISS;
    public static event GenericThreeParameterDelegate<int, string, GameObject> onEventISG;
    public static event GenericThreeParameterDelegate<int, string,Transform> onEventIST;
    public static event GenericThreeParameterDelegate<int, string, object> onEventISO;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    #region EVENT CALL FUNCTIONS

    #region BASIC EVENT CALL FUNCTIONS
    public static void OnNormalEvent()
    {
        if(DebugTable.instance.IsDebugging("EventDebug"))
        {
            Debug.Log("Invoking normal event!");
        }

        onBasicEvent?.Invoke();
    }
    #endregion BASIC EVENT CALL FUNCTIONS

    #region TRIGGER EVENT CALL FUNCTIONS
    /// <summary>
    /// To Invoke the onTriggerEnter
    /// </summary>
    /// <param name="eventCode">code to specify event</param>
    /// <param name="pos">position of event</param>
    public static void OnTriggerEnterEvent(string eventCode, Vector3 pos)
    {
        if(DebugTable.instance.IsDebugging("EventDebug"))
            Debug.Log("On Trigger Enter Event CODE: " + eventCode + " POSITION: " + pos);

        onTriggerEnter?.Invoke(eventCode, pos);
    }

    /// <summary>
    /// To Invoke the onTriggerExit
    /// </summary>
    /// <param name="eventCode">code to specify event</param>
    /// <param name="pos">position of event</param>
    public static void OnTriggerExitEvent(string eventCode, Vector3 pos)
    {
        if(DebugTable.instance.IsDebugging("EventDebug"))
            Debug.Log("On Trigger Exit Event CODE: " + eventCode + " POSITION: " + pos);

        onTriggerExit?.Invoke(eventCode, pos);
    }

    #endregion TRIGGER EVENT CALL FUNCTIONS

    #region PUZZLE EVENT CALL FUNCTIONS
    public static void OnPuzzleCompleteEvent(string eventCode)
    {
        if (DebugTable.instance.IsDebugging("EventDebug"))
            Debug.Log("On Puzzle Complete Event CODE: " + eventCode);

        onPuzzleComplete?.Invoke(eventCode);
    }

    public static void OnPuzzleResetEvent(string eventCode)
    {
        if (DebugTable.instance.IsDebugging("EventDebug"))
            Debug.Log("On Puzzle Reset Event CODE: " + eventCode);

        onPuzzleReset?.Invoke(eventCode);
    }

    public static void OnPuzzlepartCompleteEvent(string eventCode)
    {
        if (DebugTable.instance.IsDebugging("EventDebug"))
            Debug.Log("On Puzzlepart Complete Event CODE: " + eventCode);

        onPuzzlepartComplete?.Invoke(eventCode);
    }

    public static void OnPuzzlepartResetEvent(string eventCode)
    {
        if (DebugTable.instance.IsDebugging("EventDebug"))
            Debug.Log("On Puzzlepart Reset Event CODE: " + eventCode);

        onPuzzlepartReset?.Invoke(eventCode);
    }

    public static void OnPuzzlesCompleteEvent(string eventCode)
    {
        if (DebugTable.instance.IsDebugging("EventDebug"))
            Debug.Log("Invoking all puzzles compled event : " + eventCode);

        onAllPuzzlesComplete?.Invoke(eventCode);
    }

    public static void OnPuzzles25CompletedEvent(string eventCode)
    {
        if (DebugTable.instance.IsDebugging("EventDebug"))
            Debug.Log("Invoking 25% puzzles compled event : " + eventCode);

        on25PercentCompletion?.Invoke(eventCode);
    }

    public static void OnPuzzles50CompletedEvent(string eventCode)
    {
        if (DebugTable.instance.IsDebugging("EventDebug"))
            Debug.Log("Invoking 50% puzzles compled event : " + eventCode);

        on50PercentCompletion?.Invoke(eventCode);
    }

    public static void OnPuzzles75CompletedEvent(string eventCode)
    {
        if (DebugTable.instance.IsDebugging("EventDebug"))
            Debug.Log("Invoking 75% puzzles compled event : " + eventCode);

        on75PercentCompletion?.Invoke(eventCode);
    }

    #endregion PUZZLE EVENT CALL FUNCTIONS

    #region SAVE EVENT CALL FUNCTIONS
    public static void OnSave()
    {
        if(DebugTable.EventDebug)
            Debug.Log("Calling On Save Event!");

        onSave?.Invoke();
    }

    public static void OnLoad()
    {
        if (DebugTable.EventDebug)
            Debug.Log("Calling On Load Event!");

        onLoad?.Invoke();
    }
    #endregion

    #region GENERIC EVENT CALL FUNCTIONS

    #region GENERIC SINGLE PARAMETER CALL FUNCTIONS
    public void OnGenericEvent(int param)
    {
        if(DebugTable.EventDebug)
        {
            Debug.Log("Invoking Generic event with single parameter : " + param.GetType() + " " + param);
        }
        onEventI?.Invoke(param);
    }

    public void OnGenericEvent(float param)
    {
        if (DebugTable.EventDebug)
            Debug.Log("Invoking Geneic event with single parameter : " + param.GetType() + " " + param);

        onEventF?.Invoke(param);
    }

    public void OnGenericEvent(string param)
    {
        if (DebugTable.EventDebug)
            Debug.Log("Invoking Geneic event with single parameter : " + param.GetType() + " " + param);

        onEventS?.Invoke(param);
    }

    public void OnGenericEvent(Vector3 param)
    {
        if (DebugTable.EventDebug)
            Debug.Log("Invoking Geneic event with single parameter : " + param.GetType() + " " + param);

        onEventV?.Invoke(param);
    }

    public void OnGenericEvent(GameObject param)
    {
        if (DebugTable.EventDebug)
            Debug.Log("Invoking Geneic event with single parameter : " + param.GetType() + " " + param);

        onEventG?.Invoke(param);
    }

    public void OnGenericEvent(Transform param)
    {
        if (DebugTable.EventDebug)
            Debug.Log("Invoking Geneic event with single parameter : " + param.GetType() + " " + param);

        onEventT?.Invoke(param);
    }

    public void OnGenericEvent(object param)
    {
        if (DebugTable.EventDebug)
            Debug.Log("Invoking Geneic event with single parameter : " + param.GetType() + " " + param);

        onEventO?.Invoke(param);
    }

    #endregion SINGLE PARAMETER

    #region GENERIC TWO PARAMETER CALL FUNCTIONS

    public void OnGenericEvent(int param1, float param2)
    {
        if (DebugTable.EventDebug)
        {
            Debug.Log("Invoking Generic event with two parameter : " + param1.GetType() + " " + param1 + " " + param2.GetType() + " " + param2);
        }
        onEventIF?.Invoke(param1, param2);
    }

    public void OnGenericEvent(int param1, string param2)
    {
        if (DebugTable.EventDebug)
            Debug.Log("Invoking Geneic event with two parameter : " + param1.GetType() + " " + param1 + " " + param2.GetType() + " " + param2);

        onEventIS?.Invoke(param1, param2);
    }

    public void OnGenericEvent(int param1 , Vector3 param2)
    {
        if (DebugTable.EventDebug)
            Debug.Log("Invoking Geneic event with two parameter : " + param1.GetType() + " " + param1 + " " + param2.GetType() + " " + param2);

        onEventIV?.Invoke(param1, param2);
    }

    public void OnGenericEvent(int param1, GameObject param2)
    {
        if (DebugTable.EventDebug)
            Debug.Log("Invoking Geneic event with two parameter : " + param1.GetType() + " " + param1 + " " + param2.GetType() + " " + param2);

        onEventIG?.Invoke(param1, param2);
    }

    public void OnGenericEvent(int param1 , Transform param2)
    {
        if (DebugTable.EventDebug)
            Debug.Log("Invoking Geneic event with two parameter : " + param1.GetType() + " " + param1 + " " + param2.GetType() + " " + param2);

        onEventIT?.Invoke(param1, param2);
    }

    public void OnGenericEvent(int param1, object param2)
    {
        if (DebugTable.EventDebug)
            Debug.Log("Invoking Geneic event with two parameter : " + param1.GetType() + " " + param1 + " " + param2.GetType() + " " + param2);

        onEventIO?.Invoke(param1, param2);
    }

    #endregion GENERIC TWO PARAMETER CALL FUNCTIONS

    #region GENERIC THREE PARAMETER CALL FUNCTIONS

    public void OnGenericEvent(int param1, string param2, float param3)
    {
        if (DebugTable.EventDebug)
        {
            Debug.Log("Invoking Generic event with three parameter : " + param1.GetType() + " " + param1 + " " + param2.GetType() + " " + param2 + " " + param3.GetType() + " " + param3);
        }
        onEventISF?.Invoke(param1, param2, param3);
    }

    public void OnGenericEvent(int param1, string param2, string param3)
    {
        if (DebugTable.EventDebug)
        {
            Debug.Log("Invoking Generic event with three parameter : " + param1.GetType() + " " + param1 + " " + param2.GetType() + " " + param2 + " " + param3.GetType() + " " + param3);
        }
        onEventISS?.Invoke(param1, param2, param3);
    }

    public void OnGenericEvent(int param1, string param2, Vector3 param3)
    {
        if (DebugTable.EventDebug)
        {
            Debug.Log("Invoking Generic event with three parameter : " + param1.GetType() + " " + param1 + " " + param2.GetType() + " " + param2 + " " + param3.GetType() + " " + param3);
        }
        onEventISV?.Invoke(param1, param2, param3);
    }
    public void OnGenericEvent(int param1, string param2, Transform param3)
    {
        if (DebugTable.EventDebug)
        {
            Debug.Log("Invoking Generic event with three parameter : " + param1.GetType() + " " + param1 + " " + param2.GetType() + " " + param2 + " " + param3.GetType() + " " + param3);
        }
        onEventIST?.Invoke(param1, param2, param3);
    }

    public void OnGenericEvent(int param1, string param2, GameObject param3)
    {
        if (DebugTable.EventDebug)
        {
            Debug.Log("Invoking Generic event with three parameter : " + param1.GetType() + " " + param1 + " " + param2.GetType() + " " + param2 + " " + param3.GetType() + " " + param3);
        }
        onEventISG?.Invoke(param1, param2, param3);
    }

    public void OnGenericEvent(int param1, string param2, object param3)
    {
        if (DebugTable.EventDebug)
        {
            Debug.Log("Invoking Generic event with three parameter : " + param1.GetType() + " " + param1 + " " + param2.GetType() + " " + param2 + " " + param3.GetType() + " " + param3);
        }
        onEventISO?.Invoke(param1, param2, param3);
    }


    #endregion GENERIC THREE PARAMETER CALL FUNCTIONS


    #endregion GENERIC EVENTS

    #endregion EVENT CALL FUNCTIONS

}
