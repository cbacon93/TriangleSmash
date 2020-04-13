using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator self = null;
    
    public GameObject firstRoom;
    public GameObject levelDescriptor;
    public int levelNumber = 3;
    
    public GameObject roomObject;
    public GameObject enemyObject;
    public GameObject coinObject;
    public GameObject keyObject;
    public GameObject chestObject;
    public GameObject doorObject;
    public GameObject nextLevelObject;
    
    
    public float corridorWidth = 3f;
    public float minCorridorLength = 5f;
    public float maxCorridorLength = 15f;
    public float minRoomLength = 10f;
    public float maxRoomLength = 20f;
    public int minRoomCount = 3;
    public int maxRoomCount = 6;
    public float dirChangeValue = 0.3f;
    
    private GameObject currentRoom;
    private int roomCounter = 2;
    private int enemyCounter = 1;
    private int chestCounter = 1;
    private int keyCounter = 1;
    
    private Vector2 startDirection = Vector2.right;
    private Vector2 currentDirection = Vector2.right;
    
    private List<GameObject> droppingObjects;
    
    
    void Awake()
    {
        self = this;
    }
    
    void Start()
    {
        droppingObjects = new List<GameObject>();
        currentRoom = firstRoom;
        int roomCount = Mathf.RoundToInt(Random.Range(minRoomCount, maxRoomCount));
        PickStartDirection();
        currentDirection = startDirection;
        
        for (int i=0; i<roomCount; i++)
        {
            GenRoomWithCorridor(i != 0);
            PlaceEnemies();
            PlaceChest();
            ChangeBuildDirection();
        }
        
        GenEndRoom();
        
        UnityEngine.UI.Text text = levelDescriptor.GetComponent<UnityEngine.UI.Text>();
        text.text = "Level " + levelNumber.ToString();
    }
    
    void GenRoomWithCorridor(bool canBeLocked)
    {
        //has chance for being locked
        bool placeDoorKeyCombo = canBeLocked && (Random.value < 0.3f);
        
        if (placeDoorKeyCombo) PlaceKey(keyCounter);
        GenCorridor();
        if (placeDoorKeyCombo) PlaceDoor(keyCounter++);
        
        float roomWidth = Random.Range(minRoomLength, maxRoomLength);
        float roomHeight = Random.Range(minRoomLength, maxRoomLength);
        
        GenRoom(new Vector2(roomWidth, roomHeight));
    }
    
    void GenRoom(Vector2 dimension)
    {
        GameObject room = Instantiate(roomObject);
        
        float dx = 0f;
        float dy = 0f;
        
        if (currentDirection == Vector2.right || currentDirection == Vector2.left)
        {
            dx = currentRoom.transform.localScale.x/2f + dimension.x/2f;
            dy = Random.Range(-1f,1f)*(currentRoom.transform.localScale.y/2f - dimension.y/2f);
            
            if (currentDirection == Vector2.left)
                dx = -dx;
        }
        if (currentDirection == Vector2.up || currentDirection == Vector2.down)
        {
            dx = Random.Range(-1f,1f)*(currentRoom.transform.localScale.x/2f - dimension.x/2f);
            dy = currentRoom.transform.localScale.y/2f + dimension.y/2f;
            
            if (currentDirection == Vector2.down)
                dy= -dy;
        }
        
        room.transform.position = currentRoom.transform.position + Vector3.right * dx + Vector3.up * dy;
        
        room.transform.localScale = new Vector3(dimension.x, dimension.y, 1f);
        room.transform.parent = gameObject.transform;
        room.name = "Room " + roomCounter++.ToString();
        currentRoom = room;
    }
    
    void GenCorridor()
    {
        float x = Random.Range(minCorridorLength, maxCorridorLength);
        float y = corridorWidth;
        
        if (currentDirection == Vector2.up || currentDirection == Vector2.down)
        {
            float z = x;
            x = y;
            y = z;
        }
        
        GenRoom(new Vector2(x, y));
    }
    
    void GenEndRoom()
    {
        GenCorridor();
        PlaceDoor(100);
        HideKey(100);
        
        GameObject nlo = Instantiate(nextLevelObject);
        
        float dx = 0f;
        float dy = 0f;
        
        if (currentDirection == Vector2.right || currentDirection == Vector2.left)
        {
            dx = currentRoom.transform.localScale.x/2f - corridorWidth/2f;
            if (currentDirection == Vector2.left)
                dx = -dx;
        }
        if (currentDirection == Vector2.up || currentDirection == Vector2.down)
        {
            dy = currentRoom.transform.localScale.y/2f - corridorWidth/2f;
            if (currentDirection == Vector2.down)
                dy = -dy;
        }
        
        nlo.transform.position = currentRoom.transform.position + Vector3.right * dx + Vector3.up * dy;
        nlo.transform.localScale = new Vector3(corridorWidth, corridorWidth, 1f);
        nlo.transform.parent = transform.parent;
        
        NextLevelScript nls = nlo.GetComponent<NextLevelScript>();
        nls.levelName = "LevelX";
        nls.nextLevel = levelNumber+1;
    }
    
    
    void PlaceEnemies()
    {
        float maxEnemies = Mathf.Clamp(TimerController.GetTimeRaw()/60f*2f, 2, 15);
        int eCount = Mathf.RoundToInt(Random.Range(0, maxEnemies));
        
        
        for (int x = 0; x < eCount; x++)
        {
            float dx = Random.Range(-1f, 1f) * (currentRoom.transform.localScale.x/2f-1f);
            float dy = Random.Range(-1f, 1f) * (currentRoom.transform.localScale.y/2f-1f);
            
            GameObject enemy = Instantiate(enemyObject);
            enemy.transform.position = currentRoom.transform.position + Vector3.right * dx + Vector3.up * dy;
            enemy.transform.parent = transform.parent;
            enemy.name = "Enemy " + enemyCounter++.ToString();
            droppingObjects.Add(enemy);
        }
    }
    
    
    void PlaceChest()
    {
        float spawnChance = Mathf.Clamp(0.5f - TimerController.GetTimeRaw()/1000f, 0.05f, 1f);
        if (spawnChance >= Random.value || true)
        {
            float dx = Random.Range(-1f, 1f) * (currentRoom.transform.localScale.x/2f-1f);
            float dy = Random.Range(-1f, 1f) * (currentRoom.transform.localScale.y/2f-1f);
            
            GameObject chest = Instantiate(chestObject);
            chest.transform.position = currentRoom.transform.position + Vector3.right * dx + Vector3.up * dy;
            chest.transform.parent = transform.parent;
            chest.name = "Chest " + chestCounter++.ToString();
            droppingObjects.Add(chest);
            /*
            GameObject key = Instantiate(keyObject);
            key.SetActive(false);
            key.transform.parent = transform.parent;
            
            CollectableController cc = key.GetComponent<CollectableController>();
            cc.keyId = keyId;
            
            ItemDropper id = chest.GetComponent<ItemDropper>();
            id.droppingItems.Add(new RandomDrop(key, 1, 1f));
            */
        }
    }
    
    
    void PlaceDoor(int doorId)
    {
        GameObject nlo = Instantiate(doorObject);
        
        float dx = 0f;
        float dy = 0f;
        float doorWidth = 1f;
        float doorHeight = corridorWidth;
        
        if (currentDirection == Vector2.right || currentDirection == Vector2.left)
        {
            dx = -currentRoom.transform.localScale.x/2f + doorWidth/2f;
            if (currentDirection == Vector2.left)
                dx = -dx;
        }
        if (currentDirection == Vector2.up || currentDirection == Vector2.down)
        {
            doorWidth = corridorWidth;
            doorHeight = 1f;
            
            dy = -currentRoom.transform.localScale.y/2f + doorHeight/2f;
            if (currentDirection == Vector2.down)
                dy = -dy;
        }
        
        nlo.transform.position = currentRoom.transform.position + Vector3.right * dx + Vector3.up * dy;
        nlo.transform.localScale = new Vector3(doorWidth, doorHeight, 1f);
        nlo.transform.parent = transform.parent;
        nlo.name = "Door " + doorId.ToString();
        
        DoorController dc = nlo.GetComponent<DoorController>();
        dc.doorId = doorId;
    }
    
    void PlaceKey(int keyId)
    {
        float dx = Random.Range(-1f, 1f) * (currentRoom.transform.localScale.x/2f-1f);
        float dy = Random.Range(-1f, 1f) * (currentRoom.transform.localScale.y/2f-1f);
        
        GameObject key = Instantiate(keyObject);
        key.transform.position = currentRoom.transform.position + Vector3.right * dx + Vector3.up * dy;
        key.transform.parent = transform.parent;
        key.name = "Key " + keyId.ToString();
        
        CollectableController cc = key.GetComponent<CollectableController>();
        cc.keyId = keyId;
    }
    
    void HideKey(int keyId)
    {
        GameObject key = Instantiate(keyObject);
        key.SetActive(false);
        key.transform.parent = transform.parent;
        key.name = "Key " + keyId.ToString();
        
        CollectableController cc = key.GetComponent<CollectableController>();
        cc.keyId = keyId;
        
        //get random element
        int index = Mathf.RoundToInt(Random.Range(0, droppingObjects.Count));
        ItemDropper id = droppingObjects[index].GetComponent<ItemDropper>();
        id.droppingItems.Add(new RandomDrop(key, 1, 1f));
    }
    
    
    void PickStartDirection()
    {
        float value = Random.value;
        
        if (value < 0.25f)
        {
            startDirection = Vector2.up;
        } else if (value < 0.5f)
        {
            startDirection = Vector2.right;
        }
        else if (value < 0.75f)
       {
           startDirection = Vector2.down;
       } else {
           startDirection = Vector2.left;
       }
    }
    
    void ChangeBuildDirection()
    {
        float value = Random.value;
        
        if (value < dirChangeValue)
        {
            Vector2 nextDirection = Vector2.zero;
            
            do {
                float value2 = Random.value;
                if (value2 < 0.25f)
                {
                    nextDirection = Vector2.up;
                } else if (value2 < 0.5f)
                {
                    nextDirection = Vector2.right;
                }
                else if (value2 < 0.75f)
               {
                   nextDirection = Vector2.down;
               } else {
                   nextDirection = Vector2.left;
               }
           } while (nextDirection == -startDirection || nextDirection == -currentDirection);
           currentDirection = nextDirection;
        }
    }
    
    public static void SetLevelNumber(int lvl)
    {
        if (self == null) return;
        self.levelNumber = lvl;
    }
}
