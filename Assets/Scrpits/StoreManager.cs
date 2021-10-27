using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public GameObject plantItem;
    List<PlantObject> plantObjects = new List<PlantObject>();

    private void Awake()
    {
        Debug.Log("1");
        var loadPlants = Resources.LoadAll("Plants", typeof(PlantObject));
        foreach (var plant in loadPlants)
        {
            plantObjects.Add((PlantObject)plant);
            Debug.Log("2");
        }
        plantObjects.Sort(SortByPrice);

        foreach (var plant in plantObjects)
        {
            PlantItem newPlant = Instantiate(plantItem, transform).GetComponent<PlantItem>();
            newPlant.plant = plant;
            Debug.Log("3");
        }
    }

    int SortByPrice(PlantObject plantObject1, PlantObject plantObject2)
    {
        return plantObject1.buyPrice.CompareTo(plantObject2.buyPrice);
    }

    int SortByTime(PlantObject plantObject1, PlantObject plantObject2)
    {
        return plantObject1.timeBtwStages.CompareTo(plantObject2.timeBtwStages);
    }
}
