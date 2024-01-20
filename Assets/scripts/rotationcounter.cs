using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class rotationcounter : MonoBehaviour

{
    public TextMeshProUGUI counterText;
    private  int counter = 0;

    void Start()
    {
        UpdateText();
    }

    void Update()
    {
        
    }

    public  void IncreaseCounter()
    {
        counter++;
        UpdateText();

    }

    void UpdateText()
    {
        counterText.text = counter.ToString();
    }
    
}
