using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daycycle : MonoBehaviour
{
    public int hour;
    public int min;
    public int sec;
    public float angle;

    public GameObject Sun;
    public GameObject Moon;

    public int imh;
    [SerializeField]
    private bool realTime = true;
    int timestamp;
    void Update ()
    {
        if(System.DateTime.Now.Hour < 18 &&  7 < System.DateTime.Now.Hour)
        {
            Sun.SetActive(true);
            Moon.SetActive(false);
        }
        else
        {
            Moon.SetActive(true);
            Sun.SetActive(false);
        }
        if (realTime)
        {
            hour = System.DateTime.Now.Hour;
            min = System.DateTime.Now.Minute;
            sec = System.DateTime.Now.Second;
            //var now = System.DateTime.Now.ToLocalTime();
            //var span = (now - new System.DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            //timestamp = (int)span.TotalSeconds;

        }


        angle = 0.004167f * ((hour * 3600) + (min * 60) + sec);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
