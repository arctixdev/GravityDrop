using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectMaster : MonoBehaviour
{
    Camera mainCam = Camera.main;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            // get mouse -> grid pos.
            if (mainCam == null) mainCam = Camera.main;
            Vector3 worldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int gridPos = (Vector2Int)Vector3Int.RoundToInt(worldPos / 2.5f);
            
            
        }
    }
}
