using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class levelHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private int currentLevel = 1;

    private Vector3 startingPlayerPos;
    private Quaternion startingPlayerRot;
    private Quaternion startingCamRot;

    [SerializeField] mapLoader ML;

    [SerializeField] GameObject player;

    [SerializeField] CinemachineVirtualCamera cineCam;

    [SerializeField] private TrailRenderer playerTrail;
    [SerializeField] bool resetLevel; // IMPORTANT: remove when moving to production

    [SerializeField] private mainController mainController;

    

    void Start()
    {
        startingPlayerPos = player.transform.position;
        startingPlayerRot = player.transform.rotation;
        startingCamRot = cineCam.transform.rotation;
        
        
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
        mainController.resetGravity();
        resetPlayerPos();
    }

    void saveLevel(int level){
        PlayerPrefs.SetInt("level", level);
    }
    int getLevel(){
        return PlayerPrefs.GetInt("level", 1);
    }

    private void resetPlayerPos(){

        player.transform.position = startingPlayerPos;
        player.transform.rotation = startingPlayerRot;
        playerTrail.Clear();
        cineCam.ForceCameraPosition(startingPlayerPos + Vector3.back * 50, startingCamRot);
    }


}
