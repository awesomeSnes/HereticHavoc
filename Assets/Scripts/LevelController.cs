using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelController : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyTypes;
    [SerializeField] GameObject[] enemySpawners;
    [SerializeField] float minSpawnDelay = 0.2f;
    [SerializeField] float maxSpawnDelay = 2f;
    [SerializeField] int minSpawns = 1;
    [Range(2, 8)] [SerializeField] int maxSpawns = 4 ;
    [Range(0,1)] [SerializeField] List<float> enemySpawnChance;
    [SerializeField] Level[] levels;

    private PlayerController player;
    private int currentLevel = 0;
    private GameObject enemyToSpawn;
    private float enemyToSpawnSect;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }
    void Start()
    {
        
//        UpdateLevel();
        
    }

    public void UpdateLevel()
    {
        if(player.score >= levels[currentLevel + 1].scoreThreshold)
        {
            Debug.Log("Level Up!!!");
            currentLevel++;
            enemyTypes = levels[currentLevel].enemyTypes.ToList();
            minSpawnDelay = levels[currentLevel].minSpawnDelay;
            maxSpawnDelay = levels[currentLevel].maxSpawnDelay;
            minSpawns = levels[currentLevel].minSpawns;
            maxSpawns = levels[currentLevel].maxSpawns;
            enemySpawnChance = levels[currentLevel].CalculateSpawnPercentage();
        }
        else if(player.score == 0)
        {
            Debug.Log("Level Up!!!");
            enemyTypes = levels[currentLevel].enemyTypes.ToList();
            minSpawnDelay = levels[currentLevel].minSpawnDelay;
            maxSpawnDelay = levels[currentLevel].maxSpawnDelay;
            minSpawns = levels[currentLevel].minSpawns;
            maxSpawns = levels[currentLevel].maxSpawns;
            enemySpawnChance = levels[currentLevel].CalculateSpawnPercentage();
            StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies()
    {
        while(true){
            int numToSpawn = Random.Range(minSpawns, maxSpawns + 1);
            List<GameObject> tempSpawnList = enemySpawners.ToList();
            for(int i = 0; i < numToSpawn; i++)
            {   
                int spawner = Random.Range(0, tempSpawnList.Count - 1);
                //if(tempSpawnList.Count <= 1){ enemyToSpawn = tempSpawnList[0];}
                    enemyToSpawnSect = Random.Range(0f, 1f);
                    for(int j = 0; j < enemyTypes.Count; j++)
                    {
                        if(enemyToSpawnSect >= enemySpawnChance[j] && enemyToSpawnSect <= enemySpawnChance[j+1])
                        {
                            enemyToSpawn = enemyTypes[j];
                            break;
                        }
                    }
                Instantiate(enemyToSpawn, tempSpawnList[spawner].transform.position, Quaternion.identity);
                tempSpawnList.RemoveAt(spawner);
            }
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }


}
