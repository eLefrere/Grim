using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Author : Veli-Matti Vuoti
/// 
/// This class is the SearchState and contains the functionality of monster searching
/// </summary>
public class SearchState : IState
{

    private Monster monster;

    public float stopTime = 2f;
    public float startTime;
    public float exitTime;
    public float curTime;

    public SearchState(Monster monster)
    {
        this.monster = monster;
    }

    public void EnterState()
    {
        if (DebugTable.MonsterDebug)
        {
            Debug.Log("Entering " + this);
        }

        startTime = 0;
    }

    public void ExitState()
    {
        if (DebugTable.MonsterDebug)
        {
            Debug.Log("Exiting " + this);
        }
        exitTime = curTime;
    }

    public void UpdateState()
    {
        //if (DebugTable.MonsterDebug)
        //{
        //    Debug.Log("Searching the eatable...");
        //}

        if (monster.isPlayerVisible && monster.awareness > monster.chaseAwareness)
        {
            monster.fsm.ChangeState(typeof(ChaseState));
        }

        curTime += Time.deltaTime;

        if (curTime < stopTime)
        {
            if (DebugTable.MonsterDebug)
                Debug.Log("Sniffing Around " + curTime );

            monster.nav.SetDestination(monster.transform.position);
        }
        else
        {
            //Random point close
            if (monster.nav.remainingDistance < 0.1f || monster.nav.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
            {
                monster.nav.speed = 0.6f;

                Vector3 randomPoint = monster.fsm.GetRandomPoint(monster.searchRadius);

                monster.nav.SetDestination(randomPoint);
            }
        }

        monster.awareness -= Time.deltaTime;

        if (monster.awareness < monster.minAwareness)
        {
            //monster.fsm.ChangeState(typeof(WanderState));
            EventManager.OnMonsterDespawn();
        }
    }
}
