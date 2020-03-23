using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : IState
{

    private Monster monster;

    public WanderState(Monster monster)
    {
        this.monster = monster;
    }

    public void EnterState()
    {
        if (DebugTable.MonsterDebug)
        {
            Debug.Log("Entering " + this);
        }

        monster.nav.SetDestination(monster.fsm.GetRandomPoint(monster.wanderRadius));
        monster.nav.speed = monster.wanderSpeed;
    }

    public void ExitState()
    {
        if (DebugTable.MonsterDebug)
        {
            Debug.Log("Exiting " + this);
        }
    }
    public void UpdateState()
    {
        //if (DebugTable.MonsterDebug)
        //{
        //    Debug.Log(monster.name + " Is Wandering Around... ");
        //}

        monster.awareness += Time.deltaTime;

        if(monster.nav.remainingDistance < 0.1f || monster.nav.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            monster.nav.SetDestination(monster.fsm.GetRandomPoint(monster.wanderRadius));
        }

        if( monster.awareness > monster.chaseAwareness)
        {
            monster.fsm.ChangeState(typeof(ChaseState));
        }
        
    }
}
