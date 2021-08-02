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

    public PlantObject selectedPlant;   

    SpriteRenderer plot;
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
        Debug.Log(plotStage);
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
            if(plantStage == selectedPlant.plantStages.Length-1)
            {
                Harvest();
            }
        }
        else
        {
            Plant();
        }
    }

    void Harvest()
    {
        Debug.Log("Harvested");
        isPlanted = false;
        plant.gameObject.SetActive(false);
    }
    void Plant()
    {
        Debug.Log("Planted");
        isPlanted = true;
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
