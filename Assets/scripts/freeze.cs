using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freeze: MonoBehaviour
{
    // public vector3 eh;
    public bool xPos = false;
    public bool yPos = false;
    public bool zPos = false;

    public Vector3 startPos;
    public Quaternion startRot;

    
    // public Quaternion startRot;
    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.localPosition;
        startRot = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {   
        transform.localPosition = new Vector3(xPos ? startPos.x : transform.localPosition.x, yPos ? startPos.y : transform.localPosition.y, zPos ? startPos.z : transform.localPosition.z);
        
    }


    void FixedUpdate()
    {

    }
}
