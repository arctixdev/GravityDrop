using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{

    public static int currentLevel =  1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void updateActiveLVL(){ 
        string objectname =  gameObject.name;
        int newActiveLVL = int.Parse(objectname);

        currentLevel = newActiveLVL;
        print(currentLevel);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
