using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public static PlantManager instance;
    public List<PlantData> plantTypes;   // List of all plant types
    public PlantGrowth plantPrefab;
    [SerializeField] private GameObject m_PlantsRoot;
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

   public void PlacePlant(PlantData selectedPlantData, Vector2 pos)
    {
        //Instantiate a new plant from the prefab
        PlantGrowth newPlant = Instantiate(plantPrefab, m_PlantsRoot.transform);
        newPlant.gameObject.transform.position = pos;

        // Assign the selected PlantData to the new plant
        newPlant.SetPlantData(selectedPlantData);

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

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        if(GUILayout.Button("Increment"))
        {
            IncrementAllPlantsGrowth();
        }
        if (GUILayout.Button("Decrement"))
        {
            DecrementAllPlantsGrowth();
        }
        GUILayout.EndVertical();
    }

    public void IncrementAllPlantsGrowth()
    {
        foreach (PlantGrowth plant in activePlants)
        {
            plant.IncrementGrowth();
        }
    }

    void DecrementAllPlantsGrowth()
    {
        foreach (PlantGrowth plant in activePlants)
        {
            plant.DecrementGrowth();
        }
    }
}
