using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class selectableV2 : MonoBehaviour
{
    public GameObject ui;
    public Animator animator;
    public void select()
    {
        animator.SetBool(0, !animator.GetBool(0));
    }

    public void setMainWheight(float wheight)
    {
        this.GetComponent<Rigidbody2D>().mass = wheight;
    }
}

//[System.Serializable]
//public class selectEvent : UnityEvent
//{
//    // You can extend UnityEvent to add parameters or use UnityEvent<T> for typed events.
//}
