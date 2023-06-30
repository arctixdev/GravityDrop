using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private int currentLevel = 1;

    [SerializeField] mapLoader ML;
    void Start()
    {
        currentLevel = getLevel();
        ML.importMapFromFile("lvl" + currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextLevel(){
        currentLevel += 1;
        saveLevel(currentLevel);
        ML.importMapFromFile("lvl" + currentLevel);
    }

    void saveLevel(int level){
        PlayerPrefs.SetInt("level", level);
    }
    int getLevel(){
        return PlayerPrefs.GetInt("level", 1);
    }
}
