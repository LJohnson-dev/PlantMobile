using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] OSMManager m_OSMManager;
    [SerializeField] private float m_NearDistanceMeters = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HUDDefinition targetHUD = HUDDefinition.NotNearGreenspace;

        Vector2 pos = GPSManager.instance.GPSPos;
        foreach (OsmSharp.Node node in m_OSMManager.GetGreenspaceNodes())
        {
            float distInMeters = CalculateDistance(pos, new Vector2((float)node.Latitude.Value, (float)node.Longitude.Value));
            if (distInMeters <= m_NearDistanceMeters)
            {
                targetHUD = HUDDefinition.NearGreenSpace;
               
                break;
            }
        }

        if(HudManager.Instance.ActiveHUD != HUDDefinition.Garden)
        {
            HudManager.Instance.SetActiveHUD(targetHUD);
        }
    }

    private float CalculateDistance(Vector2 coord1, Vector2 coord2)
    {
        int R = 6371; // Radius of Earth in kilometers
        var latRad1 = Mathf.Deg2Rad * coord1.x;
        var latRad2 = Mathf.Deg2Rad * coord2.x;
        var dLatRad = Mathf.Deg2Rad * (coord2.x - coord1.x);
        var dLongRad = Mathf.Deg2Rad * (coord2.y - coord1.y);

        var a = Mathf.Pow(Mathf.Sin(dLatRad / 2), 2) +
                Mathf.Pow(Mathf.Sin(dLongRad / 2), 2) * Mathf.Cos(latRad1) * Mathf.Cos(latRad2);
        var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        var totalDistKm = R * c;  //Distance in kilometers
        var totalDistMeters = totalDistKm * 1000; // Distance in meters

        return totalDistMeters;
    }


}
