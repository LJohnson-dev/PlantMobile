using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantGrowth : MonoBehaviour
{
    private int currentGrowthStage = 0; //Track current growth stage
    private Image m_Image;
    private PlantData m_PlantData; //Ref to PlantData ScriptableObject

    // Start is called before the first frame update
    void Awake()
    {
        m_Image = GetComponent<Image>();
    }

    public void SetPlantData(PlantData plantData)
    {
        m_PlantData = plantData;
        UpdateGrowthStage();
    }

    // Update is called once per frame
    void UpdateGrowthStage()
    {
        if(currentGrowthStage < m_PlantData.m_growthStages.Length)
        {
            m_Image.sprite = m_PlantData.m_growthStages[currentGrowthStage];
        }
    }

    public void IncrementGrowth()
    {
        if(currentGrowthStage < m_PlantData.m_growthStages.Length - 1)
        {
            currentGrowthStage++;
            UpdateGrowthStage();
        }
    }

    public void DecrementGrowth()
    {
        if (currentGrowthStage > 0)
        {
            currentGrowthStage--;
            UpdateGrowthStage();
        }
    }
}
