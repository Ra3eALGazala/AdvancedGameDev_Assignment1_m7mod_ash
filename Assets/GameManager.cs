using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 
    public TextMeshProUGUI scoreText;   
    private int destroyedCount = 0;

    void Awake()
    {
        instance = this; 
        }

    public void AddPoint()
    {
        destroyedCount++;
        scoreText.text = "النقاط: " + destroyedCount;
        Debug.Log("تم تدمير جسم! المجموع: " + destroyedCount);
    }
}