using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class keyToggle : MonoBehaviour
{

    [SerializeField] ToggleGroup toggleGroup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i <= 9; i++)
        {
            if(Input.GetKeyDown("" + i)){
                enableToggle(i - 1);

            }
        }
        
    }

    public void enableToggle(int toggleIndex){
        if(toggleIndex == -1){
            toggleGroup.GetComponentsInChildren<Toggle>().LastOrDefault().isOn = true;   
            return;
        }
        toggleGroup.GetComponentsInChildren<Toggle>()[toggleIndex].isOn = true;   
    }
}
