using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class camaraMover : MonoBehaviour
{
    // Start is called before the first frame update

    float xspeed;
    float yspeed;
    [SerializeField]
    float Maxspeed;
    [SerializeField]
    public float Acceleration;

    [SerializeField]
    public float rotationTime;

    private int rotation;

    [SerializeField]
    private CinemachineVirtualCamera virtuelCam;
    private Camera cam;

    private bool isDragging = false;
    private Vector3 previousMousePosition;
    
    void Start()
    {
        virtuelCam = GetComponent<CinemachineVirtualCamera>(); 
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
            yspeed = Mathf.Clamp(yspeed + Acceleration * Time.deltaTime, -Maxspeed, Maxspeed);
        }
        else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
            yspeed = Mathf.Clamp(yspeed - Acceleration * Time.deltaTime, -Maxspeed, Maxspeed);
        }
        else if(yspeed != 0){
            yspeed = clampToZero(yspeed, Acceleration * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            xspeed = Mathf.Clamp(xspeed + Acceleration * Time.deltaTime, -Maxspeed, Maxspeed);
        }
        else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            xspeed = Mathf.Clamp(xspeed - Acceleration * Time.deltaTime, -Maxspeed, Maxspeed);
        }
        else if(xspeed != 0){
            xspeed = clampToZero(xspeed, Acceleration * Time.deltaTime);

        }

        if(Input.mouseScrollDelta.y != 0 && Input.GetKey(KeyCode.LeftControl)){
            virtuelCam.m_Lens.OrthographicSize *= 1 + Input.mouseScrollDelta.y * -0.1f;
        }
        

        if(xspeed != 0 || yspeed != 0){

            transform.Translate(new Vector3(xspeed * Time.deltaTime, yspeed * Time.deltaTime, 0));
        }

        if(Input.GetKeyDown("[1]")){
            rotateCam(-1);
            print("rot");
        }
        if(Input.GetKeyDown("[3]")){
            rotateCam(1);
        }

        if (Input.GetMouseButtonDown(2)) // Middle mouse button pressed
        {
            isDragging = true;
            previousMousePosition = cam.ScreenToWorldPoint(Input.mousePosition) - cam.transform.position;
            
            
        }


        if (Input.GetMouseButton(2))
        {
            Vector3 currentMousePosition = cam.ScreenToWorldPoint(Input.mousePosition) - cam.transform.position;
            Vector3 mouseDelta = previousMousePosition - currentMousePosition;
            transform.position += mouseDelta;
            
            // print(cam.cameraToWorldMatrix);

            previousMousePosition = currentMousePosition;
        }
    
    }

    void rotateCam(int deg){
        rotation += deg;
        rotation = rotation % 4;
        iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(0, 0, rotation * 90), "time", rotationTime, "easetype", iTween.EaseType.easeInOutCubic));
        // rotation = rotation % 4;
    }
    float clampToZero(float n, float distance){
        if(n == 0) return 0;
        if(distance > Mathf.Abs(n)) return 0;
        
        return n - Mathf.Sign(n) * distance;

    }
}
