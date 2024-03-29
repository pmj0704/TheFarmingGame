using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class FarmManager : MonoSingleton<FarmManager>
{
    public PlantItem selectedPlant;
    public bool isPlanting = false;
    public Text moneyTxt;
    
    public Color buyColor = Color.green;
    public Color cancleColor = Color.red;   

    public bool isSelecting = false;
    public int selectedTool = 0;

    public Image[] buttonsImg;
    public Sprite normalButton;
    public Sprite selectedButton;
    public GameObject[] buttons;
    public GameObject warningText;
    public TextMeshProUGUI warningTXT;
    public Sprite[] plotStages;
    public Color unavaiableColor = Color.red;
    public Color avaiableColor = Color.green;



    void Start()
    {
        moneyTxt.text = "$" + GameManager.Instance.currentUser.money;
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
            buttonsImg[toolNumber - 1].sprite = selectedButton;
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
                buttonsImg[selectedTool - 1].sprite = normalButton;
            }
            isSelecting = false;
            selectedTool = 0;
        }
    }

    public void Transaction(int vaule)
    {
        GameManager.Instance.currentUser.money += vaule;
        moneyTxt.text = "$" + GameManager.Instance.currentUser.money;
    }

}
