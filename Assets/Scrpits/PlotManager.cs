using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        plotTimer = Random.Range(660f, 7200f);
        plot = GetComponent<SpriteRenderer>();
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantColider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        plotStage = plotStages.Length -1;
        fm = transform.parent.GetComponent<FarmManager>();
    }

    void Update()
    {

        plotTimer -= Time.deltaTime;
        if(plotTimer < 0 && plotStage > 0)
        {
            plotStage--;
            UpdatePlot();
        }

        if(isPlanted){
        timer -= Time.deltaTime;
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
        if(isPlanted)
        {
            if(plantStage == selectedPlant.plantStages.Length-1 && !fm.isPlanting)
            {
                Harvest();
            }
        }
        else if(fm.isPlanting && fm.selectedPlant.plant.buyPrice <= fm.money)
        {
            Plant(fm.selectedPlant.plant);
        }
    }

    private void OnMouseOver()
    {
        if(fm.isPlanting)
        {
            if(isPlanted || fm.selectedPlant.plant.buyPrice > fm.money)
            {
                plot.color = unavaiableColor;
            }
            else
            {
                plot.color = avaiableColor;
            }
        }
    }

    private void OnMouseExit()
    {
        plot.color = Color.white;
    }

    void Harvest()
    {
        isPlanted = false;
        plant.gameObject.SetActive(false);
        fm.Transaction(selectedPlant.sellPrice);
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
        plant.sprite = selectedPlant.plantStages[plantStage];
        plantColider.size = plant.sprite.bounds.size;
        plantColider.offset = new Vector2(0, plant.bounds.size.y/2);
    }
    void UpdatePlot()
    {
        plot.sprite = plotStages[plotStage];
        plotTimer = Random.Range(660f, 7200f);
    }
}
