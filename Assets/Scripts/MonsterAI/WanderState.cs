using System.Collections;
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
        monster.fsm.StartCoroutine(monster.fsm.Wait(2f));
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

        //monster.awareness += Time.deltaTime;
        if (monster.awareness > monster.chaseAwareness)
        {
            monster.fsm.ChangeState(typeof(ChaseState));
        }

        if(monster.fsm.isWaiting)
        {
            monster.nav.isStopped = true;
        }

        if (monster.nav.remainingDistance < 0.1f || monster.nav.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            float changeToWait = Random.Range(0, 100);
            Debug.Log(changeToWait);

            if (changeToWait <= 40)
            {
                monster.fsm.isWaiting = true;
                float t = Random.Range(3f, 8f);
                monster.fsm.StartCoroutine(monster.fsm.Wait(t));
            }
            else
            {          
                monster.nav.SetDestination(monster.fsm.GetRandomPoint(monster.wanderRadius));      
            }
        }

    

    }
}
