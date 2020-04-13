using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    public int coinValue = 1;
    public float healthValue = 0;
    
    public bool isKey = false;
    public int keyId = 0;
    
    public bool isWeapon = false;
    
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (coinValue != 0)
            {
                CoinCounter.AddCoins(coinValue);
            }
            
            if (healthValue != 0)
            {
                PlayerController.AddHealth(healthValue);
            }
            
            if (isKey)
            {
                GameEvents.self.OnCollectingKey(keyId);
            }         
            
            Destroy(gameObject);
        }
    }
}
