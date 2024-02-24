using System.Collections;
using System.Collections.Generic;
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
        char lastChar = objectname[objectname.Length - 1];
        int newActiveLVL = lastChar - '0';

        currentLevel = newActiveLVL;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
