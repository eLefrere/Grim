using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{

    private Monster monster;

    public ChaseState(Monster monster)
    {
        this.monster = monster;
    }

    public void EnterState()
    {
        if(DebugTable.MonsterDebug)
        {
            Debug.Log("Entering " + this);
        }
    }

    public void ExitState()
    {
        if (DebugTable.MonsterDebug)
        {
            Debug.Log("Exiting " + this);
        }
        monster.nav.speed = monster.baseSpeed;
    }

    public void UpdateState()
    {
        //if (DebugTable.MonsterDebug)
        //{
        //    Debug.Log("Chasing the player...");
        //}

        monster.nav.destination = monster.target.position;

        monster.awareness += Time.deltaTime;
        monster.nav.speed += monster.speedIncrease * Time.deltaTime;

        if (!monster.isPlayerVisible && monster.awareness < monster.deadlyAwareness)
        {      
            monster.fsm.ChangeState(typeof(SearchState));
        }
        
    }
}
