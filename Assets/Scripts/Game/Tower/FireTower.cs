﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : Tower
{
    public GameObject fireParticlesPrefab;
    protected override void AttackEnemy()
    {
        base.AttackEnemy();
        GameObject particles = (GameObject)Instantiate(fireParticlesPrefab, transform.position + new Vector3(0, 0.5f), fireParticlesPrefab.transform.rotation);
        //5 Scale fire particle radius with the aggro radius
        particles.transform.localScale *= aggroRadius / 10.0f;

        foreach (Enemy enemy in EnemyManager.Instance.GetEnemiesInRange(transform.position, aggroRadius))
        {
            enemy.TakeDamage(attackPower);
        }
    }

}
