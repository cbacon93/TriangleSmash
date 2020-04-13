using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
    private float time;
    private int coins;
    private float health;
    public string levelName = "";
    public int nextLevel = 2;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Player") return;
        time = TimerController.GetTimeRaw();
        coins = CoinCounter.GetCoins();
        health = PlayerController.GetHealth();
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(levelName);
    }
    
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TimerController.AddTime(time);
        CoinCounter.AddCoins(coins);
        PlayerController.SetHealth(health);
        LevelGenerator.SetLevelNumber(nextLevel);
        
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Destroy(this);
    }
}
