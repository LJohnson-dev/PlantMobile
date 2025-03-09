using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public static PlantManager instance;
    public List<PlantData> plantTypes;   // List of all plant types
    public PlantGrowth plantPrefab;     

    private List<PlantGrowth> activePlants = new List<PlantGrowth> (); //List of all active plant instances

    
    void Awake()
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

   public void PlacePlant(PlantData selectedPlantData)
    {
        //Instantiate a new plant from the prefab
        PlantGrowth newPlant = Instantiate(plantPrefab);

        // Assign the selected PlantData to the new plant
        newPlant.plantData = selectedPlantData;

        activePlants.Add(newPlant); // Add new plant to list of active plants  
    }

    public PlantData GetRandomPlantType()
    {
        if (plantTypes.Count == 0)
        {
            Debug.LogError("No plant types found");
            return null;
        }

        // Generate random index from start of list to end of list
        int randomIndex = Random.Range(0, plantTypes.Count);

        // Get the PlantData object at the randomly generated index
        return plantTypes[randomIndex];
    }

    
    void IncrementAllPlantsGrowth()
    {
        foreach (PlantGrowth plant in activePlants)
        {
            plant.IncrementGrowth();
        }
    }
}
