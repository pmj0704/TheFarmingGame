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

    bool isDry = true;
    public Sprite drySprite;
    public Sprite normalSprite;
    public Sprite unavaiableSprite;

    private Collider2D col;
    OnOff onoff;

    float speed = 1f;
    public bool isBought = true;

    void Start()
    {
        onoff = FindObjectOfType<OnOff>();
        plotTimer = Random.Range(660f, 7200f);
        plot = GetComponent<SpriteRenderer>();
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        plantColider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        plotStage = plotStages.Length -1;
        fm = transform.parent.GetComponent<FarmManager>();
        if(isBought)
        {
            plot.sprite = drySprite;
        }
        else
        {
            plot.sprite = unavaiableSprite;
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
        if(plotTimer < 0 && plotStage > 0)
        {
            plotStage--;
            UpdatePlot();
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
                    isDry = false;
                    plot.sprite = normalSprite;
                    if(isPlanted) UpdatePlant();
                }
                    break;
                case 2:
                    if(fm.money >= 20 && !isBought && !((fm.money - 20) <= 10))
                    {
                        fm.Transaction(-20);
                        isBought = true;
                        plot.sprite = drySprite;
                    }
                    break;
                case 3:
                    if(fm.money >= 30 && isBought && !((fm.money - 30) <= 10))
                    {
                        fm.Transaction(-30);
                        if(speed < 2) speed += .2f;
                    }
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
                        if(isBought && fm.money > 30)
                        {
                            plot.color = avaiableColor;
                        }
                        else
                        {
                            plot.color = unavaiableColor;
                        }
                    break;
                case 2:
                        if(!isBought && fm.money > 20)
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
        isPlanted = false;
        plant.gameObject.SetActive(false);
        fm.Transaction(selectedPlant.sellPrice);
        isDry = true;
        plot.sprite = drySprite;
        speed = 1f;
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
        plot.sprite = plotStages[plotStage];
        plotTimer = Random.Range(660f, 7200f);
    }
}
