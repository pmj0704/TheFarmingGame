[System.Serializable]
public class Plot
{
    public bool isPlanted = false;
    public int plantStage = 0;
    public float timer;
    public int plotStage;
    public float plotTimer;
    public bool isDry = true;
    public float speed = 1f;
    public bool isBought = false;
    public bool isDark = true;
    public bool lightBought = false;
    public PlantObject selectedPlant;
}