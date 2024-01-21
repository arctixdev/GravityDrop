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
}

[System.Serializable]
public class selectEvent : UnityEvent
{
    // You can extend UnityEvent to add parameters or use UnityEvent<T> for typed events.
}
