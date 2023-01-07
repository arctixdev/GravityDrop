using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseeffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)){
            Vector3 msPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position = new Vector3(msPos.x, msPos.y, transform.position.z);
        }
    }
}
