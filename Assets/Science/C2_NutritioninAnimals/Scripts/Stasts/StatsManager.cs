using UnityEngine;

public class StatsManager : MonoBehaviour
{
    //This will work as statsmanager 
    //Task of this sricp is to house various data that needs to be shared at the end of the module
    // 1.Time taken to perform the activtiys
    // 2.Amount of foucs
    
    public static StatsManager _StatsManager { get; private set; }

    public int score = 0;
    public float act2time = 0;
    public int incorrectPlacementAct2 = 0;
    public float act4time = 0;
    public int incorrectPlacementAct4 = 0;
    
    //Amount of foucs 
    public float act1focus = 0;
    public float act3focus = 0;
    
    
    
    private void Awake()
    {
        if (_StatsManager == null)
        {
            _StatsManager = this;
            // Optional: DontDestroyOnLoad(gameObject); // If you need it to persist between scenes
        }
        else if (_StatsManager != this)
        {
            Destroy(gameObject);
        }
    }
    
    public void sendData()
    {
        //This will send all the collected data to the backend in the end of the module
    }
    
    
}

