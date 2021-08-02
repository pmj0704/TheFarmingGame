using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    bool isPlanted = false;
    SpriteRenderer plant;
    [SerializeField] private Sprite[] plantStages;
    BoxCollider2D plantColider;
    int plantStage = 0;
    float timeBtwStages = 2f;
    float timer;
    void Start()
    {
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantColider = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if(isPlanted){
        timer -= Time.deltaTime;
        if(timer < 0 && plantStage < plantStages.Length-1)
        {
            timer = timeBtwStages;
            plantStage++;
            UpdatePlant();
        }
        }
    }

    private void OnMouseDown()
    {
        if(isPlanted)
        {
            if(plantStage == plantStages.Length-1)
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
        timer = timeBtwStages;
        plant.gameObject.SetActive(true);
    }
    void UpdatePlant()
    {
        plant.sprite = plantStages[plantStage];
        plantColider.size = plant.sprite.bounds.size;
        plantColider.offset = new Vector2(0, plant.bounds.size.y/2);
    }
}
