using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public int gold;
    public int waveNumber;
    public int escapedEnemies;
    public int maxAllowedEscapedEnemies = 5;
    public bool enemySpawningOver;
    public AudioClip gameWinSound;
    public AudioClip gameLoseSound;
    private bool gameOver;

    private void Awake()
    {
        Instance = this;

    }

    private void Update()
    {
       if (!gameOver && enemySpawningOver)
       {
            if (EnemyManager.Instance.Enemies.Count == 0)
            {
                QuitToTitleScreen();
            }
        }
    }

    void OnGameWin()
    {
        AudioSource.PlayClipAtPoint(gameWinSound, Camera.main.transform.position);
        gameOver = true;
        UIManager.Instance.ShowWinScreen();
    }

    public void QuitToTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    //1 
    public void OnEnemyEscape()
    {
        escapedEnemies++;
        UIManager.Instance.ShowDamage();
        if (escapedEnemies == maxAllowedEscapedEnemies)
        {
            // Too many enemies escaped, you lose the game  
            OnGameLose();
        }
    }

    //2 
    private void OnGameLose()
    {
        gameOver = true;
        AudioSource.PlayClipAtPoint(gameLoseSound, Camera.main.transform.position);
        EnemyManager.Instance.DestroyAllEnemies();   WaveManager.Instance.StopSpawning();
        UIManager.Instance.ShowLoseScreen();
    }

    //3
    public void RetryLevel()
    {
        SceneManager.LoadScene("Game");
    } 

}
