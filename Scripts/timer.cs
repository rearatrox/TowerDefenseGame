using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Timers;

public class timer : MonoBehaviour
{
    
    public Text timerLabel;
    public float time = 1;
    public GameManager gM;
    private void Start()
    {
        gM = FindObjectOfType<GameManager>();
        gM.countdownZeit = 5.0f;
           
    }
   
    void Update()
    {
       if(FindObjectOfType<GameManager>().startTimer)
        {
           gM.countdownZeit -= Time.deltaTime;

            var minutes = gM.countdownZeit / 60; //Divide the guiTime by sixty to get the minutes.
            var seconds = gM.countdownZeit % 60;//Use the euclidean division for the seconds.
            var fraction = (gM.countdownZeit * 100) % 100;
           
            //update the label value
          timerLabel.text = "Nächste Welle in: " + string.Format("{0:00}", seconds);
            
        }
        
    }
}