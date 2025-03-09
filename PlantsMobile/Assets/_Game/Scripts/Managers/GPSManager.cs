using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSManager : MonoBehaviour
{
    public static GPSManager instance;

    [SerializeField] Vector2 m_DebugGPSLocation = Vector2.zero;
    [SerializeField] bool m_UseDebugGPSLocation = false;

    public Vector2 GPSPos 
    {  
        get
        {
            if (m_UseDebugGPSLocation)
            {
                // Setting origin to current player GPSpos for accuracy
                GPSEncoder.SetLocalOrigin(m_DebugGPSLocation);
                return m_DebugGPSLocation;
            }

            return _gpsPos;
        }
        private set
        {
            _gpsPos = value;
            RefreshGPSCoordSystem();
        }
    }
    private Vector2 _gpsPos;
    public Vector2 GPSPosUCS 
    {
        get 
        {
            // Converting spherical measurements of long and lat into flat measurements for unity
            return GPSEncoder.GPSToUCS(GPSPos);
        } 
    }

    private bool hasGPSInitialised = false;
    
    void Awake()
    {
       if(instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void RefreshGPSCoordSystem()
    {
        GPSEncoder.SetLocalOrigin(GPSPos);
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {  
        // Wait until the editor and unity remote are connected before starting a location service
        yield return new WaitForSeconds(5);
        
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            print("Location services are disabled...");
            yield break;
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            GPSPos = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
            hasGPSInitialised = true;

            // Access granted and location value could be retrieved
            //print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if(hasGPSInitialised)
        {
            GPSPos = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
            print($"latitude {GPSPos.x} longitude {GPSPos.y}");
        }
        
    }


}
