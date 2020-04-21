using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemDropper))]
public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRadius = 10f;
    public float minRadius = 3f;
    public float projectileSpeed = 11f;
    public float shootingRate = 1f;
    public float health = 1f;
    
    public GameObject projectile;
    
    public AudioSource dieSound;
    
    private bool is_alive = true;
    private float shootingTimeout = 2f;
    private Rigidbody2D rb;
    private PolygonCollider2D pc;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PolygonCollider2D>();
        shootingTimeout = 1f/shootingRate;
    }

    // Update is called once per frame
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
        
        //position
        Vector3 playerPosition = PlayerController.GetPosition();
        if (playerPosition != null)
        {
            Vector3 toPlayer = transform.position - playerPosition;
            if (toPlayer.magnitude <= detectionRadius && toPlayer.magnitude >= minRadius)
            {
                rb.velocity = -toPlayer.normalized*speed;
            } else {
                rb.velocity = rb.velocity * 0.9f;
            }
            
            //rotation
            if (toPlayer.magnitude <= detectionRadius)
            {
                float angle = Vector2.SignedAngle(Vector2.up, new Vector2(toPlayer.x, toPlayer.y))+180;
                Vector3 desiredAngle = new Vector3(0f, 0f, angle);
                transform.localEulerAngles = desiredAngle;
                
                //avoid hitting own guy
                RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up*0.6f, transform.up, detectionRadius);
                if (hit.collider != null)
                if (hit.collider.tag != "Enemy")
                {
    
                    //and shoot
                    if (shootingTimeout <= 0f)
                    {
                        
                        shootingTimeout = 1f/shootingRate;
                        
                        GameObject go = Instantiate(projectile);
                        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
                        go.transform.position = transform.position + transform.up*0.6f;
                        rb.velocity = transform.up*projectileSpeed;
                    }
                }
                shootingTimeout -= Time.deltaTime;
            }
        }
    }
    
    
    public void OnHit()
    {
        health -= 1f;
        
        if (health <= 0f)
        {
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(pc);
            is_alive = false;
            
            GetComponent<ParticleSystem>().Play();
            dieSound.Play();
            
            GetComponent<ItemDropper>().Drop();
        }
    }
}
