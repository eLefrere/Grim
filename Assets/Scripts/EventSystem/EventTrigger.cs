using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Triggers event with trigger collider
/// </summary>
[RequireComponent(typeof(SphereCollider))]
public class EventTrigger : MonoBehaviour
{
    [Header("EventCode for objects to catch event")]
    public string eventCode;

    [Header("Does this trigger only once during game?")]
    public bool happenOnce;
    public bool triggered;

    [Header("Trigger Cooldown")]
    public float cd = 1f;
    bool oncd = false;
 
    [Header("Can only be triggered by object with this TAG or TAGS")]
    public string[] triggerTag;
    string previousMatchingTag;

    [Header("Trigger radius")]
    public float radius;

    public Color gizmoColor;

    SphereCollider sphereCollider;

    /// <summary>
    /// Checks if collides with object that has corrcet tag to trigger the event
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
      
        for (int i = 0; i < triggerTag.Length; i++)
        {
            if (oncd)
                return;

            

            if (other.gameObject.CompareTag(triggerTag[i]) && previousMatchingTag != triggerTag[i])
            {
                triggered = true;

                EventManager.OnTriggerEnterEvent(eventCode, transform.position); //Kutsuu eventtiä managerista
                StartCoroutine(CD());

                if (happenOnce)
                {
                    gameObject.SetActive(false);
                    return;
                }

                previousMatchingTag = triggerTag[i];

            }
        }

        previousMatchingTag = null;

    }

    /// <summary>
    /// Cooldown for event
    /// </summary>
    /// <returns></returns>
    IEnumerator CD ()
    {
        oncd = true;
        yield return new WaitForSeconds(cd);
        oncd = false;
    }

    /// <summary>
    /// For Drawing radius for sphere collider
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    /// <summary>
    /// Does stuff in editor
    /// </summary>
    private void OnValidate()
    {
        if (sphereCollider == null)
        {
            sphereCollider = gameObject.GetComponent<SphereCollider>();
        }

        if (sphereCollider != null)
        {
            if (sphereCollider.radius != radius)
            {
                sphereCollider.radius = radius;
            }

            if(!sphereCollider.isTrigger)
            {
                sphereCollider.isTrigger = true;
            }
        }
    
    }


}
