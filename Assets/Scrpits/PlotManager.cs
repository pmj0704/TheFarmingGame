using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlotManager : MonoBehaviour
{
    bool isPlanted = false;
    SpriteRenderer plant;
    BoxCollider2D plantColider;

    int plantStage = 0;
    float timer;


    private SpriteRenderer plot;

    private PlantObject selectedPlant;
    int plotStage;
    float plotTimer;

    bool isDry = true;

    private Collider2D col;
    OnOff onoff;

    float speed = 1f;
    public bool isBought = true;
    private bool isDark = true;
    private bool lightBought = false;
    void Start()
    {
        onoff = FindObjectOfType<OnOff>();
        plotTimer = Random.Range(0f, 1800f);
        plot = GetComponent<SpriteRenderer>();
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        plantColider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        if(isBought)
        {
            plotStage = 3;
            
        plot.sprite = FarmManager.Instance.plotStages[plotStage];
        plotTimer = Random.Range(0f, 1800f);
        }
        else
        {
            plotStage = 0;

        plot.sprite = FarmManager.Instance.plotStages[plotStage];
        plotTimer = Random.Range(0f, 1800f);
        }
    }

    void Update()
    {

        if(onoff.On)
        {
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }

        plotTimer -= Time.deltaTime;
        if(plotTimer < 0 && isBought)
        {
            if(isDry)
            {
                if(plotStage < 5 && plotStage > 2)
                plotStage++;
                UpdatePlot();

            }
            else
            {
                if(plotStage > 5 && plotStage < 8)
                {
                    plotStage++;
                UpdatePlot();
                }
            }
        }

        if(isPlanted && !isDry){
        timer -= speed * Time.deltaTime;
        if(timer < 0 && plantStage < selectedPlant.plantStages.Length - 1)
        {
            timer = selectedPlant.timeBtwStages;
            plantStage++;
            UpdatePlant();
        }
        }
    }

    private void OnMouseDown()
    {
        if(isPlanted && FarmManager.Instance.isSelecting && plantStage == selectedPlant.plantStages.Length-1)
        {
            ErrorItemandHarvest();
        }
        if(isPlanted)
        {
            if(plantStage == selectedPlant.plantStages.Length-1 && !FarmManager.Instance.isPlanting && !FarmManager.Instance.isSelecting)
            {
                Harvest();
            }
        }
        else if(FarmManager.Instance.isPlanting && FarmManager.Instance.selectedPlant.plant.buyPrice <= FarmManager.Instance.money && isBought)
        {
            Plant(FarmManager.Instance.selectedPlant.plant);
        }
        if(FarmManager.Instance.isSelecting)
        {
            switch (FarmManager.Instance.selectedTool)
            {   
                case 1:
                if(isBought){
                    StartCoroutine(Watering());
                }
                    break;
                case 2:
                    if(FarmManager.Instance.money >= 20 && !isBought && ((FarmManager.Instance.money - 20) >= 10))
                    {
                        FarmManager.Instance.Transaction(-20);
                        isBought = true;
                        if(0 < (plotStage + 1) && plotStage < 3)
                        {
                            plotStage += 3;
                        }
                    plot.sprite = FarmManager.Instance.plotStages[plotStage];
                    plotTimer = Random.Range(0f, 1800f);
                    }
                    break;
                case 3:
                    if(FarmManager.Instance.money >= 5 && isBought && ((FarmManager.Instance.money - 5) >= 10))
                    {
                        FarmManager.Instance.Transaction(-5);
                        if(speed < 2) speed += .2f;
                    }
                    break;
                case 4:
                    if( isBought && ((plotStage + 1) % 3 != 1))
                    {
                        plotStage--;
                          switch ((plotStage+1) % 3)
                                {
                                    case 1:
                                        speed += 0.1f;
                                        break;
                                    case 2:
                                        speed += 0.2f;
                                        break;
                                    default: break;
                                 }
                        UpdatePlot();
                    }
                    break;
                case 5:
                    if (!lightBought)
                    {
                        if (FarmManager.Instance.money >= 20 && isBought && ((FarmManager.Instance.money - 20) >= 10))
                        {
                            FarmManager.Instance.Transaction(-20);
                            lightBought = true;
                            isDark = false;
                            plot.gameObject.transform.GetChild(3).gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        if (isDark)
                        {
                            plot.gameObject.transform.GetChild(3).gameObject.SetActive(true);
                            isDark = false;
                        }
                        else
                        {
                            plot.gameObject.transform.GetChild(3).gameObject.SetActive(false);
                            isDark = true;
                        }
                    }
                    break;
                case 6:
                    break;
                default:
                    break;
            }
        }
    }

    private void OnMouseOver()
    {

        if(FarmManager.Instance.isPlanting)
        {
            if(isPlanted || FarmManager.Instance.selectedPlant.plant.buyPrice > FarmManager.Instance.money || !isBought)
            {
                plot.color = FarmManager.Instance.unavaiableColor;
            }
            else
            {
                plot.color = FarmManager.Instance.avaiableColor;
            }
        }

        if(FarmManager.Instance.isSelecting)
            {
                switch (FarmManager.Instance.selectedTool)
                {
                case 1:
                if(isBought)
                        {
                            plot.color = FarmManager.Instance.avaiableColor;
                        }
                        else
                        {
                            plot.color = FarmManager.Instance.unavaiableColor;
                        }
                    break;
                case 3:
                        if(isBought && (FarmManager.Instance.money - 5) >= 10)
                        {
                            plot.color = FarmManager.Instance.avaiableColor;
                        }
                        else
                        {
                            plot.color = FarmManager.Instance.unavaiableColor;
                        }
                    break;
                case 2:
                        if(!isBought && ((FarmManager.Instance.money - 20) >= 10))
                        {
                            plot.color = FarmManager.Instance.avaiableColor;
                        }
                        else
                        {
                            plot.color = FarmManager.Instance.unavaiableColor;
                        }
                    break;
                case 4:
                        if(isBought && ((plotStage + 1) % 3 != 0))
                        {
                            plot.color = FarmManager.Instance.avaiableColor;
                        }
                        else
                        {
                            plot.color = FarmManager.Instance.unavaiableColor;
                        }
                break;
                case 5:
                    if(isBought & !lightBought & ((FarmManager.Instance.money - 20) >= 10))
                    {
                        plot.color = FarmManager.Instance.avaiableColor;
                    }
                    else
                    {
                        plot.color = FarmManager.Instance.unavaiableColor;
                    }
                    break;
                default:
                            plot.color = FarmManager.Instance.unavaiableColor;
                        break;
                }
            }
    }

    private void OnMouseExit()
    {
        plot.color = Color.white;
    }

    void Harvest()
    {
        if(FarmManager.Instance.isSelecting)
        {
            ErrorItemandHarvest();
        }
        else
        {
            isPlanted = false;
            plant.gameObject.SetActive(false);
            FarmManager.Instance.Transaction(selectedPlant.sellPrice);
            isDry = true;
            if(plotStage < 9 && plotStage > 5)
            plotStage -= 3;
        plot.sprite = FarmManager.Instance.plotStages[plotStage];
        plotTimer = Random.Range(0f, 1800f);
            speed = 1f;
        }
    }
    void Plant(PlantObject newPlant)
    {
        selectedPlant = newPlant;
        isPlanted = true;

        FarmManager.Instance.Transaction(-selectedPlant.buyPrice);

        plantStage = 0;
        UpdatePlant();
        timer = selectedPlant.timeBtwStages;
        plant.gameObject.SetActive(true);
    }
    void UpdatePlant()
    {
        if(isDry)
        {
            plant.sprite = selectedPlant.dryPlanted;
        }
        else
        {
            plant.sprite = selectedPlant.plantStages[plantStage];
        }
        plantColider.size = plant.sprite.bounds.size;
        plantColider.offset = new Vector2(0, plant.bounds.size.y/2);
    }
    void UpdatePlot()
    {
        if((plotStage+1) % 3 == 1)
        {
            switch ((plotStage+1) % 3)
        {
            case 1:
                speed += 0.1f;
            break;
            case 2:
                speed += 0.2f;
            break;
            default:
            break;
        }
        }
        else
        {
            switch ((plotStage+1) % 3)
        {   
            case 1:
                speed -= 0.1f;
            break;
            case 2:
                speed -= 0.2f;
            break;
            default:
            break;
        }
        }
       
        plot.sprite = FarmManager.Instance.plotStages[plotStage];
        plotTimer = Random.Range(0f, 1800f);
    }
    private void ErrorItemandHarvest()
    {
            Debug.Log("ItSel");
            FarmManager.Instance.warningTXT.text = "You must deselect the tool to harvest your crops";
            FarmManager.Instance.warningText.GetComponent<Animator>().Play("Fade");
            FarmManager.Instance.buttons[FarmManager.Instance.selectedTool - 1].GetComponent<Animator>().Play("ItemIsUsing");
    }
    private IEnumerator Watering()
    {
        plot.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds (1.3f);
        plot.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        isDry = false;
        if(2 < plotStage && plotStage < 6)
        {
            plotStage += 3;
        }
        
        plot.sprite = FarmManager.Instance.plotStages[plotStage];
        plotTimer = Random.Range(0f, 1800f);
        if(isPlanted) UpdatePlant();
    }
}
