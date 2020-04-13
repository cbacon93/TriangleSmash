using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public float timer = 0f;
    public bool running = false;
    public static TimerController self = null;
    
    UnityEngine.UI.Text tf;
    
    // Start is called before the first frame update
    void Awake()
    {
        timer = 0f;
        running = true;
        tf = GetComponent<UnityEngine.UI.Text>();
        self = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
            timer += Time.deltaTime;
            
        tf.text = GetTime();
        
    }
    
    public static void StopTimer()
    {
        if (self == null) return;
        self.running = false;
    }
    
    public static string GetTime()
    {
        if (self == null) return "";
        
        //update to text
        int minutes = Mathf.FloorToInt(self.timer/60f);
        int seconds = Mathf.FloorToInt(self.timer-minutes*60f);
        
        return minutes.ToString() + ":" + seconds.ToString("00");
    }
    
    public static float GetTimeRaw()
    {
        if (self == null) return 0f;
        return self.timer;
    }
    
    public static void AddTime(float time)
    {
        if (self == null) return;
        self.timer += time;
    }
    
    ~TimerController()
    {
        //self = null;
    }
}
