using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Classe serializável para permitir edição no Inspetor
[System.Serializable]
public class PipeGroup
{
    public string GroupName;
    public GameObject[] pipes; // Array de canos dentro de cada grupo
}


public class PipeSpawner : MonoBehaviour
{
    // apenas spawnar os canos, unica responsabilidade
    GameObject player; // precisa conhecer o player pro evento 
    [SerializeField] List<PipeGroup> pipeGroups = new List<PipeGroup>();
    private GameObject pipeSpawnPosition;
    public GameObject pipe;
    public GameObject pipeWithSaw;
    [SerializeField] float pipeSpawnDelay;
    [SerializeField] bool SpawnPipes;


    void Start()
    {
        StartCoroutine(SpawnCoroutine());
        player = GameObject.Find("Player");
    }

    void OnEnable()
    {
        birdController.OnPlayerDied += StopSpawning;
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
                Instantiate(pipeWithSaw, new Vector3(pipeSpawnPosition.transform.position.x, randomHeight), Quaternion.identity);
                yield return new WaitForSeconds(pipeSpawnDelay);
            }
            else
            {
                Instantiate(pipe, new Vector3(pipeSpawnPosition.transform.position.x, randomHeight), Quaternion.identity);
                yield return new WaitForSeconds(pipeSpawnDelay);
            }

        }
    }

}
