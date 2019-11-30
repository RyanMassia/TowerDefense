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
    //3 Reference TO text 
    public Text txtGold;
    public Text txtWave;
    public Text txtEscapedEnemies;

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

}
