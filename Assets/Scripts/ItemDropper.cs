using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    public List<RandomDrop> droppingItems;
    public float droppingVelocity = 2f;
    public float radius = 0.3f;
    
    public void Drop()
    {
        List<GameObject> items = new List<GameObject>();
        
        for (int i=0; i < droppingItems.Count; i++)
        {
            if (Random.value <= droppingItems[i].probability)
            {
                for(int j=0; j<droppingItems[i].number; j++)
                {
                    items.Add(droppingItems[i].droppingObject);
                }
            }
        }
        
        //drop items
        if (items.Count > 0)
        {
            float eachAngle = 360f/items.Count;
            float startAngle = Random.value*360;
            
            for(int i=0; i < items.Count; i++)
            {
                Vector3 toItem = Quaternion.Euler(0, 0, startAngle+eachAngle*i)*new Vector3(0f,1f,0f)*radius;
                
                //instantiate if prefab
                GameObject go;
                if (items[i].gameObject.scene.name == null)
                {
                    go = Instantiate(items[i]);
                    go.transform.position = transform.position + toItem;
                } else {
                    go = items[i];
                    go.transform.position = transform.position;
                    go.SetActive(true);
                }
                
                
                
            }
        }
    }
}
