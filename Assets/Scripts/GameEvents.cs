using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents self = null;
    
    public Action<int> onCollectingKey;
    
    // Start is called before the first frame update
    void Awake()
    {
        self = this;
    }

    
    
    public void OnCollectingKey(int keyId)
    {
        if (onCollectingKey != null)
        {
            onCollectingKey(keyId);
        }
    }
}
