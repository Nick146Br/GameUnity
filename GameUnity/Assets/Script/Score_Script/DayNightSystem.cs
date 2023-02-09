using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayNightSystem : MonoBehaviour
{
    public float currentTime;
    public float dayLengthMinutes;
    public TextMeshProUGUI timeText;
    private float rotationSpeed;
    float translateTime;
    float midday;
    float ponto_fase = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 360 / dayLengthMinutes / 60;
        midday = dayLengthMinutes * 60 / 2;
        // SingletonExample.Instance.Score_temp  =0;

    }   

    // Update is called once per frame
    void Update()
    {
        currentTime += 1 * Time.deltaTime;
        translateTime = (currentTime / (midday * 2));

        float t = translateTime * 24f;
        float hours = Mathf.Floor(t);
        int total = 100;
        // SingletonExample.Instance.Score_temp = Mathf.Max(SingletonExample.Instance.Score_temp, 0);
        // timeText.text = "Score: " + SingletonExample.Instance.Score_temp.ToString();
        transform.Rotate(new Vector3(1,0,0) * rotationSpeed * Time.deltaTime);
    }
}
