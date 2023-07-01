using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private int currentLevel = 1;

    private Vector3 startingPlayerPos;

    [SerializeField] mapLoader ML;

    [SerializeField] GameObject player;
    [SerializeField] bool resetLevel; // IMPORTANT: remove when moving to production

    

    void Start()
    {
        startingPlayerPos = player.transform.position;

        
        if(resetLevel){
            currentLevel = 1;
        } else {
            currentLevel = getLevel();
        }

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
        StartCoroutine(resetPlayerPos());
    }

    void saveLevel(int level){
        PlayerPrefs.SetInt("level", level);
    }
    int getLevel(){
        return PlayerPrefs.GetInt("level", 1);
    }

    IEnumerator resetPlayerPos(){

        player.transform.position = startingPlayerPos;

        player.SetActive(true);

    }


}
