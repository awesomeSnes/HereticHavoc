using UnityEngine;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
    [SerializeField] public int scoreThreshold;
    [SerializeField] public GameObject[] enemyTypes;
    [SerializeField] public float[] enemyFrequency;  
    [SerializeField] public float minSpawnDelay = 0.2f;
    [SerializeField] public float maxSpawnDelay = 2f;
    [SerializeField] public int minSpawns = 1;
    [Range(2, 8)] [SerializeField] public int maxSpawns = 4 ; 
    [SerializeField] public List<float> enemySpawnChance;

    void Start()
    {

    }

    public List<float> CalculateSpawnPercentage()
    {
        enemySpawnChance = new List<float>();
        float totalFrequency = 0;
        float sectioner = 0;
        //Calculate Total Frequency
        for(int i = 0; i < enemyFrequency.Length; i++)
        {
            totalFrequency += enemyFrequency[i];
        }
        enemySpawnChance.Add(0f);
        //Calculate averages
        for(int j = 0; j < enemyFrequency.Length; j++)
        {
            enemySpawnChance.Add(enemyFrequency[j]/totalFrequency + sectioner);
            sectioner += enemyFrequency[j]/totalFrequency;
            Debug.Log(enemyFrequency[j]/totalFrequency);
        }
        enemySpawnChance.Add(1f);
        Debug.Log(enemySpawnChance);
        return enemySpawnChance;
    }
}
