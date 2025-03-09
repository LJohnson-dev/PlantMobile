using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenScreen : MonoBehaviour
{
    [SerializeField] private GameObject m_SeedButtonParent;
    [SerializeField] private GardenSeedButton m_SeedButtonPrefab;

    private void Start()
    {
        foreach(PlantData plantType in PlantManager.instance.plantTypes)
        {
            SpawnButtonForPlantType(plantType);
        }
    }

    private void SpawnButtonForPlantType(PlantData data)
    {
        GardenSeedButton newButton = Instantiate(m_SeedButtonPrefab, m_SeedButtonParent.transform);
        newButton.SetSeedType(data);
    }
}
