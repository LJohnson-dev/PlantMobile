using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    private Dictionary<string, int> m_Items = new Dictionary<string, int>(); // Item name -> count ("RoseSeeds" -> 2)

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
            DontDestroyOnLoad(gameObject);

            // Make an entry for all seeds and start them at 0
            foreach(PlantData type in PlantManager.instance.plantTypes)
            {
                m_Items.Add(type.SeedName, 0);
            }
        }
    }

    public void AddToInventory(string itemName, int count)
    {
        if(!m_Items.ContainsKey(itemName))
        {
            m_Items.Add(itemName, count);
            return;
        }
        m_Items[itemName] += count;
    }

    public void RemoveFromInventory(string itemName, int count)
    {
        if (!m_Items.ContainsKey(itemName))
        {
            Debug.LogError($"{itemName} was not an entry in Inventory");
            return;
        }
        m_Items[itemName] -= count;
    }
}
