using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public GameObject canvas;
    public UnityEngine.UI.Text textField;
    public static GameOverHandler self = null;
    
    void Awake()
    {
        self = this;
    }
    
    public static void OnGameOver()
    {
        if (self == null) return;
        self.canvas.SetActive(true);
        self.GetComponent<AudioSource>().Play();
        
        self.textField.text = "You got a score of " + CoinCounter.GetCoins() + " and took " + TimerController.GetTime() + " Minutes!";
    }
    
    
    public void OnRestartButton()
    {
        SceneManager.LoadScene("Level1");
    }
}
