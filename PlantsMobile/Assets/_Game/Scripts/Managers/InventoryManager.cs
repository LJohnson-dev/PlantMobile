using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private Dictionary<string, int> m_Items = new Dictionary<string, int>(); // Item name -> count ("RoseSeeds" -> 2)

    public delegate void OnItemCountChanged(string itemName, int newCount, int oldCount);

    public event OnItemCountChanged OnItemCountChange;


    void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // Make an entry for all seeds and start them at 0
        foreach (PlantData type in PlantManager.instance.plantTypes)
        {
            m_Items.Add(type.SeedName, 0);
        }
    }

    public void AddToInventory(string itemName, int count)
    {
        if(!m_Items.ContainsKey(itemName))
        {
            m_Items.Add(itemName, 0);
        }

        int oldCount = m_Items[itemName];
        m_Items[itemName] += count;

        OnItemCountChange?.Invoke(itemName, m_Items[itemName], oldCount);
    }

    public void RemoveFromInventory(string itemName, int count)
    {
        if (!m_Items.ContainsKey(itemName))
        {
            Debug.LogError($"{itemName} was not an entry in Inventory");
            return;
        }

        int oldCount = m_Items[itemName];
        m_Items[itemName] -= count;

        OnItemCountChange(itemName, m_Items[itemName], oldCount);
    }

    public int GetItemCount(string itemName)
    {
        if(!m_Items.ContainsKey(itemName))
        {
            return 0;
        }

        return m_Items[itemName];
    }
}
