using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectableChildOld : MonoBehaviour
{
    public GameObject master;
    private void Start()
    {
        if (master.GetComponent<selectableOld>() == null)
        {
            throw new MissingComponentException("master of "+this.name+" is missing the selectable component");
        }
    }
}
