using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicCameraMover : MonoBehaviour
{
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position = new Vector3(pos.x, pos.y + speed * Time.deltaTime, pos.z);
            pos = this.transform.position;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position = new Vector3(pos.x - speed * Time.deltaTime, pos.y, pos.z);
            pos = this.transform.position;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position = new Vector3(pos.x, pos.y - speed * Time.deltaTime, pos.z);
            pos = this.transform.position;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position = new Vector3(pos.x + speed * Time.deltaTime, pos.y, pos.z);
        }
    }
}
