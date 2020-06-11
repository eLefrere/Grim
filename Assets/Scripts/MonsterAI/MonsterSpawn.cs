using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Author : Veli-Matti Vuoti
/// 
/// This Class Spawns Monster when spawn event happens
/// </summary>
public class MonsterSpawn : MonoBehaviour
{

    public GameObject monsterPrefab;
    public GameObject monster;

    public Transform[] spawnPoints;
    public Transform[] validSpawnPoints;

    public bool isSpawn;
    public bool isSpawnPressed;

    private void OnEnable()
    {
        EventManager.onMonsterSpawn += Spawn;
        EventManager.onMonsterDespawn += Despawn;
    }

    private void OnDisable()
    {
        EventManager.onMonsterSpawn -= Spawn;
        EventManager.onMonsterDespawn -= Despawn;
    }

    public void Spawn()
    {
        if (monster == null)
        {
            monster = Instantiate(monsterPrefab, transform.position, transform.rotation);
        }
        else
        {
            monster.SetActive(true);
        }
    }

    private void Update()
    {
        if(isSpawnPressed)
        {
            isSpawnPressed = false;

            if(!isSpawn)
            {
                isSpawn = true;
                EventManager.OnMonsterSpawn();
            }
            else
            {
                isSpawn = false;
                EventManager.OnMonsterDespawn();
            }
        }
    }

    public void Despawn()
    {
        monster.SetActive(false);
        monster.transform.position = transform.position;
        monster.transform.rotation = transform.rotation;
    }
}
