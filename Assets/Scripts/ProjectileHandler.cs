using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    public float maxLifetime = 5f;
    
    private bool is_alive = true;
    

    // Update is called once per frame
    void Update()
    {
        if (!is_alive)
        {
            if (!GetComponent<ParticleSystem>().isPlaying)
            {
                Destroy(gameObject);
                return;
            }
        }
        
        maxLifetime -= Time.deltaTime;
        if (maxLifetime < 0)
        {
            Destroy(gameObject);
            return;
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerController pc = col.GetComponent<PlayerController>();
        EnemyController ec = col.GetComponent<EnemyController>();
        ChestController cc = col.GetComponent<ChestController>();
        if (pc != null)
            pc.OnHit();
        if (ec != null)
            ec.OnHit();
        if (cc != null)
            cc.OnHit();
        
        //collectable no hit
        if (col.tag == "Collectable")
            return;
        
        OnHit();
    }
    
    
    public void OnHit()
    {    
        Destroy(GetComponent<SpriteRenderer>());
        Destroy(GetComponent<TrailRenderer>());
        Destroy(GetComponent<CircleCollider2D>());
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps.Play();
        is_alive = false;
    }
}
