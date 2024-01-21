using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boolChanger : MonoBehaviour
{
    Animator animator;
    [SerializeField] private string boolId;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void setBoolId(string value)
    {
        boolId = value;
    }
    public void SetBool(bool value)
    {
        animator.SetBool(boolId, value);
    }
    public void changeBool()
    {
        bool x = animator.GetBool(boolId);
        if(x) animator.SetBool(boolId, false);
        else animator.SetBool(boolId, true);
    }

}
