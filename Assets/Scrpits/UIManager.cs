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
        Grid.transform.localScale = new Vector2(0.75f, 0.75f);
        Grid.transform.position = new Vector2(-3.65f, 0);
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
       Grid.transform.localScale = new Vector2(1f, 1f);
       Grid.transform.position = new Vector2(0, 0);
      
       
   }
}
