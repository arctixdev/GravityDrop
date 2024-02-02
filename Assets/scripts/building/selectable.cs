using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class selectable : MonoBehaviour
{
    public selectEvent uiTrigger;
    public void select()
    {
        uiTrigger?.Invoke();
    }

    public void setMainWheight(float wheight)
    {
        this.GetComponent<Rigidbody2D>().mass = wheight;
    }
}

[System.Serializable]
public class selectEvent : UnityEvent
{
    // You can extend UnityEvent to add parameters or use UnityEvent<T> for typed events.
}
