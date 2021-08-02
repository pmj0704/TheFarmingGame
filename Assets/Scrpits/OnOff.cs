using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    [SerializeField] private GameObject Game;
    private bool On = true;
    
    public void turnOn()
    {
        if(On)
        {
            Game.transform.position = new Vector3(Game.transform.position.x, Game.transform.position.y, -20);
            On = false;
        }
        else
        {
            Game.transform.position = new Vector3(Game.transform.position.x, Game.transform.position.y, -8);
            On = true;
        }
    }
}
