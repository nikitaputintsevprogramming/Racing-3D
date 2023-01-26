using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] public Text textLaps;
    [SerializeField] public Text textTimer;
    [SerializeField] public Text textBestTimeOfLap;

    [SerializeField] public int countLaps;      
    [SerializeField] public float time; 
    [SerializeField] public float StartTime;  
    [SerializeField] public float BestTime;     
    [SerializeField] public int BestLap;     

    [SerializeField] List<float> TimesOfLaps = new List<float>();

    void Start()
    {
        textLaps = gameObject.transform.GetChild(0).GetComponent<Text>();
        textTimer = gameObject.transform.GetChild(1).GetComponent<Text>();
        textBestTimeOfLap = gameObject.transform.GetChild(2).GetComponent<Text>();

        StartTime = Time.fixedTime;
        StartLaps();
    }

    void Update()
    {
        Timer();
        // наш первый чит-код для сброса таймера
        if(Input.GetKeyDown(KeyCode.K))
        {                       
            TimesOfLaps.Add(time); 
            BestTimeOfLap();        
            ResetTimer(); 
            countLaps++;     
            textLaps.text = "Laps: " + countLaps;
        }
    }

    public void StartLaps()
    {
        countLaps = 1;
        textLaps.text = "Laps: " + countLaps;        
    }

    public void AddLap()
    {        
        TimesOfLaps.Add(time); 
        BestTimeOfLap();        
        ResetTimer(); 
        countLaps++;     
        textLaps.text = "Laps: " + countLaps;
    }

    public void Timer()
    {
        time = Time.fixedTime - StartTime;        
        textTimer.text = time.ToString("0.0");
    }

    public void ResetTimer()
    {
        StartTime = Time.fixedTime;
    }

    public void BestTimeOfLap()
    {
        foreach (var item in TimesOfLaps)
        {
            // на 1-ом кругу просто записываем время
            if(countLaps == 1)
            {
                BestTime = time;
                BestLap = countLaps;
            }

            // а затем после 1-го круга проверяем было ли текущее время меньше предыдущего
            if(item < BestTime)
            {                
                BestTime = item;
                BestLap = countLaps;
            }            
        }
        textBestTimeOfLap.text = "Best time: " + BestTime.ToString("0.0") + " " + "on lap: " + BestLap.ToString();
    }
}
