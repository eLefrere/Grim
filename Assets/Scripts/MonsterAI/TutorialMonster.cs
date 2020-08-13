using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class TutorialMonster : MonoBehaviour
{
    public Transform[] waypoints;

    public float stopTime = 3f;

    NavMeshAgent nav;
    Animator anim;

    public bool attackMode = false;
    Transform target;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.SetDestination(waypoints[0].transform.position);
        anim = GetComponentInChildren<Animator>();
        anim.SetFloat("Moving", nav.speed);
    }

    private void OnEnable()
    {
        EventManager.onPlayerHide += ChangePassive;
        EventManager.onPlayerUnHide += SeePlayer;
    }

    private void OnDisable()
    {
        EventManager.onPlayerHide -= ChangePassive;
        EventManager.onPlayerUnHide -= SeePlayer;
    }

    public void ChangePassive()
    {
        nav.ResetPath();
        attackMode = false;
        nav.SetDestination(waypoints[0].transform.position);
    }

    public void SeePlayer()
    {
        target = FindObjectOfType<Valve.VR.InteractionSystem.Player>().hmdTransform;
        attackMode = true;
        nav.SetDestination(target.position);
    }

    private void Update()
    {
        if(attackMode)
            nav.SetDestination(target.position);
        
        if (nav.remainingDistance < 0.1f || nav.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {     
            StartCoroutine(Wait(stopTime));        
        }
     
    }

    public IEnumerator Wait(float t)
    {
        nav.isStopped = true;
        anim.SetFloat("Moving", 0.0f);
        yield return new WaitForSeconds(t);
        nav.isStopped = false;
        anim.SetFloat("Moving", nav.speed);
        nav.SetDestination(waypoints[1].transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("MONSTER HIT PLAYER GAMEOVER!");
        }
    }
}
