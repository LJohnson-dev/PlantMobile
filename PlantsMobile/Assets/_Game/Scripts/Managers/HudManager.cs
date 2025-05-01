using System;
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
    [SerializeField] private HUDDefinition StartScreen;

    public static HudManager Instance;
    public GameObject[] HudDefinitions = new GameObject[0]; 
    public HUDDefinition ActiveHUD { get; private set; }
    

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;

            SetActiveHUD(StartScreen);
        }
    }

    public void SetActiveHUD(HUDDefinition targetHUD)
    {
        for (int i = 0; i < HudDefinitions.Length; i++)
        {
            bool newState = (int)targetHUD == i;
            HudDefinitions[i].SetActive(newState);
        }
        ActiveHUD = targetHUD;
    }

    public void SetActiveHUD(int targetHUD)
    {
        SetActiveHUD((HUDDefinition)targetHUD);
    }
}
