using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int doorId;
    public bool isOpen = true;
    public Color closedColor;
    public Color openColor;
    
    
    void Start()
    {
        GameEvents.self.onCollectingKey += OnCollectingKey;
        
        if (isOpen)
        {
            Open();
        } else {
            Close();
        }
        
    }
    
    
    void OnCollectingKey(int keyId)
    {
        if (keyId == doorId)
        {
            Open();
        }
    }

    void Open()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = openColor;
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        bc.enabled = false;
    }
    
    void Close()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = closedColor;
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        bc.enabled = true;
    }
}
