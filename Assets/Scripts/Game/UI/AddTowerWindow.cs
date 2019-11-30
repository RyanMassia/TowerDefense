using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AddTowerWindow : MonoBehaviour
{
    //1 reference where tower should be built
    public GameObject towerSlotToAddTowerTo;
    //2 looks for towers tyoe as a string name 
    public void AddTower(string towerTypeAsString)
    {
        //3   Convert the string parameter that was passed
        TowerType type = (TowerType)Enum.Parse(typeof(TowerType), towerTypeAsString, true);
        //4  makes sure player has enoguh gold to afford tower
        if (TowerManager.Instance.GetTowerPrice(type) <= GameManager.Instance.gold)
        {
            //5  subtract price from players gold   
            GameManager.Instance.gold -= TowerManager.Instance.GetTowerPrice(type);
            //6 creats tower and disables addtower window  
            TowerManager.Instance.CreateNewTower(towerSlotToAddTowerTo, type);
            gameObject.SetActive(false);
        }
    }
}
