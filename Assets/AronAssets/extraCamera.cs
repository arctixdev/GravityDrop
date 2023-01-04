using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extraCamera : MonoBehaviour
{
    [SerializeField]
    private mainController mainController;
    private bool used = false;
    void OnTriggerEnter(Collider other)
    {
        if(used == false)
        {
            if(!other.isTrigger)
            {
                if(other.gameObject.name == "player")
                {
                    //used = true;
                    mainController.extraCameraPos(true);
                }
            }
        }
        //else
        //{
        //    if(!other.isTrigger)
        //    {
        //        if(other.gameObject.name == "player")
        //        {
        //            used = false;
        //            mainController.extraCameraPos(false);
        //        }
        //    }
        //}
    }
}
