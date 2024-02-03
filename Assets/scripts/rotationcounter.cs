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
        hidecounter();

    }

    void Awake()
    {
    
        UpdateText();

    }
    void Update()
    {
        
    }

    public void hidecounter(){


        counterText.text = "";

    }
    public void IncreaseCounter()
    {
        counter++;
        UpdateText();

    }
    public void Resetcounter(){
        counter = 0;
        print("resestcounter");
        UpdateText();

    }
    void UpdateText()
    {
        counterText.text = counter.ToString();
    }
    
}
