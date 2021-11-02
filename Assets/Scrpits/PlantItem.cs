using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantItem : MonoBehaviour
{
    public PlantObject plant;

    public Text nameTxt;
    public Text priceTxt;
    public Image icon;

    public Image btnImage;
    public Text  btnTxt;

    void Start()
    {
        InitializeUI();
    }

    public void BuyPlant()
    {
        Debug.Log("Bought" + plant.plantName);
        FarmManager.Instance.SelectedPlant(this);
    }

    void InitializeUI()
    {
        nameTxt.text = plant.plantName;
        priceTxt.text = "$" + plant.buyPrice;
        icon.sprite = plant.icon;
    }
}
