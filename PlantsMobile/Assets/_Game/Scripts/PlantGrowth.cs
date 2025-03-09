using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    public PlantData plantData; //Ref to PlantData ScriptableObject

    private int currentGrowthStage = 0; //Track current growth stage
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateGrowthStage();
    }

    // Update is called once per frame
    void UpdateGrowthStage()
    {
        if(currentGrowthStage < plantData.m_growthStages.Length)
        {
            spriteRenderer.sprite = plantData.m_growthStages[currentGrowthStage];
        }
    }

    public void IncrementGrowth()
    {
        if(currentGrowthStage < plantData.m_growthStages.Length - 1)
        {
            currentGrowthStage++;
            UpdateGrowthStage();
        }
    }
}
