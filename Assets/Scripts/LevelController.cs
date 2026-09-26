using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject[] enemyTypes;
    [SerializeField] GameObject[] enemySpawners;
    [SerializeField] float minSpawnDelay = 0.2f;
    [SerializeField] float maxSpawnDelay = 2f;
    [SerializeField] int minSpawns = 1;
    [Range(2, 8)] [SerializeField] int maxSpawns = 4 ;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while(true){
            int numToSpawn = Random.Range(minSpawns, maxSpawns + 1);
            List<GameObject> tempSpawnList = enemySpawners.ToList();
            for(int i = 0; i < numToSpawn; i++)
            {
                int spawner = Random.Range(0, tempSpawnList.Count);
                Instantiate(enemyTypes[Random.Range(0, enemyTypes.Length)], tempSpawnList[spawner].transform.position, Quaternion.identity);
                tempSpawnList.RemoveAt(spawner);
            }
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }


}
