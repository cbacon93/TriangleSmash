using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float projectileSpeed = 11f;
    public float health = 3f;
    public float maxHealth = 3f;
    public float shootingRate = 5f;
    public GameObject projectile;
    
    private static PlayerController self = null;
    private Rigidbody2D rb;
    private float shootingTimeout = 0f;
    private bool is_alive = true;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        self = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_alive)
        {
            if (!GetComponent<ParticleSystem>().isPlaying)
            {
                Destroy(gameObject);
                GameOverHandler.OnGameOver();
            }
            return;
        }
        
        //position
        rb.velocity = Vector3.right * Input.GetAxis("Horizontal") * speed + Vector3.up * Input.GetAxis("Vertical") * speed;
        
        //rotation
        /*Vector2 mv = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (mv.magnitude > 0.1) 
        {
            float angle = Vector2.SignedAngle(Vector2.up, mv);
            Vector3 desiredAngle = new Vector3(0f, 0f, angle);
            
            transform.localEulerAngles = desiredAngle;
        }*/
        
        Camera cam = Camera.current;
        if (cam != null)
        {
            Vector2 mousepos = Input.mousePosition;
            Vector2 playerpos = cam.WorldToScreenPoint(transform.position);
            float angle = Vector2.SignedAngle(Vector2.up, mousepos-playerpos);
            Vector3 desiredAngle = new Vector3(0f, 0f, angle);
            transform.localEulerAngles = desiredAngle;
        }
        
        
        //shoot
        if (Input.GetButton("Fire1") && shootingTimeout <= 0f)
        {
            shootingTimeout = 1f/shootingRate;
            GameObject go = Instantiate(projectile);
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            go.transform.position = transform.position + transform.up*0.6f;
            rb.velocity = transform.up*projectileSpeed;
        }
        shootingTimeout -= Time.deltaTime;
        
    }
    
    
    
    
    public void OnHit(float dmg=1f)
    {
        health -= dmg;
        
        if (health <= 0f)
        {
            is_alive = false;
            TimerController.StopTimer();
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<PolygonCollider2D>());
            GetComponent<ParticleSystem>().Play();
        }
    }
    
    
    public static void AddHealth(float health)
    {
        if (self == null) return;
        self.health += health;
        if (self.health >= self.maxHealth)
        {
            self.health = self.maxHealth;
        }
    }
    
    public static void SetHealth(float health)
    {
        if (self == null) return;
        self.health = health;
    }
    
    public static float GetHealth()
    {
        if (self == null) return -1;
        return self.health;
    }
    
    public static Vector3 GetPosition()
    {
        if (self == null) return Vector3.zero;
        return self.transform.position;
    }
    
    
    ~PlayerController()
    {
        //self = null;
    }
}
