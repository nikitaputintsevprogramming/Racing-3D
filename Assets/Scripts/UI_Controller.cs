using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] public Text textLaps;
    [SerializeField] public Text textTimer;

    [SerializeField] public int countLaps;      
    [SerializeField] public float time; 

    void Start()
    {
        textLaps = gameObject.transform.GetChild(0).GetComponent<Text>();
        textTimer = gameObject.transform.GetChild(1).GetComponent<Text>();
        
        StartLaps();
    }

    void Update()
    {
        Timer();
    }

    public void StartLaps()
    {
        countLaps = 1;
        textLaps.text = "Laps: " + countLaps;
        
    }

    public void AddLap()
    {
        countLaps++;
        textLaps.text = "Laps: " + countLaps;
    }

    public void Timer()
    {
        time = Time.fixedTime;
        textTimer.text = time.ToString("0.0");
    }
}
