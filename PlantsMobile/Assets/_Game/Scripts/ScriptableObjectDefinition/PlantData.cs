using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantData", menuName = "ScriptableObjects/PlantData", order = 1)]
public class PlantData : ScriptableObject
{

    public string m_plantName;
    public Sprite[] m_growthStages;
    public string SeedName
    {
        get { return $"{m_plantName}_Seed"; }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
