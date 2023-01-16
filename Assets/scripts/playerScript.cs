using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.CompareTag("killPlayer")){
            Debug.Log("killing player");
        }
    }
}

/// <summary>
/// Sent when an incoming collider makes contact with this object's
/// collider (2D physics only).
/// </summary>
/// <param name="other">The Collision2D data associated with this collision.</param>
