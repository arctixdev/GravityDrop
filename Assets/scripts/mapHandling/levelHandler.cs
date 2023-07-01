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
            currentLevel = 1;
        } else {
            currentLevel = getLevel();
        }

        ML.importMapFromFile("lvl" + currentLevel);
        if(freezePhysicsWhileLoading){
            StartCoroutine(enablePhysics());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextLevel(){
        if(freezePhysicsWhileLoading){
            Physics2D.simulationMode = SimulationMode2D.Script;
        }
        currentLevel += 1;
        saveLevel(currentLevel);
        ML.importMapFromFile("lvl" + currentLevel);
        mainController.resetGravity();
        resetPlayerPos();
        StartCoroutine(enablePhysics());
        
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

        

    }

    private void resetPlayerPos(){

        player.transform.position = startingPlayerPos;
        player.transform.rotation = startingPlayerRot;
        playerTrail.Clear();
        cineCam.ForceCameraPosition(startingPlayerPos + Vector3.back * 50, startingCamRot);
    }



}
