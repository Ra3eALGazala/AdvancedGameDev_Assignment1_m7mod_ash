using UnityEngine;
using TMPro; 

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; 
    public float timeElapsed = 0f;

    void Update()
    {
        
        timeElapsed += Time.deltaTime;

    
        DisplayTime(timeElapsed);
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}