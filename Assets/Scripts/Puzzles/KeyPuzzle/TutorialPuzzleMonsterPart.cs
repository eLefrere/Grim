using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPuzzleMonsterPart : Puzzlepart
{

    public GameObject tutorialMonsterPrefab;
    public GameObject tutorialMonster;
    public Transform[] waypoints;

    public float delay = 2f;

    private void OnEnable()
    {
        EventManager.onMonsterSpawnTutorial += SpawnMonster;
    }

    private void OnDisable()
    {
        EventManager.onMonsterSpawnTutorial -= SpawnMonster;
    }

    public void SpawnMonster()
    {
        StartCoroutine(DelaySpawn(delay));
       
    }

    public IEnumerator DelaySpawn(float t)
    {
        yield return new WaitForSeconds(t);
        tutorialMonster = Instantiate(tutorialMonsterPrefab, transform.position, transform.rotation);
        tutorialMonster.GetComponent<TutorialMonster>().waypoints = waypoints;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("TutorialMonster"))
        {
            tutorialMonster.SetActive(false);
            SetFinished();
        }
    }

}
