using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlotManager : MonoBehaviour
{
    SpriteRenderer plant;
    BoxCollider2D plantColider;
    private SpriteRenderer plot;
    [SerializeField]
    private int plotNumber;
    private Collider2D col;
    OnOff onoff;
    void Start()
    {
        onoff = FindObjectOfType<OnOff>();
        plot = GetComponent<SpriteRenderer>();
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        plantColider = transform.GetChild(0).GetComponent<BoxCollider2D>();


        if (GameManager.Instance.currentUser.plotList[plotNumber].isBought)
        {
            plot.sprite = FarmManager.Instance.plotStages[GameManager.Instance.currentUser.plotList[plotNumber].plotStage];
            GameManager.Instance.currentUser.plotList[plotNumber].plotTimer = Random.Range(0f, 1800f);
        }
        else
        {
            plot.sprite = FarmManager.Instance.plotStages[GameManager.Instance.currentUser.plotList[plotNumber].plotStage];
            GameManager.Instance.currentUser.plotList[plotNumber].plotTimer = Random.Range(0f, 1800f);
        }


        if (GameManager.Instance.currentUser.plotList[plotNumber].isBought)
        {
            UpdatePlant();
            UpdatePlot();
        }
        if (GameManager.Instance.currentUser.plotList[plotNumber].isBought && GameManager.Instance.currentUser.plotList[plotNumber].isPlanted)
        {
            plant.gameObject.SetActive(true);
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

        GameManager.Instance.currentUser.plotList[plotNumber].plotTimer -= Time.deltaTime;
        if(GameManager.Instance.currentUser.plotList[plotNumber].plotTimer < 0 && GameManager.Instance.currentUser.plotList[plotNumber].isBought)
        {
            if(GameManager.Instance.currentUser.plotList[plotNumber].isDry)
            {
                if(GameManager.Instance.currentUser.plotList[plotNumber].plotStage < 5 && GameManager.Instance.currentUser.plotList[plotNumber].plotStage > 2)
                    GameManager.Instance.currentUser.plotList[plotNumber].plotStage++;
                UpdatePlot();

            }
            else
            {
                if(GameManager.Instance.currentUser.plotList[plotNumber].plotStage > 5 && GameManager.Instance.currentUser.plotList[plotNumber].plotStage < 8)
                {
                    GameManager.Instance.currentUser.plotList[plotNumber].plotStage++;
                UpdatePlot();
                }
            }
        }

        if(GameManager.Instance.currentUser.plotList[plotNumber].isPlanted && !GameManager.Instance.currentUser.plotList[plotNumber].isDry){
            GameManager.Instance.currentUser.plotList[plotNumber].timer -= GameManager.Instance.currentUser.plotList[plotNumber].speed * Time.deltaTime;
        if(GameManager.Instance.currentUser.plotList[plotNumber].timer < 0 && GameManager.Instance.currentUser.plotList[plotNumber].plantStage < GameManager.Instance.currentUser.plotList[plotNumber].selectedPlant.plantStages.Length - 1)
        {
                GameManager.Instance.currentUser.plotList[plotNumber].timer = GameManager.Instance.currentUser.plotList[plotNumber].selectedPlant.timeBtwStages;
                GameManager.Instance.currentUser.plotList[plotNumber].plantStage++;
            UpdatePlant();
        }
        }
    }

    private void OnMouseDown()
    {
        if(GameManager.Instance.currentUser.plotList[plotNumber].isPlanted && FarmManager.Instance.isSelecting && GameManager.Instance.currentUser.plotList[plotNumber].plantStage == GameManager.Instance.currentUser.plotList[plotNumber].selectedPlant.plantStages.Length-1)
        {
            ErrorItemandHarvest();
        }
        if(GameManager.Instance.currentUser.plotList[plotNumber].isPlanted)
        {
            if(GameManager.Instance.currentUser.plotList[plotNumber].plantStage == GameManager.Instance.currentUser.plotList[plotNumber].selectedPlant.plantStages.Length-1 && !FarmManager.Instance.isPlanting && !FarmManager.Instance.isSelecting)
            {
                Harvest();
            }
        }
        else if(FarmManager.Instance.isPlanting && FarmManager.Instance.selectedPlant.plant.buyPrice <= GameManager.Instance.currentUser.money && GameManager.Instance.currentUser.plotList[plotNumber].isBought)
        {
            Plant(FarmManager.Instance.selectedPlant.plant);
        }
        if(FarmManager.Instance.isSelecting)
        {
            switch (FarmManager.Instance.selectedTool)
            {   
                case 1:
                if(GameManager.Instance.currentUser.plotList[plotNumber].isBought){
                    StartCoroutine(Watering());
                }
                    break;
                case 2:
                    if(GameManager.Instance.currentUser.money >= 20 && !GameManager.Instance.currentUser.plotList[plotNumber].isBought && ((GameManager.Instance.currentUser.money - 20) >= 10))
                    {
                        FarmManager.Instance.Transaction(-20);
                        GameManager.Instance.currentUser.plotList[plotNumber].isBought = true;
                        if(0 < (GameManager.Instance.currentUser.plotList[plotNumber].plotStage + 1) && GameManager.Instance.currentUser.plotList[plotNumber].plotStage < 3)
                        {
                            GameManager.Instance.currentUser.plotList[plotNumber].plotStage += 3;
                        }
                    plot.sprite = FarmManager.Instance.plotStages[GameManager.Instance.currentUser.plotList[plotNumber].plotStage];
                        GameManager.Instance.currentUser.plotList[plotNumber].plotTimer = Random.Range(0f, 1800f);
                    }
                    break;
                case 3:
                    if(GameManager.Instance.currentUser.money >= 5 && GameManager.Instance.currentUser.plotList[plotNumber].isBought && ((GameManager.Instance.currentUser.money - 5) >= 10))
                    {
                        FarmManager.Instance.Transaction(-5);
                        if(GameManager.Instance.currentUser.plotList[plotNumber].speed < 2) GameManager.Instance.currentUser.plotList[plotNumber].speed += .2f;
                    }
                    break;
                case 4:
                    if(GameManager.Instance.currentUser.plotList[plotNumber].isBought && ((GameManager.Instance.currentUser.plotList[plotNumber].plotStage + 1) % 3 != 1))
                    {
                        GameManager.Instance.currentUser.plotList[plotNumber].plotStage--;
                          switch ((GameManager.Instance.currentUser.plotList[plotNumber].plotStage +1) % 3)
                                {
                                    case 1:
                                GameManager.Instance.currentUser.plotList[plotNumber].speed += 0.1f;
                                        break;
                                    case 2:
                                GameManager.Instance.currentUser.plotList[plotNumber].speed += 0.2f;
                                        break;
                                    default: break;
                                 }
                        UpdatePlot();
                    }
                    break;
                case 5:
                    if (!GameManager.Instance.currentUser.plotList[plotNumber].lightBought)
                    {
                        if (GameManager.Instance.currentUser.money >= 20 && GameManager.Instance.currentUser.plotList[plotNumber].isBought && ((GameManager.Instance.currentUser.money - 20) >= 10))
                        {
                            FarmManager.Instance.Transaction(-20);
                            GameManager.Instance.currentUser.plotList[plotNumber].lightBought = true;
                            GameManager.Instance.currentUser.plotList[plotNumber].isDark = false;
                            plot.gameObject.transform.GetChild(3).gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        if (GameManager.Instance.currentUser.plotList[plotNumber].isDark)
                        {
                            plot.gameObject.transform.GetChild(3).gameObject.SetActive(true);
                            GameManager.Instance.currentUser.plotList[plotNumber].isDark = false;
                        }
                        else
                        {
                            plot.gameObject.transform.GetChild(3).gameObject.SetActive(false);
                            GameManager.Instance.currentUser.plotList[plotNumber].isDark = true;
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
            if(GameManager.Instance.currentUser.plotList[plotNumber].isPlanted || FarmManager.Instance.selectedPlant.plant.buyPrice > GameManager.Instance.currentUser.money || !GameManager.Instance.currentUser.plotList[plotNumber].isBought)
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
                if(GameManager.Instance.currentUser.plotList[plotNumber].isBought)
                        {
                            plot.color = FarmManager.Instance.avaiableColor;
                        }
                        else
                        {
                            plot.color = FarmManager.Instance.unavaiableColor;
                        }
                    break;
                case 3:
                        if(GameManager.Instance.currentUser.plotList[plotNumber].isBought && (GameManager.Instance.currentUser.money - 5) >= 10)
                        {
                            plot.color = FarmManager.Instance.avaiableColor;
                        }
                        else
                        {
                            plot.color = FarmManager.Instance.unavaiableColor;
                        }
                    break;
                case 2:
                        if(!GameManager.Instance.currentUser.plotList[plotNumber].isBought && ((GameManager.Instance.currentUser.money - 20) >= 10))
                        {
                            plot.color = FarmManager.Instance.avaiableColor;
                        }
                        else
                        {
                            plot.color = FarmManager.Instance.unavaiableColor;
                        }
                    break;
                case 4:
                        if(GameManager.Instance.currentUser.plotList[plotNumber].isBought && ((GameManager.Instance.currentUser.plotList[plotNumber].plotStage + 1) % 3 != 0))
                        {
                            plot.color = FarmManager.Instance.avaiableColor;
                        }
                        else
                        {
                            plot.color = FarmManager.Instance.unavaiableColor;
                        }
                break;
                case 5:
                    if(GameManager.Instance.currentUser.plotList[plotNumber].isBought & !GameManager.Instance.currentUser.plotList[plotNumber].lightBought & ((GameManager.Instance.currentUser.money - 20) >= 10))
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
            GameManager.Instance.currentUser.plotList[plotNumber].isPlanted = false;
            plant.gameObject.SetActive(false);
            FarmManager.Instance.Transaction(GameManager.Instance.currentUser.plotList[plotNumber].selectedPlant.sellPrice);
            GameManager.Instance.currentUser.plotList[plotNumber].isDry = true;
            if(GameManager.Instance.currentUser.plotList[plotNumber].plotStage < 9 && GameManager.Instance.currentUser.plotList[plotNumber].plotStage > 5)
                GameManager.Instance.currentUser.plotList[plotNumber].plotStage -= 3;
        plot.sprite = FarmManager.Instance.plotStages[GameManager.Instance.currentUser.plotList[plotNumber].plotStage];
            GameManager.Instance.currentUser.plotList[plotNumber].plotTimer = Random.Range(0f, 1800f);
            GameManager.Instance.currentUser.plotList[plotNumber].speed = 1f;
        }
    }
    void Plant(PlantObject newPlant)
    {
        GameManager.Instance.currentUser.plotList[plotNumber].selectedPlant = newPlant;
        GameManager.Instance.currentUser.plotList[plotNumber].isPlanted = true;

        FarmManager.Instance.Transaction(-GameManager.Instance.currentUser.plotList[plotNumber].selectedPlant.buyPrice);

        GameManager.Instance.currentUser.plotList[plotNumber].plantStage = 0;
        UpdatePlant();
        GameManager.Instance.currentUser.plotList[plotNumber].timer = GameManager.Instance.currentUser.plotList[plotNumber].selectedPlant.timeBtwStages;
        plant.gameObject.SetActive(true);
    }
    void UpdatePlant()
    {
        if(GameManager.Instance.currentUser.plotList[plotNumber].isDry)
        {
            plant.sprite = GameManager.Instance.currentUser.plotList[plotNumber].selectedPlant.dryPlanted;
        }
        else
        {
            plant.sprite = GameManager.Instance.currentUser.plotList[plotNumber].selectedPlant.plantStages[GameManager.Instance.currentUser.plotList[plotNumber].plantStage];
        }
        plantColider.size = plant.sprite.bounds.size;
        plantColider.offset = new Vector2(0, plant.bounds.size.y/2);
    }
    void UpdatePlot()
    {
        if((GameManager.Instance.currentUser.plotList[plotNumber].plotStage +1) % 3 == 1)
        {
            switch ((GameManager.Instance.currentUser.plotList[plotNumber].plotStage +1) % 3)
        {
            case 1:
                    GameManager.Instance.currentUser.plotList[plotNumber].speed += 0.1f;
            break;
            case 2:
                    GameManager.Instance.currentUser.plotList[plotNumber].speed += 0.2f;
            break;
            default:
            break;
        }
        }
        else
        {
            switch ((GameManager.Instance.currentUser.plotList[plotNumber].plotStage +1) % 3)
        {   
            case 1:
                    GameManager.Instance.currentUser.plotList[plotNumber].speed -= 0.1f;
            break;
            case 2:
                    GameManager.Instance.currentUser.plotList[plotNumber].speed -= 0.2f;
            break;
            default:
            break;
        }
        }
       
        plot.sprite = FarmManager.Instance.plotStages[GameManager.Instance.currentUser.plotList[plotNumber].plotStage];
        GameManager.Instance.currentUser.plotList[plotNumber].plotTimer = Random.Range(0f, 1800f);
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
        GameManager.Instance.currentUser.plotList[plotNumber].isDry = false;
        if(2 < GameManager.Instance.currentUser.plotList[plotNumber].plotStage && GameManager.Instance.currentUser.plotList[plotNumber].plotStage < 6)
        {
            GameManager.Instance.currentUser.plotList[plotNumber].plotStage += 3;
        }
        
        plot.sprite = FarmManager.Instance.plotStages[GameManager.Instance.currentUser.plotList[plotNumber].plotStage];
        GameManager.Instance.currentUser.plotList[plotNumber].plotTimer = Random.Range(0f, 1800f);
        if(GameManager.Instance.currentUser.plotList[plotNumber].isPlanted) UpdatePlant();
    }
}
