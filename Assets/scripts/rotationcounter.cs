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

    public void hidecounter(){

        Resetcounter();
       // counterText.SetActive(false);

    }
    public void IncreaseCounter()
    {
        counter++;
        UpdateText();

    }
    public void Resetcounter(){
        counter = 0;
    }
    void UpdateText()
    {
        counterText.text = counter.ToString();
    }
    
}
