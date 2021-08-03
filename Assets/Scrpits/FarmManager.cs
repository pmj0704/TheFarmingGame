using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmManager : MonoBehaviour
{
    public PlantItem selectedPlant;
    public bool isPlanting = false;
    public int money = 100;
    public Text moneyTxt;
    
    public Color buyColor = Color.green;
    public Color cancleColor = Color.red;   

    void Start()
    {
        moneyTxt.text = "$" + money;
    }

    public void SelectedPlant(PlantItem newPlant)
    {
        if(selectedPlant == newPlant)
        {
            Debug.Log("Deselected" + selectedPlant.plant.plantName);
            selectedPlant.btnImage.color = buyColor;
            selectedPlant.btnTxt.text = "Buy";
            selectedPlant = null;
            isPlanting = false;
        }
        else
        {
            if(selectedPlant != null)
            {
                selectedPlant.btnImage.color = buyColor;
                selectedPlant.btnTxt.text = "Buy";
            }
            selectedPlant = newPlant;
            selectedPlant.btnImage.color = cancleColor;
            selectedPlant.btnTxt.text = "Cancle";
            Debug.Log("selected" + selectedPlant.plant.plantName);
            isPlanting = true;
        }
    }

    public void Transaction(int vaule)
    {
        money += vaule;
        moneyTxt.text = "$" + money;
    }

}
