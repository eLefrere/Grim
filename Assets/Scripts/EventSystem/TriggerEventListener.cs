using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Class listens the events and instantiates the event
/// </summary>
public class TriggerEventListener : MonoBehaviour
{

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
                gameEvent[i].Launch(pos);
            }
        }
       
    }

    public void TriggerExitEvent(string eventCode, Vector3 pos)
    {
        //TODO : do event specific stuff

        for (int i = 0; i < gameEvent.Length; i++)
        {
            if(eventCode == gameEvent[i].eventCode)
            {
                gameEvent[i].Launch(pos);
            }
        }
    }
}
