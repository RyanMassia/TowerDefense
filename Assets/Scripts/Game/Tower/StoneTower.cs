using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTower : Tower //child of Tower Script
{
    //1 what tower will shoot (stone )
    public GameObject stonePrefab;

    //2 override Attack enemy so that parents code gets run first
    protected override void AttackEnemy()
    {   base.AttackEnemy();
        //3   spawen new projectile 
        GameObject stone = (GameObject)Instantiate(stonePrefab,   towerPieceToAim.position, Quaternion.identity);
        stone.GetComponent<Stone>().enemyToFollow = targetEnemy;
        stone.GetComponent<Stone>().damage = attackPower;
    } 
}
