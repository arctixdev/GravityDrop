using System;
using UnityEngine;
using UnityEngine.UI;

public class valueHandler : MonoBehaviour
{
    //makes things from sliders and buttons be able to activate different things as well as be changed by code

    public event Action<float> floatValueEvent;
    public event Action<bool> boolValueEvent;
    public event Action buttonEvent;
    public void floatValue(float value)
    {
        floatValueEvent(value);
    }
    public void boolValue(bool value)
    {
        boolValueEvent(value);
    }
    [SerializeField] Slider slider;
    [SerializeField] private Button button;
    private void Start()
    {
        if (button) button.onClick.AddListener(buttonEvent.Invoke);
    }
    private void Update()
    {
        if (slider) floatValue(slider.value);
    }
}
