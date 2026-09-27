using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] List<UpgradeTemplate> upgrades;
    [SerializeField] Button upgradeBtn1;
    [SerializeField] Button upgradeBtn2;
    [SerializeField] Button upgradeBtn3;
    [SerializeField] GameObject upgradeMenu;

    private PlayerController player;
    private int currentLevel = 0;
    private GameObject enemyToSpawn;
    private float enemyToSpawnSect;
    [SerializeField] int upgrade1 = 0; [SerializeField] int upgrade2 = 0; [SerializeField] int upgrade3 = 0;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }
    void Start()
    {
        upgradeMenu.SetActive(false);
    }

    public void UpdateLevel()
    {
        if(player.score >= levels[currentLevel + 1].scoreThreshold)
        {
            Debug.Log("Level Up!!!");
            PromptUpgrade();
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

    void PromptUpgrade()
    {
        Cursor.lockState = CursorLockMode.None;
        upgradeMenu.SetActive(true);
        Time.timeScale = 0f;
        upgrade1 = 0; upgrade2 = 0; upgrade3 = 0;
        do{
        upgrade1 = Random.Range(0, upgrades.Count); upgrade2 = Random.Range(0, upgrades.Count); upgrade3 = Random.Range(0, upgrades.Count);
        } while(upgrade1 == upgrade2 || upgrade2 == upgrade3 || upgrade3 == upgrade1);
        upgradeBtn1.GetComponent<Image>().sprite = upgrades[upgrade1].upgradeSprite;
        upgradeBtn2.GetComponent<Image>().sprite = upgrades[upgrade2].upgradeSprite;
        upgradeBtn3.GetComponent<Image>().sprite = upgrades[upgrade3].upgradeSprite;
    }

    public void Upgrade(int btnNum)
    {
        if(btnNum == 1)
        {
            upgrades[upgrade1].Upgrade(player);
            upgrades.RemoveAt(upgrade1);
        } else if(btnNum == 2)
        {
            upgrades[upgrade2].Upgrade(player);
            upgrades.RemoveAt(upgrade2);
        } else if(btnNum == 3)
        {
            upgrades[upgrade3].Upgrade(player);
            upgrades.RemoveAt(upgrade3);
        }
        Time.timeScale = 1f;
        upgradeMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
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
