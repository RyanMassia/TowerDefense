using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //1 references UI Manager for instance 
    public static UIManager Instance;
    //2 Reference to addTowerWindow 
    public GameObject addTowerWindow;
    public GameObject towerInfoWindow;
    public GameObject winGameWindow;
    public GameObject loseGameWindow;
    public GameObject blackBackground;
    //3 Reference TO text 
    public Text txtGold;
    public Text txtWave;
    public Text txtEscapedEnemies;
    public Transform enemyHealthBars;
    public GameObject enemyHealthBarPrefab;

    //1 Set Instance to the UIManager script
    void Awake()
    {
        Instance = this;
    }

    //2 Update the gold, wave and escaped enemies Text values
    private void UpdateTopBar()
    {
        txtGold.text = GameManager.Instance.gold.ToString();
        txtWave.text = "Wave " + GameManager.Instance.waveNumber + " / " + WaveManager.Instance.enemyWaves.Count;
        txtEscapedEnemies.text = "Escaped Enemies " + GameManager.Instance.escapedEnemies + " / " + GameManager.Instance.maxAllowedEscapedEnemies;
    }
    //3  Takes a Tower Slot as a parameter, passes it along to the Add Tower Window ﬁeld and shows it at the position of the slot 
    public void ShowAddTowerWindow(GameObject towerSlot)
    {
        addTowerWindow.SetActive(true);
        addTowerWindow.GetComponent<AddTowerWindow>().towerSlotToAddTowerTo = towerSlot;
        UtilityMethods.MoveUiElementToWorldPosition(addTowerWindow.GetComponent<RectTransform>(), towerSlot.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTopBar();
    }

    public void ShowTowerInfoWindow(Tower tower)
    {
        towerInfoWindow.GetComponent<TowerInfoWindow>().tower = tower; towerInfoWindow.SetActive(true);
        UtilityMethods.MoveUiElementToWorldPosition(towerInfoWindow.GetComponent<RectTransform>(), tower.transform.position);
    }

    public void ShowWinScreen()
    {
        blackBackground.SetActive(true);
        winGameWindow.SetActive(true);
    }
    public void ShowLoseScreen()
    {
        blackBackground.SetActive(true);
        loseGameWindow.SetActive(true);
    }

    //1 CreateHealthBarForEnemy accepts the enemy that needs a health bar as its sole parameter.
    public void CreateHealthBarForEnemy(Enemy enemy)
    {
        //2  Create a new health ba
        GameObject healthBar = Instantiate(enemyHealthBarPrefab);
        //3  Parent the new health bar to EnemyHealthBar
        healthBar.transform.SetParent(enemyHealthBars, false);
        //4  Pass the enemy reference to the health bar.
        healthBar.GetComponent<EnemyHealthBar>().enemy = enemy;
    } 

}
