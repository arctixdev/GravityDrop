using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plusIcon : MonoBehaviour
{
    [HideInInspector] public chainGenV2 myChain;
    Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Camera tCam = Camera.main;
            if (cam != tCam) cam = tCam;

            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit && hit.transform.gameObject == gameObject)
            {
                Debug.Log("attempting: making a new handle");
                int i = myChain.findMyObjectIndex(gameObject);
                if(i != -1) myChain.makeNewHandle(i == 1 ? -1 : i);
            }
        }
    }
}
