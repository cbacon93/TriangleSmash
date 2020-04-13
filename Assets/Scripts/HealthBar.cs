using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject h1;
    public GameObject h2;
    public GameObject h3;

    // Update is called once per frame
    void Update()
    {
        float health = PlayerController.GetHealth();
        
        h3.SetActive(health >= 3f);
        h2.SetActive(health >= 2f);
        h1.SetActive(health >= 1f);
    }
}
