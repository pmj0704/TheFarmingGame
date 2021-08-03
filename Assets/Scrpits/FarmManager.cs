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

    public bool isSelecting = false;
    public int selectedTool = 0;

    public Image[] butttonsImg;
    public Sprite normalButton;
    public Sprite selectedButton;

    void Start()
    {
        moneyTxt.text = "$" + money;
    }

    public void SelectedPlant(PlantItem newPlant)
    {
        if(selectedPlant == newPlant)
        {
            CheckSelection();
        }
        else
        {
            CheckSelection();
            selectedPlant = newPlant;
            selectedPlant.btnImage.color = cancleColor;
            selectedPlant.btnTxt.text = "Cancle";
            isPlanting = true;
        }
    }

    public void SelectTool(int toolNumber)
    {
        if(toolNumber == selectedTool)
        {
            CheckSelection();
        }
        else
        {
            CheckSelection();
            isSelecting = true;
            selectedTool = toolNumber;
            butttonsImg[toolNumber - 1].sprite = selectedButton;
        }
    }

    private void CheckSelection()
    {
        if(isPlanting)
        {
            isPlanting = false;
             if(selectedPlant != null)
            {
                selectedPlant.btnImage.color = buyColor;
                selectedPlant.btnTxt.text = "Buy";
            }
            selectedPlant = null;
        }
        if(isSelecting)
        {
            if(selectedTool > 0)
            {
                butttonsImg[selectedTool - 1].sprite = normalButton;
            }
            isSelecting = false;
            selectedTool = 0;
        }
    }

    public void Transaction(int vaule)
    {
        money += vaule;
        moneyTxt.text = "$" + money;
    }

}
