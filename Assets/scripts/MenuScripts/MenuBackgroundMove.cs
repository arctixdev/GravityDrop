using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackgroundMove : MonoBehaviour
{
    public int distance = 20;
    public float speed = 1;

    private bool direction = true;

    void Update()
    {
        // Determine direction
        if (transform.position.x > distance / 2) {
            direction = false;
        } else if (transform.position.x < -(distance / 2)) {
            direction = true;
        }

        // Make the object move slowly from side to side
        if (direction == true) {
            transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0,0);
        } else {
            transform.position = transform.position - new Vector3(speed * Time.deltaTime, 0,0);
        }
    }
}
