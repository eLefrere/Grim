using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

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
            Vector3 spawnpos = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            float currentDistance = Vector3.Distance(spawnpos, Player.instance.transform.position);

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                float distance = Vector3.Distance(spawnPoints[i].position, Player.instance.transform.position);

                if (currentDistance < distance)
                {
                    spawnpos = spawnPoints[i].position;
                    currentDistance = distance;
                }
            }

            monster = Instantiate(monsterPrefab, spawnpos, transform.rotation);
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
