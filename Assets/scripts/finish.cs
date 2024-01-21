using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finish : MonoBehaviour
{
    // Start is called before the first frame update
    public rotationcounter rotatecounter;
    public string rotatecountername = "rotation-counter";
    private GameObject finishUi;
    void Start()


    {
        GameObject rotatecountergameobject = GameObject.Find(rotatecountername);

        // Reset counter
        if (rotatecountergameobject != null)
        {
            rotationcounter myRotationCounter = rotatecountergameobject.GetComponent<rotationcounter>();

            // Check if the script component was found
            if (myRotationCounter != null)
            {
                myRotationCounter.Resetcounter(); // Call the reset method on the instance
            }
            else
            {
                Debug.LogError("rotationcounter script component not found on the GameObject.");
            }
        }


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
