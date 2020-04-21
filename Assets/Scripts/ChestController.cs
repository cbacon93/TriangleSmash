using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemDropper))]
public class ChestController : MonoBehaviour
{
    public AudioSource dieSound;
    private bool is_alive = true;
    
    void Update()
    {
        if (!is_alive)
        {
            if (!GetComponent<ParticleSystem>().isPlaying)
            {
                Destroy(gameObject);
            }
            return;
        }
    }
    
    public void OnHit()
    {
        Destroy(GetComponent<SpriteRenderer>());
        Destroy(GetComponent<BoxCollider2D>());
        is_alive = false;
        
        GetComponent<ParticleSystem>().Play();
        dieSound.Play();
        GetComponent<ItemDropper>().Drop();
    }
}
