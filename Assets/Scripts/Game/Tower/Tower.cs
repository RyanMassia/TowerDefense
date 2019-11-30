using UnityEngine;
using System.Collections.Generic;

public enum TowerType
{
    Stone,
    Fire,
    Ice
}

public class Tower : MonoBehaviour
{
    //1  damage the towers inflicts on enemies
    public float attackPower = 3f;
    //2  seconds between each time it fires
    public float timeBetweenAttacksInSeconds = 1f;
    //3 min distance tower needs to attack a target
    public float aggroRadius = 15f;
    //4 level of the tower
    public int towerLevel = 1;
    //5  type of tower it is 
    public TowerType type;
    //6 sound effect as a tower shootsa 
    public AudioClip shootSound;
    //7 where the tower is targeted
    public Transform towerPieceToAim;
    //8 enemy tower is targeting
    public Enemy targetEnemy = null;
    //9  When it hits 0, the tower may shoot
    private float attackCounter;

    private void SmoothlyLookAtTarget(Vector3 target)
    {
        towerPieceToAim.localRotation = UtilityMethods.SmoothlyLook(towerPieceToAim, target);
    }

    protected virtual void AttackEnemy()
    {
        GetComponent<AudioSource>().PlayOneShot(shootSound, .15f);
    }

    //1 returns list of all eneimes with aggro range
    public List<Enemy> GetEnemiesInAggroRange()
    {
        List<Enemy> enemiesInRange = new List<Enemy>();
        //2  go through the eneemy list and add eneimes that are in range to to enemies in range list 
        foreach (Enemy enemy in EnemyManager.Instance.Enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= aggroRadius)
            {
                enemiesInRange.Add(enemy);
            }
        }
        //3  returns the list
        return enemiesInRange;
    }

    //4 returns enemy closest to tower
    public Enemy GetNearestEnemyInRange()
    {
        Enemy nearestEnemy = null;
        float smallestDistance = float.PositiveInfinity;
        //5  goes through all enemies within range, nearest one will be target
        foreach (Enemy enemy in GetEnemiesInAggroRange())
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < smallestDistance)
            {
                smallestDistance = Vector3.Distance(transform.position, enemy.transform.position);
                nearestEnemy = enemy;
            }
        }
        //6  returns nearest enemy
        return nearestEnemy;
    }

    public virtual void Update()
    {
        //1  lowers attack counter
        attackCounter -= Time.deltaTime;
        //2   looks to see if eneemys are targeted
        if (targetEnemy == null)
        {
            //3   if theres a transfrom to rotate look at a spot, this becomes towers idle state   
            if (towerPieceToAim)
            {
                SmoothlyLookAtTarget(towerPieceToAim.transform.position - new Vector3(0, 0, 1));
            }

            //4  attenmpts to look for a new target
            if (GetNearestEnemyInRange() != null && Vector3.Distance(transform.position, GetNearestEnemyInRange().transform.position) <= aggroRadius)
            {
                targetEnemy = GetNearestEnemyInRange();
            }
        }
        else
        { // 5 looks to see if a target is assigned
            //6 if so look at target postion
            if (towerPieceToAim)
            {
                SmoothlyLookAtTarget(targetEnemy.transform.position);
            }
            //7    if attack counter is equal or below 0 call function and reset timer
            if (attackCounter <= 0f)
            {
                // Attack  
                AttackEnemy();
                // Reset attack counter
                attackCounter = timeBetweenAttacksInSeconds;
            }
            //8  if enemy moves out of range reset target to nothing
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) > aggroRadius)
            {
                targetEnemy = null;
            }
        }
    }

    public void LevelUp()
    {
        towerLevel++;
        //Calculate new stats for this tower   
        attackPower *= 2;
        timeBetweenAttacksInSeconds *= 0.7f;
        aggroRadius *= 1.20f;
    }

    public void ShowTowerInfo()
    {
        UIManager.Instance.ShowTowerInfoWindow(this);
    }

}