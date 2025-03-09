using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HUDDefinition
{
    NearGreenSpace,
    NotNearGreenspace,
    Garden
}

public class HudManager : MonoBehaviour
{
    public static HudManager instance;
    public GameObject[] HudDefinitions = new GameObject[0]; 
    

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetActiveHUD(HUDDefinition targetHUD)
    {
        for(int i = 0; i < HudDefinitions.Length; i++)
        {
            bool newState = (int)targetHUD == i;
            HudDefinitions[i].SetActive(newState);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
