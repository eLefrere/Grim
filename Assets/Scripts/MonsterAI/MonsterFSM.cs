using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// @Author : Veli-Matti Vuoti
/// 
/// This class is Monster Finite State Machine, contains states dictionary, knowledge of current and previous states.
/// 
/// </summary>
public class MonsterFSM : MonoBehaviour
{

    public Dictionary<Type, IState> states = new Dictionary<Type, IState>();
    public IState previousState;
    public IState currentState;

    public bool initialized = false;
    public bool isWaiting = true;
    Monster monster;

    /// <summary>
    /// Initializes the states dictionary with needed state classes
    /// </summary>
    /// <param name="monster"></param>
    public void InitStates(Monster monster)
    {
        this.monster = monster;

        if(DebugTable.MonsterDebug)
        {
            Debug.Log("Initializing the statemachine!");
        }

        states.Add(typeof(WanderState), new WanderState(this.monster));
        states.Add(typeof(ChaseState), new ChaseState(this.monster));
        states.Add(typeof(SearchState), new SearchState(this.monster));

        currentState = states.Values.First();
        currentState.EnterState();

        initialized = true;
    }

    /// <summary>
    /// Changes State and calls the state end and enter functions
    /// </summary>
    /// <param name="toState"></param>
    public void ChangeState(Type toState)
    {
        if (currentState == states[toState])
            return;

        currentState.ExitState();
        previousState = currentState;
        currentState = states[toState];
        currentState.EnterState();
    }

    private void Update()
    {
        if (!initialized)
            return;

        currentState.UpdateState();
    }

    /// <summary>
    /// Returns random point from monster radius
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    public Vector3 GetRandomPoint(float radius)
    {
        return new Vector3(
                    Random.Range(monster.transform.position.x - radius, monster.transform.position.x + radius),
                    monster.transform.position.y,
                    Random.Range(monster.transform.position.z - radius, monster.transform.position.z + radius));
    }

    public IEnumerator Wait(float t)
    {
        yield return new WaitForSeconds(t);
        monster.nav.isStopped = false;
        isWaiting = false;
    }

}
