using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    
    public float timeStart;
    public Text timerText;

    // Start is called before the first frame update
    public void Start()
    {
        timerText.text = timeStart.ToString("F1");
    }

    // Update is called once per frame
    public void Update()
    {
        timeStart += Time.deltaTime;
        timerText.text = timeStart.ToString("F1");
    }
}
