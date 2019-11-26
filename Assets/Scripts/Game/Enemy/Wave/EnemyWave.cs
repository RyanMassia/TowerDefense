using System;
using System.Collections.Generic;
using UnityEngine;

//1
[Serializable]
public class EnemyWave
{
    //2 index path this will take 
    public int pathIndex;
    //3 time till wave starts 
    public float startSpawnTimeInSecond;
    //4 delay betweens spawns 
    public float timeBetweenSpawnsInSeconds = 1f;
    //5 list of enemies this wave
    public List<GameObject> listOfEnemies = new List<GameObject>();
}