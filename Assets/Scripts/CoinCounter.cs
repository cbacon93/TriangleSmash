using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    
    private static CoinCounter self = null;
    
    private UnityEngine.UI.Text tf;
    private int coins;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        self = this;
        coins = 0;
        tf = GetComponent<UnityEngine.UI.Text>();
    }
    
    public static void AddCoins(int amount)
    {
        if (self == null) return;
        
        self.coins += amount;
        self.tf.text = "x " + self.coins.ToString();
    }
    
    public static int GetCoins()
    {
        if (self == null) return -1;
        return self.coins;
    }
    
    ~CoinCounter()
    {
        //self = null;
    }
}
