using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectableObject : MonoBehaviour
{
    public bool select(bool shouldDebug)
    {
        if(shouldDebug) Debug.Log("i, "+this.gameObject.name+":"+this.gameObject.GetInstanceID()+", have been selected");
        return true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
