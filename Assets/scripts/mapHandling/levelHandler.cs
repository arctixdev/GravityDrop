using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class levelHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public rotationcounter rotatecounter;

    private Vector3 startingPlayerPos;
    private Quaternion startingPlayerRot;
    private Quaternion startingCamRot;

    [SerializeField] mapLoader ML;

    [SerializeField] GameObject player;

    [SerializeField] CinemachineVirtualCamera cineCam;

    [SerializeField] private TrailRenderer playerTrail;
    [SerializeField] bool resetLevel; // IMPORTANT: remove when moving to production

    [SerializeField] private mainController mainController;

    [SerializeField] private bool freezePhysicsWhileLoading;
    [SerializeField] private float enablePhysicsDelay;
    [SerializeField] private float disableUIExtraDelay;
    [SerializeField] private GameObject finishUi;


    private void Awake() {
        if(freezePhysicsWhileLoading){
            Physics2D.simulationMode = SimulationMode2D.Script;
        }
    }

    void Start()
    {
        startingPlayerPos = player.transform.position;
        startingPlayerRot = player.transform.rotation;
        startingCamRot = cineCam.transform.rotation;
        
        
        if(resetLevel){
            LevelSelector.currentLevel = 1;
        } else {
            LevelSelector.currentLevel = getLevel();
        }

        ML.importMapFromFile("lvl" + LevelSelector.currentLevel);
        if(freezePhysicsWhileLoading){
            StartCoroutine(enablePhysics());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextLevel(){
        LevelSelector.currentLevel += 1;
        startLevel(LevelSelector.currentLevel);
        saveLevel(LevelSelector.currentLevel);
    }

    public void startLevel(int levelIndex){
        if(freezePhysicsWhileLoading){
            Physics2D.simulationMode = SimulationMode2D.Script;
        }
        ML.importMapFromFile("lvl" + LevelSelector.currentLevel);
        mainController.resetGravity();
        resetPlayerPos();
        StartCoroutine(enablePhysics());
    }

    public void restartLevel(){
        startLevel(LevelSelector.currentLevel);
    }

    void saveLevel(int level){
        PlayerPrefs.SetInt("level", level);
    }
    int getLevel(){
        return PlayerPrefs.GetInt("level", 1);
    }

    IEnumerator enablePhysics(){
        yield return new WaitForSeconds(enablePhysicsDelay);
        if(freezePhysicsWhileLoading){
            Physics2D.simulationMode = SimulationMode2D.FixedUpdate;
        }
        yield return new WaitForSeconds(disableUIExtraDelay);
        finishUi.SetActive(false);
        if(rotatecounter != null)
            print("test");
            // rotatecounter(true);
        

    }

    private void resetPlayerPos(){

        player.transform.position = startingPlayerPos;
        player.transform.rotation = startingPlayerRot;
        playerTrail.Clear();
        cineCam.ForceCameraPosition(startingPlayerPos + Vector3.back * 50, startingCamRot);
    }



}
