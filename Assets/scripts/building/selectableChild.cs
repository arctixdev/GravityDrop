using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectableChild : MonoBehaviour
{
    public GameObject master;
    private void Start()
    {
        if (master.GetComponent<selectable>() == null)
        {
            throw new MissingComponentException("master of "+this.name+" is missing the selectable component");
        }
    }
}
