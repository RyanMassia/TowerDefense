using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoWindow : MonoBehaviour
{
    //1 Reference to the tower that can be upgraded.
    public Tower tower;
    //2 info and the text on the button.
    public Text txtInfo; 
    public Text txtUpgradeCost;
    //3 The gold cost to upgrade the tower
    private int upgradePrice;
    //4 Reference to the upgrade button.
    private GameObject btnUpgrade;

    // Find the upgrade button.
    void Awake()
    {
        btnUpgrade = txtUpgradeCost.transform.parent.gameObject;
    }

    //2 When the window opens, call UpdateInfo().
    void OnEnable()
    {
        UpdateInfo();
    } 
    private void UpdateInfo()
    {
        // Calculate new price for upgrade
        //3 Calculate the price to upgrade the tower
        upgradePrice = Mathf.CeilToInt(TowerManager.Instance.GetTowerPrice(tower.type) * 1.5f * tower.towerLevel);
        //4  Set the info text to reﬂect the tower’s level
        txtInfo.text = tower.type + " Tower Lv " + tower.towerLevel;
        //5   If the tower level is less than three, show the upgrade button. If not, hide it
        if (tower.towerLevel < 3)
        {
            btnUpgrade.SetActive(true);
            txtUpgradeCost.text = "Upgrade\n" + upgradePrice + " Gold";
        }
        else
        {
            btnUpgrade.SetActive(false);
        }
    }

    //6 When upgrading the tower, check if the player has enough gold, then subtract the amount of gold that’s needed to upgrade. Finally, disable the TowerInfoWindow GameObject.
    public void UpgradeTower()
    {
        if (GameManager.Instance.gold >= upgradePrice)
        {
            GameManager.Instance.gold -= upgradePrice;
            tower.LevelUp();
            gameObject.SetActive(false);
        }
    }

}