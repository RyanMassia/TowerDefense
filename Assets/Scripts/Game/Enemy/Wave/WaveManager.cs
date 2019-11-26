using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //1
    public static WaveManager Instance;
    //2 list of enemy waves that wuk 
    public List<EnemyWave> enemyWaves = new List<EnemyWave>();
    //3 how much time has passed 
    private float elapsedTime = 0f;
    //4 wave that currently active on screen
    private EnemyWave activeWave;
    //5 keeps tracks of last spawned, 
    private float spawnCounter = 0f;
    //6 list of already active waves 
    private List<EnemyWave> activatedWaves = new List<EnemyWave>();

    //1 sets instance of itself 
    private void Awake()
    {
        Instance = this;
    }
    //2 adds ti time elapsed cheak to see if a new wave has started
    void Update()
    {
        elapsedTime += Time.deltaTime;

        SearchForWave();
        UpdateActiveWave();
    }
    private void SearchForWave()
    {
        //3
        foreach (EnemyWave enemyWave in enemyWaves)
        {
            //4
            if(!activatedWaves.Contains(enemyWave)&&
                enemyWave.startSpawnTimeInSecond <= elapsedTime)
            {
                //
                activeWave = enemyWave;
                activatedWaves.Add(enemyWave);
                spawnCounter = 0f;
                GameManager.Instance.waveNumber++;
                //6
                break;
            }
        }
    }
    private void UpdateActiveWave()
    {
        //1 only go if there is a acitve wave 
        if(activeWave!= null)
        {
            spawnCounter += Time.deltaTime;
            //2
            if(spawnCounter >= activeWave.timeBetweenSpawnsInSeconds)
            {
                spawnCounter = 0f;
                //3
                if (activeWave.listOfEnemies.Count != 0)
                {
                    //4
                    GameObject enemy = (GameObject)Instantiate(
                        activeWave.listOfEnemies[0], WayPointManager.Instance.
                        GetSpawnPosition(activeWave.pathIndex), Quaternion.identity);
                    //5
                    enemy.GetComponent<Enemy>().pathIndex = activeWave.pathIndex;
                    //6
                    activeWave.listOfEnemies.RemoveAt(0);
                }
                else
                {
                    //7
                    activeWave = null;
                    //8
                    if(activatedWaves.Count == enemyWaves.Count)
                    {
                        // All waves are over
                        GameManager.Instance.enemySpawningOver = true;
                    }
                }
            }
        }
    }

    public void StopSpawning()
    {
        elapsedTime = 0;
        spawnCounter = 0;
        activeWave = null;
        activatedWaves.Clear();
        enabled = false;
    }
}