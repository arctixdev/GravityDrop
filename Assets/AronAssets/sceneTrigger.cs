using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneTrigger : MonoBehaviour
{
    [SerializeField]
    private mainController mainController;
    public bool used;

    public BoxCollider Bcollider;
    public bool playerExit;

    void Start() {
        Bcollider = GetComponent<BoxCollider>();
        used = false;
        playerExit = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(used == false)
        {
            if(!other.isTrigger)
            {
                if(other.gameObject.name == "player")
                {
                    mainController.nextCameraPos(1);
                    used = true;
                }
            }
        }
        //else
        //{
        //    if(!other.isTrigger)
        //    {
        //        if(other.gameObject.name == "player")
        //        {
        //            mainController.nextCameraPos(-1);
        //        }
        //    }
        //}
    }

    public void OnTriggerExit(Collider other) {
        playerExit = true;
    }

    void Update()
    {
        if (used && playerExit) {
            Bcollider.isTrigger = false;
        }
    }

}
