using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PipeSpawner : MonoBehaviour
{
    [SerializeField] GameObject pipeSpawnPosition;
    [SerializeField] float pipeSpawnDelay;
    [SerializeField] bool SpawnPipes;

    public GameObject pipe;
    public GameObject pipeWithSaw;

    void OnEnable()
    {
        GameEvents.OnGameStarted += StartSpawning;
        BirdController.OnPlayerDied += StopSpawning;
    }

    void OnDisable()
    {
        GameEvents.OnGameStarted += StartSpawning;
        BirdController.OnPlayerDied += StopSpawning;
    }

    void StartSpawning()
    {
        SpawnPipes = true;
        StartCoroutine(SpawnCoroutine());
    }

    void StopSpawning()
    {
        SpawnPipes = false;
        StopAllCoroutines();
    }

    // decide wich pipe it is going to spawn at a random height
    IEnumerator SpawnCoroutine()
    {
        while (SpawnPipes)
        {
            float randomHeight = Random.Range(-12, -7);
            int randomChoice = Random.Range(0, 4);
            if (randomChoice < 3)
            {
                Instantiate(pipeWithSaw, new Vector3(pipeSpawnPosition.transform.position.x, randomHeight, 0), Quaternion.identity);
                yield return new WaitForSeconds(pipeSpawnDelay);
            }
            else
            {
                Instantiate(pipe, new Vector3(pipeSpawnPosition.transform.position.x, randomHeight, 0), Quaternion.identity);
                yield return new WaitForSeconds(pipeSpawnDelay);
            }

        }
    }

}
