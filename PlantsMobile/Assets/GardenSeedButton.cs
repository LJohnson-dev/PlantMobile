using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GardenSeedButton : MonoBehaviour
{
    [SerializeField] private Button m_DragButton;
    [SerializeField] private Image m_SeedImage;
    [SerializeField] private TMP_Text m_SeedCountText;

    public void SetSeedType(PlantData newTarget)
    { 
        m_CurrentSeedType = newTarget;
        m_SeedImage.sprite = newTarget.m_growthStages[0];

        Inventory.Instance.OnItemCountChange += Instance_OnItemCountChange;
        UpdateText();
    }

    private void Instance_OnItemCountChange(string itemName, int newCount, int oldCount)
    {
        if (itemName == m_CurrentSeedType.SeedName)
        {
            UpdateText();
        }
    }

    private void UpdateText()
    {
        if(m_CurrentSeedType == null) return;

        int count = Inventory.Instance.GetItemCount(m_CurrentSeedType.SeedName);
        m_SeedCountText.text = $"x{count}";

        gameObject.SetActive(count != 0);
    }

    private PlantData m_CurrentSeedType = null;
}
