using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class probetiesTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float health; changeCheck healthCheck = new changeCheck();
    
    [SerializeField]
    float someOtherField;
    [SerializeField]
    float xPos; changeCheck __xpos = new changeCheck();
    
    void OnValidate() {
        if (healthCheck.check(health)) {
            print($"Health changed from {healthCheck.oldVar} to {health}");
            
        }
        if(__xpos.check(xPos)){
            transform.position = new Vector3(xPos, transform.position.y, transform.position.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


