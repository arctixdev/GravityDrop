using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movableHandle : MonoBehaviour
{
    // IMPORTANT this is a placeholder script and is probalbly going to get changed with the reworked editor
    // for a better implementation in the total scheme
    
    Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    GameObject selected;
    Vector3 offset;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Camera tCam = Camera.main;
            if (cam != tCam) cam = tCam;

            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit && hit.transform.gameObject == gameObject)
            {
                Debug.Log("i was hit");
                selected = hit.collider.gameObject;
                offset = selected.transform.position - (Vector3)hit.point;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            selected = null;
        }
        if (selected)
        {
            Vector3 x = cam.ScreenToWorldPoint(Input.mousePosition); x.z = 0;
            float prevZ = selected.transform.position.z;
            selected.transform.position = new Vector3(x.x + offset.x, x.y + offset.y, prevZ);
        }
    }
}
