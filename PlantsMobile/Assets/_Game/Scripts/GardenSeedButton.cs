using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GardenSeedButton : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Button m_DragButton;
    [SerializeField] private Image m_SeedImage;
    [SerializeField] private TMP_Text m_SeedCountText;
    [SerializeField] private RectTransform m_RectTransform;
    private Vector2 m_StartPos;

    public void SetSeedType(PlantData newTarget)
    { 
        m_CurrentSeedType = newTarget;
        m_SeedImage.sprite = newTarget.m_growthStages[0];

        InventoryManager.Instance.OnItemCountChange += Instance_OnItemCountChange;
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

        int count = InventoryManager.Instance.GetItemCount(m_CurrentSeedType.SeedName);
        m_SeedCountText.text = $"x{count}";

        gameObject.SetActive(count != 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // move image
        m_RectTransform.anchoredPosition += eventData.delta;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_DragButton.image.color = new Color32(255, 255, 255, 170);

        // record image pos
        m_StartPos = m_RectTransform.anchoredPosition; 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // tell plant manager plant seed type at current pos
        PlantManager.instance.PlacePlant(m_CurrentSeedType, m_RectTransform.transform.position);

        // restore image pos
        m_RectTransform.anchoredPosition = m_StartPos;

        m_DragButton.image.color = new Color32(255, 255, 255, 255);

        // decrement from inventory
        InventoryManager.Instance.RemoveFromInventory(m_CurrentSeedType.SeedName, 1);
    }


    private PlantData m_CurrentSeedType = null;
}
