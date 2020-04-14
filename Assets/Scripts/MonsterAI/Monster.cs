using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// @Author : Veli-Matti Vuoti
/// 
/// This Class is Monster with all monster related variables, target and MonsterFSM
/// </summary>
public class Monster : MonoBehaviour
{

    public Transform target;
    public MonsterFSM fsm;

    public float awareness = 0f;
    public float chaseAwareness = 30f;
    public float minAwareness = 0f;
    public float maxAwareness = 100f;
    public float deadlyAwareness = 80f;

    public float baseSpeed;
    public float wanderSpeed = 0.4f;
    public float speedIncrease = 0.001f;
    public float searchRadius = 2f;
    public float wanderRadius = 5f;

    public bool isPlayerVisible = true;
    
    public NavMeshAgent nav => GetComponent<NavMeshAgent>();

    private void Start()
    {
        target = FindObjectOfType<Valve.VR.InteractionSystem.Player>().transform;
        fsm.InitStates(this);
        baseSpeed = nav.speed;
    }

    private void Update()
    {
        if (awareness > maxAwareness)
            awareness = maxAwareness;
        if (awareness < minAwareness)
            awareness = minAwareness;
    }

    private void OnEnable()
    {
        EventManager.onPlayerHide += PlayerIsHidden;
        EventManager.onPlayerUnHide += PlayerVisible;

        if (fsm.initialized)
        {
            fsm.ChangeState(typeof(WanderState));
            awareness = 0;
        }
    }

    private void OnDisable()
    {
        EventManager.onPlayerHide -= PlayerIsHidden;
        EventManager.onPlayerUnHide -= PlayerVisible;
    }

    public void PlayerIsHidden()
    {
        isPlayerVisible = false;
    }

    public void PlayerVisible()
    {
        isPlayerVisible = true;
    }

}
