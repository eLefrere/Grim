using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages events of the game
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

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    #region EventCall Functions for <EventTriggerDelegate>
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
    #endregion

}
