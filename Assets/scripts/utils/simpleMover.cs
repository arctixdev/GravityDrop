using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class executeOnEnd : UnityEvent { }

public class simpleMover : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve pCurve;
    [SerializeField]
    private AnimationCurve rCurve;
    [SerializeField]
    private float timeToComplete;
    [SerializeField]
    private Transform p1;
    [SerializeField]
    private Transform p2;
    [SerializeField]
    private Transform startPos;


    float time;
    int curPoint;
    int nextPoint;
    [SerializeField]
    private bool restart;
    [SerializeField]
    private bool startNow = false;
    private bool returnBool = false;
    [SerializeField]
    private executeOnEnd executeOnEnd; 
    // Update is called once per frame
    void Update()
    {
        if(timeToComplete-time < 0.1f) executeOnEnd.Invoke();
        if (startNow)
        {
            time += Time.deltaTime;
            time = math.min(time, timeToComplete - 0.01f);
            if (restart)
            {
                time = 0;
                restart = false;
            }
            transform.position = math.lerp(p1.position, p2.position,pCurve.Evaluate(time / timeToComplete));
            transform.rotation = Quaternion.Lerp(p1.rotation, p2.rotation, rCurve.Evaluate(time / timeToComplete));
        }
        if(returnBool)
        {
            time += Time.deltaTime;
            time = math.min(time, timeToComplete - 0.01f);
            if (restart)
            {
                time = 0;
                restart = false;
            }
            transform.position = math.lerp(p2.position, p1.position, pCurve.Evaluate(time / timeToComplete));
            transform.rotation = Quaternion.Lerp(p2.rotation, p1.rotation, rCurve.Evaluate(time / timeToComplete));
        }
    }

    public void StartAnimation()
    {
        time = 0;
        startNow = true;
        returnBool = false;
    }
    public void returnToStart()
    {
        time = 0;
        startNow = false;
        returnBool = true;
        //transform.position = startPos.position;
        //transform.rotation = startPos.rotation;
    }
    public void cancellAll()
    {
        startNow = false;
        returnBool = false;
    }
    public void ShiftAnimation()
    {
        if(!startNow)
        {
            time = 0;
            startNow = true;
            returnBool = false;
        }
        else
        {
            time = 0;
            startNow = false;
            returnBool = true;
        }
    }
}
