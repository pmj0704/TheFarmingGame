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

    public Color avaiableColor = Color.green;
    public Color unavaiableColor = Color.red;

    private SpriteRenderer plot;

    private PlantObject selectedPlant;

    private FarmManager fm;   
    public Sprite[] plotStages;
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
        fm = transform.parent.GetComponent<FarmManager>();
        if(isBought)
        {
            plotStage = 3;
            
        plot.sprite = plotStages[plotStage];
        plotTimer = Random.Range(0f, 1800f);
        }
        else
        {
            plotStage = 0;

        plot.sprite = plotStages[plotStage];
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
        if(isPlanted && fm.isSelecting && plantStage == selectedPlant.plantStages.Length-1)
        {
            ErrorItemandHarvest();
        }
        if(isPlanted)
        {
            if(plantStage == selectedPlant.plantStages.Length-1 && !fm.isPlanting && !fm.isSelecting)
            {
                Harvest();
            }
        }
        else if(fm.isPlanting && fm.selectedPlant.plant.buyPrice <= fm.money && isBought)
        {
            Plant(fm.selectedPlant.plant);
        }
        if(fm.isSelecting)
        {
            switch (fm.selectedTool)
            {   
                case 1:
                if(isBought){
                    StartCoroutine(Watering());
                }
                    break;
                case 2:
                    if(fm.money >= 20 && !isBought && ((fm.money - 20) > 10))
                    {
                        fm.Transaction(-20);
                        isBought = true;
                        if(0 < (plotStage + 1) && plotStage < 3)
                        {
                            plotStage += 3;
                        }
                    plot.sprite = plotStages[plotStage];
                    plotTimer = Random.Range(0f, 1800f);
                    }
                    break;
                case 3:
                    if(fm.money >= 5 && isBought && ((fm.money - 5) > 10))
                    {
                        fm.Transaction(-5);
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
                        if (fm.money >= 20 && isBought && ((fm.money - 20) > 10))
                        {
                            fm.Transaction(-20);
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

        if(fm.isPlanting)
        {
            if(isPlanted || fm.selectedPlant.plant.buyPrice > fm.money || !isBought)
            {
                plot.color = unavaiableColor;
            }
            else
            {
                plot.color = avaiableColor;
            }
        }

        if(fm.isSelecting)
            {
                switch (fm.selectedTool)
                {
                case 1:
                if(isBought)
                        {
                            plot.color = avaiableColor;
                        }
                        else
                        {
                            plot.color = unavaiableColor;
                        }
                    break;
                case 3:
                        if(isBought && (fm.money - 5) > 10)
                        {
                            plot.color = avaiableColor;
                        }
                        else
                        {
                            plot.color = unavaiableColor;
                        }
                    break;
                case 2:
                        if(!isBought && ((fm.money - 20) > 10))
                        {
                            plot.color = avaiableColor;
                        }
                        else
                        {
                            plot.color = unavaiableColor;
                        }
                    break;
                case 4:
                        if(isBought && ((plotStage + 1) % 3 != 0))
                        {
                            plot.color = avaiableColor;
                        }
                        else
                        {
                            plot.color = unavaiableColor;
                        }
                break;
                case 5:
                    if(isBought & !lightBought)
                    {
                        plot.color = avaiableColor;
                    }
                    else
                    {
                        plot.color = unavaiableColor;
                    }
                    break;
                default:
                            plot.color = unavaiableColor;
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
        if(fm.isSelecting)
        {
            ErrorItemandHarvest();
        }
        else
        {
            isPlanted = false;
            plant.gameObject.SetActive(false);
            fm.Transaction(selectedPlant.sellPrice);
            isDry = true;
            if(plotStage < 9 && plotStage > 5)
            plotStage -= 3;
        plot.sprite = plotStages[plotStage];
        plotTimer = Random.Range(0f, 1800f);
            speed = 1f;
        }
    }
    void Plant(PlantObject newPlant)
    {
        selectedPlant = newPlant;
        isPlanted = true;

        fm.Transaction(-selectedPlant.buyPrice);

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
       
        plot.sprite = plotStages[plotStage];
        plotTimer = Random.Range(0f, 1800f);
    }
    private void ErrorItemandHarvest()
    {
            Debug.Log("ItSel");
            fm.warningTXT.text = "You must deselect the tool to harvest your crops";
            fm.warningText.GetComponent<Animator>().Play("Fade");
            fm.buttons[fm.selectedTool - 1].GetComponent<Animator>().Play("ItemIsUsing");
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
        
        plot.sprite = plotStages[plotStage];
        plotTimer = Random.Range(0f, 1800f);
        if(isPlanted) UpdatePlant();
    }
}
