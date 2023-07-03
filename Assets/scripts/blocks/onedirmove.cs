using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onedirmove : MonoBehaviour
{
    public float z;
    // Start is called before the first frame update
    void Start()
    {   
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float rot = transform.eulerAngles.z;
        if(rot < 0){
            rot += 360;
        }
        rb.constraints = (rot % 180 > 45 ? RigidbodyConstraints2D.FreezePositionX : RigidbodyConstraints2D.FreezePositionY) | RigidbodyConstraints2D.FreezeRotation;

    }
    void Update()
    {
        z = transform.eulerAngles.z;
    }
}
