using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NearGreenspaceScreen : MonoBehaviour
{
    public Image seedPopup;
    [SerializeField] private Button seedPopupButton;
    private PlantData popupReceivedPlant;

    // Start is called before the first frame update
    void Start()
    {
        CreateSeedPopup();
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
        Inventory.Instance.AddToInventory(popupReceivedPlant.SeedName, 1);
        popupReceivedPlant = null;
    }
}
