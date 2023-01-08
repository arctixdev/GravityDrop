using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuHandler : MonoBehaviour
{
    public Button startButton;
    private VisualElement root;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        startButton = root.Q<Button>("start-button");

        startButton.clicked += StartButtonPressed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartButtonPressed() {
        Debug.Log("Omg it works");
        root.Q("start-button").AddToClassList("jumpOut");
    }
}

public static class UiHelpers {
    public static void DelayAddToClassList(VisualElement ui, string classToAdd = "animate", int delay = 100) {
        ui.schedule.Execute(() => ui.AddToClassList(classToAdd)).StartingIn(delay);
    }
}
