using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finish : MonoBehaviour
{
    // Start is called before the first frame update
    public rotationcounter rotatecounter;

    private GameObject finishUi;
    void Start()


    {
        //reset counter
        if (rotatecounter != null)
            rotatecounter.hidecounter();
        foreach (Transform t in GameObject.Find("Canvas").GetComponentInChildren<Transform>(true))
        {
            if(t.name == "finish-ui"){
                finishUi = t.gameObject;
            }
        }
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
         
        finishUi.SetActive(true);
    }
}
