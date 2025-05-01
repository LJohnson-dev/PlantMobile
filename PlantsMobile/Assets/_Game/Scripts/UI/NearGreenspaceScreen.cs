using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NearGreenspaceScreen : MonoBehaviour
{
    [SerializeField] private Image seedPopup;
    [SerializeField] private Button seedPopupButton;

    private PlantData popupReceivedPlant;


    private void OnEnable()
    {
        CreateSeedPopup();
        PlantManager.instance.IncrementAllPlantsGrowth();
    }
    public void CreateSeedPopup()
    {
        seedPopupButton.gameObject.SetActive(true);
        popupReceivedPlant = PlantManager.instance.GetRandomPlantType();
        seedPopup.sprite = popupReceivedPlant.m_growthStages[0];
    }

    public void OnButtonClick()
    {
        seedPopupButton.gameObject.SetActive(false);
        InventoryManager.Instance.AddToInventory(popupReceivedPlant.SeedName, 1);
        popupReceivedPlant = null;
    }
}
