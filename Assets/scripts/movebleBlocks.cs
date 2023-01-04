using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movebleBlocks: MonoBehaviour
{
    [SerializeField]
    private bool lockX;
    [SerializeField]
    private bool lockZ;

    private Vector3 startPos;
    private Quaternion startRot;

    
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
        transform.localPosition = new Vector3(lockX ? startPos.x : transform.localPosition.x,startPos.y, lockZ ? startPos.z : transform.localPosition.z);
        transform.localRotation = startRot;
    }


    void FixedUpdate()
    {

    }
}
