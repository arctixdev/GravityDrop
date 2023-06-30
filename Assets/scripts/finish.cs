using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finish : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            enabelUi();
        }
    }

    private void enabelUi(){
         
        foreach (Transform t in GameObject.Find("Canvas").GetComponentInChildren<Transform>(true))
        {
            if(t.name == "finish-ui"){
                t.gameObject.SetActive(true);
            }
        }
    }
}
