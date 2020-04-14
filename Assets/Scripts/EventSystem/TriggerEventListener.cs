using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author : Veli-Matti Vuoti
/// 
/// This Class listens the trigger events and raises the correct game events
/// </summary>
public class TriggerEventListener : MonoBehaviour
{
    [Header("List of scripted game events! Like spawn ghost that moves around or other jumpscares!")]
    public GameEvent[] gameEvent;

    private void Start()
    {
        EventManager.onTriggerEnter += TriggerEnterEvent;
        EventManager.onTriggerExit += TriggerExitEvent;

    }

    private void OnDestroy()
    {
        EventManager.onTriggerEnter -= TriggerEnterEvent;
        EventManager.onTriggerExit -= TriggerExitEvent;
    }

    /// <summary>
    /// Listens trigger event and raises the matching events from game event list!
    /// </summary>
    /// <param name="eventCode"></param>
    /// <param name="pos"></param>
    public void TriggerEnterEvent(string eventCode, Vector3 pos)
    {
        //TODO : do event specific stuff

        for (int i = 0; i < gameEvent.Length; i++)
        {
            if (gameEvent[i] == null)
            {
                Debug.LogError("NULL SPOT IN " + this.name + " Game Event Array!");
                continue;
            }

            if(eventCode == gameEvent[i].eventCode)
            {
                gameEvent[i].Raise(pos);
            }
        }
       
    }

    /// <summary>
    /// Checks the correct events from game event list to raise on Trigger Exit event
    /// </summary>
    /// <param name="eventCode"></param>
    /// <param name="pos"></param>
    public void TriggerExitEvent(string eventCode, Vector3 pos)
    {
        //TODO : do event specific stuff

        for (int i = 0; i < gameEvent.Length; i++)
        {
            if (gameEvent[i] == null)
            {
                Debug.LogError("NULL SPOT IN " + this.name + " Game Event Array!");
                continue;
            }

            if (eventCode == gameEvent[i].eventCode)
            {
                gameEvent[i].Raise(pos);
            }
        }
    }
}
