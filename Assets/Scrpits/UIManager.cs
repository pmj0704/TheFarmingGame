using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject Store;
    public GameObject Grid;

    public Image storeButton;

    public Sprite normalButton;
    public Sprite selectedButton;

    bool clickedOnce = false;

   public void OpenStore()
   {
    if(!clickedOnce)
    {
        storeButton.sprite = selectedButton;
        Store.SetActive(true);
        Grid.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
        Grid.transform.position = new Vector3(-3.65f, -1f, 0f);
        clickedOnce = true;
    }
    else
    {
        CloseStore();
        clickedOnce = false;
    }
   }

   public void CloseStore()
   {
       storeButton.sprite = normalButton;
       Store.SetActive(false);
       Grid.transform.localScale = new Vector3(1f, 1f, 1f);
       Grid.transform.position = new Vector3(0f, -1f, 0f);
      
       
   }
}
