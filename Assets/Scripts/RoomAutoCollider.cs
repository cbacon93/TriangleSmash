using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAutoCollider : MonoBehaviour
{
    public bool createTopCollider = false;
    public bool createBottomCollider = false;
    public bool createLeftCollider = false;
    public bool createRightCollider = false;
    
    public float colliderSize = 0.1f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (createTopCollider) 
        {
            BoxCollider2D bc = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
            bc.size = new Vector2(1.05f, colliderSize);
            bc.offset = new Vector2(0f, 0.5f+colliderSize/2f);
        }
        
        if (createBottomCollider)
        {
            BoxCollider2D bc = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
            bc.size = new Vector2(1.05f, colliderSize);
            bc.offset = new Vector2(0f, -0.5f-colliderSize/2f);
        }
        
        if (createLeftCollider) 
        {
            BoxCollider2D bc = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
            bc.size = new Vector2(colliderSize, 1.05f);
            bc.offset = new Vector2(-0.5f-colliderSize/2f, 0f);
        }
        
        if (createRightCollider)
        {
            BoxCollider2D bc = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
            bc.size = new Vector2(colliderSize, 1.05f);
            bc.offset = new Vector2(0.5f+colliderSize/2f, 0f);
        }
    }
}
