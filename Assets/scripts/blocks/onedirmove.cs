using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onedirmove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.constraints = transform.rotation.z % 180 > 45 ? RigidbodyConstraints2D.FreezePositionX : RigidbodyConstraints2D.FreezePositionY;
    }
}
