using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseeffect : MonoBehaviour
{
    [SerializeField] TrailRenderer_Local trailRenderer;
    
    Camera _cam;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)){
            Vector3 msPos = _cam.ScreenToWorldPoint(Input.mousePosition);

            transform.position = new Vector3(msPos.x, msPos.y, transform.position.z);
        }

        if(Input.GetMouseButtonDown(0)){
            trailRenderer.Reset();
        }
    }
}
