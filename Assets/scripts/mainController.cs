using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform[] cameraPositions;
    [SerializeField]
    private Transform[] extraCameraPositions;
    [SerializeField]
    private GameObject[] levels_forDisabling;
    [SerializeField]
    private int rotOffset;
    [SerializeField]
    private float time;
    public float rot;
    [HideInInspector]
    public bool IsHitingWall;
    private float rotNew;
    private float curRot = 0;
    private int curCameraPosition = 0;
    private int lastCameraPosition = 3;
    private Vector3 lastPosition;

    public ThudScript thudScipt;

    public IngameMenuScript menuScript;

    private float SAngle;

    //bool isMobile;

    Vector2 pM;
    Vector2 M;

    Vector2 startpM;



    public bool mobileRotateEnabled = false;

    [SerializeField]
    private bool useMenu;
    [SerializeField]
    private bool airRot;
    [SerializeField]
    private float waitBeforeGravChange = 0.34f;
    [SerializeField]
    private Vector3 preRot;

    // Declare a serialized field for the dropdown box
    [SerializeField]
    // Declare a non-serialized field to store the selected option
    private bool Dimension3D = true;


    float oldrottomouse;
    float startrotmouse;
    float RM;
    public enum rotModes
    {
        MouseYPos,
        rotationOfLevel
    }

    [SerializeField]
    public rotModes rotationMode;

    [SerializeField]
    public float rotDisToRotate;


    public iTween.EaseType EaseType;

    void Awake()
    {
        Physics.gravity = new Vector3(0,-9.81f,0);
    }
    void Start()
    {
        rot = transform.eulerAngles.z;

        //isMobile = false;


        Vector3 gravShiftCalc = new Vector3(0,-9.81f,0);
        if(Dimension3D){
            Physics.gravity = gravShiftCalc;
        }
        else{
            // so its 2d
            Physics2D.gravity = gravShiftCalc;
        }
    }

    // Update is called once per frame
    float calcMouseRot(){
        Vector2 screenMiddle = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector2 endPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        Vector2 direction = endPosition - screenMiddle;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        return -angle;
    }
    void Update()
    {
        pM = new Vector2(Input.mousePosition.x / Screen.width * 100, Input.mousePosition.y / Screen.height * 100);
        RM = calcMouseRot();
        if(Input.GetMouseButtonDown(0)){

            startpM = pM;
            startrotmouse = RM;
            oldrottomouse = RM;
            // Debug.Log(Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0));
        }

        if(Input.GetMouseButton(0) && mobileRotateEnabled){
            transform.Rotate(0, 0, RM - oldrottomouse);

            oldrottomouse = calcMouseRot();
            
        }
        
        if(Input.GetMouseButtonUp(0)){
            if((IsHitingWall || airRot) && mobileRotateEnabled){
                // Debug.Log(RM - oldrottomouse);
                // adding the last rotation velocity
                RM += RM - oldrottomouse;
                if(rotationMode == rotModes.rotationOfLevel){

                    float rotdiff = startrotmouse - RM ;
                    if(rotdiff < -180) rotdiff += 360;
                    if(rotdiff > 180) rotdiff -= 360;
                    // Debug.Log(rotdiff);
                    
                    
                    if(rotdiff > rotDisToRotate){
                        rot -= 90;
                    }
                    if(rotdiff < -rotDisToRotate){
                        rot += 90;
                    
                    }
                }
                else{
                    float xdiff = pM.y - startpM.y;
                    
                    if(xdiff > rotDisToRotate){
                        rot -= 90;
                    }
                    if(xdiff < -rotDisToRotate){
                        rot += 90;
                    }
                }
            }
            iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(preRot.x, preRot.y, rot), "time", time, "easetype", EaseType));
        }
        if(useMenu){
            menuScript.level((int) (Input.mousePosition.x / Screen.width * 100));
        }
        //Debug.Log(curCameraPosition);
        for(int i = 0;i<=levels_forDisabling.Length-1;i++)
        {
            if(i!=curCameraPosition)
            {
                levels_forDisabling[i].SetActive(false);
            }
            else
            {
                levels_forDisabling[i].SetActive(true);
                // menuScript.level(i+1);
            }
        }
        if(cameraPositions.Length != 0)
        {            
            if(lastCameraPosition != curCameraPosition)
            {
                transform.position = cameraPositions[curCameraPosition].position;
                lastCameraPosition = curCameraPosition;
            }
        }
        if(IsHitingWall || airRot)
        {
            
            if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
                rot += 180;
                iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(preRot.x, preRot.y, rot), "time", time, "easetype", iTween.EaseType.easeInOutCubic));
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
                rot -= 90;
                iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(preRot.x, preRot.y, rot), "time", time, "easetype", iTween.EaseType.easeInOutCubic));
            }
            // if(Input.GetKeyDown(KeyCode.DownArrow)) {
            //     iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(0, 270, transform.eulerAngles.z+rotOffset+0), "time", time, "easetype", iTween.EaseType.easeInOutCubic));
            // }
            if(Input.GetKeyDown(KeyCode.RightArrow)  || Input.GetKeyDown(KeyCode.D)) {
                rot += 90;
                iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(preRot.x, preRot.y, rot), "time", time, "easetype", iTween.EaseType.easeInOutCubic));
            }

            if(mobileRotateEnabled){

                if(shouldMobileRot(90)){
                    rot = 180;
                   
                }
                if(shouldMobileRot(180)){
                    rot = 90;
                    
                }
                if(shouldMobileRot(-90)){
                    rot = 0;
                    
                }
                if(shouldMobileRot(0)){
                    rot = -90;
                   
                }
            }
            rot = rot % 360;
            rotNew = rot < 0 ? rot + 360 : rot;
            if(curRot != rotNew)
            {
                // Update everything if the rotation has happened
                curRot = rotNew;
                thudScipt.rotate();
                StartCoroutine(shiftGrav());
                // hej johannes
            }

        }
    }

    bool shouldMobileRot(int degree){
        if(SAngle == 0) return false;
        return Mathf.Min(Mathf.Abs(degree - SAngle),  Mathf.Abs(degree - (SAngle - 360))) < 50;
    }

    IEnumerator shiftGrav()
    {
        Vector3 gravShiftCalc = new Vector3(0,-9.81f,0);
        if(Dimension3D){

            if(rotNew == 90) gravShiftCalc = new Vector3(0,0,9.81f);

            else if(rotNew == 270) gravShiftCalc = new Vector3(0,0,-9.81f);

        }
        else{

            if(rotNew == 90) gravShiftCalc = new Vector3(9.81f,0,0);
            
            else if(rotNew == 270) gravShiftCalc = new Vector3(-9.81f,0,0);
            
        }
        if(rotNew == 0)
        {
            gravShiftCalc = new Vector3(0,-9.81f,0);
        }
        else if(rotNew == 180)
        {
            gravShiftCalc = new Vector3(0,9.81f,0);
        }
        yield return new WaitForSeconds(waitBeforeGravChange);
        if(Dimension3D){
            Physics.gravity = gravShiftCalc;
        }
        else{
            // so its 2d
            Physics2D.gravity = gravShiftCalc;
        }
            

    }
    public void nextCameraPos(int amount)
    {
        curCameraPosition += amount;
    }
    public void extraCameraPos(bool on)
    {
        if(on)
        {
            transform.position = extraCameraPositions[curCameraPosition].position;
        }
        else
        {
            transform.position = cameraPositions[curCameraPosition].position;
        }
    }

    public void resetGravity(){
        rot = 0;
        rotNew = 0;
        curRot = 0;
        StartCoroutine(shiftGrav());

    }
}
