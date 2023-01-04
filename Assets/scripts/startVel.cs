using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startVel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Vector3 startVelocity;
    [SerializeField]
    public Rigidbody rb;
    void Awake()
    {
        rb.velocity = startVelocity;
    }


}
