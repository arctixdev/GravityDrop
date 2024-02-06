using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using UnityEngine;

public class bezeirCurve : MonoBehaviour
{


    public static Vector2 Point2(float t, List<Vector2> controlPoints)
    {
        int N = controlPoints.Count - 1;
        if (N > 16)
        {
            UnityEngine.Debug.LogWarning("You have used more than 16 control points. The maximum control points allowed is 16.");
            controlPoints.RemoveRange(16, controlPoints.Count - 16);
        }

        if (t <= 0) return controlPoints[0];
        if (t >= 1) return controlPoints[controlPoints.Count - 1];

        Vector2 p = new Vector2();

        for (int i = 0; i < controlPoints.Count; ++i)
        {
            Vector2 bn = Bernstein(N, i, t) * controlPoints[i];
            p += bn;
        }
        //UnityEngine.Debug.LogWarning(p);
        return p;
    }
    public static Vector2 Point2(float t, List<Vector2> controlPoints, float[] arcLengths, int pointAmount, bool evenlySpaced)
    {
        if(!evenlySpaced) return Point2(t, controlPoints);

        //UnityEngine.Debug.Log("setting up starting values");
        float len = arcLengths[arcLengths.Length - 1];
        float targetLength = t * len;
        int low = 0, high = pointAmount, index = 0;
        //UnityEngine.Debug.Log("initiating binary search");
        while (low < high)
        {
            index = low + (((high - low) / 2) | 0);
            if (arcLengths[index] < targetLength)
            {
                low = index + 1;
        
            }
            else
            {
                high = index;
            }
        }
        if (arcLengths[index] > targetLength)
        {
            index--;
        }

        //UnityEngine.Debug.Log("binary seach found index " + index + ", converted to t would be: " + index/len);
        float lengthBefore = arcLengths[index];

        if (lengthBefore == targetLength)
        {
            //UnityEngine.Debug.LogWarning("Using stuff: " + index / pointAmount + " with presumed len: " + pointAmount + " and index: " + index + "\n" +" target length is: "+targetLength+" and t is: "+t);
            return Point2(index / pointAmount, controlPoints);
        }
        else
        {
            // (targetLength - beforeLength) defines the difference bettween the found arclength and the targetlength. aka how much longer targetlength is than beforeLength (found arclength)
            // (arcLengths[index + 1] - lengthbefore) this finds how long is to the next arclength from the found arclength


            //UnityEngine.Debug.LogWarning("Using (advanced) stuff: " + (index + (targetLength - lengthBefore) / (arcLengths[index + 1] - lengthBefore)) / pointAmount + " with presumed len: " + pointAmount + " and index: " + index + "\n" + " target length is: " + targetLength + " lerp start length is: "+ lengthBefore + " meaning lerp distance should be: " + (targetLength-lengthBefore) + " and lerp percent is: " + (targetLength - lengthBefore) / (arcLengths[index + 1] - lengthBefore) + " and t is: " + t);
            return Point2((index + (targetLength - lengthBefore) / (arcLengths[index + 1] - lengthBefore)) / pointAmount, controlPoints);
        }
    }

    public static List<Vector2> PointList2(List<Vector2> controlPoints, int pointAmount)
    {
        float interval = 1f / pointAmount;
        List<Vector2> points = new List<Vector2>();
        for (float t = 0.0f; t <= 1.0f + interval - 0.0001f; t += interval)
        {
            points.Add(Point2(t, controlPoints));
        }

        return points;
    }
    public static List<Vector2> PointList2(List<Vector2> controlPoints, bool evenlySpaced, int pointAmount, int precision = 100)
    {
        if(!evenlySpaced) return PointList2(controlPoints, pointAmount);

        float interval = 1f / pointAmount;
        float[] arcLenths = new float[precision];
        //UnityEngine.Debug.Log("beginning arclength generation");
        float counter = Time.realtimeSinceStartup, tCounter = 0;
        arcLenths[0] = 0f;
        for(int i = 1; i < precision; i++)
        {
            float index = 1f / precision * i;
            index = math.round(index * 1000) / 1000;
            float x = arcLenths[i - 1] + Vector2.Distance(Point2(index - 1, controlPoints), Point2(index, controlPoints));
            arcLenths[i] = x;
            //UnityEngine.Debug.Log("current arcLength is: "+x+" at t: "+index);
        }
        string timeThing = "";
        timeThing += "finished arclength generation in " + (tCounter += Time.realtimeSinceStartup - counter) + "seconds";
        string arcString = "";
        for(int i = 0; i < arcLenths.Length; i++)
        {
            arcString += arcLenths[i] + " - ";
        }
        //UnityEngine.Debug.Log("arcLengths are as follows: \n"+arcString);
        //UnityEngine.Debug.Log("beginning point generation");
        //for(int i = 0; i < controlPoints.Count; i++)
        //{
        //    UnityEngine.Debug.Log("control point "+ i +" is " + controlPoints[i]);
        //}
        counter = Time.realtimeSinceStartup;
        List<Vector2> points = new List<Vector2>();
        int it = 0;
        for (float t = 0.0f; t <= 1.0f + interval - 0.0001f; t += interval, it++)
        {
            //t = math.round(t * 1000) / 1000;
            //UnityEngine.Debug.Log("Arc length: " + arcLenths);
            points.Add(Point2(t, controlPoints, arcLenths, pointAmount, true));
            //points.Add(Point2(t, controlPoints));
            //UnityEngine.Debug.Log("generated point at: " + points[points.Count-1]);
            UnityEngine.Debug.Log("point made at t: "+t+", interval is: "+interval);
        }
        timeThing = "finished point generation in " + (tCounter += Time.realtimeSinceStartup - counter) + "seconds with " + it + " iterations with a iterations value of " + interval + "\n" + timeThing;
        timeThing = "finished biased bezier curve in "+tCounter + "\n" + timeThing;
        UnityEngine.Debug.Log(timeThing);

        return points;
    }

private static float Bernstein(int n, int i, float t)
{
        float t_i = Mathf.Pow(t, i);
        float t_n_minus_i = Mathf.Pow((1 - t), (n - i));

        float basis = Binomial(n, i) * t_i * t_n_minus_i;
        return basis;
    }

    private static float Binomial(int n, int i)
    {
        float ni;
        float a1 = mathStuff.factorial(n);
        float a2 = mathStuff.factorial(i);
        float a3 = mathStuff.factorial(n - i);
        ni = a1 / (a2 * a3);
        return ni;
    }


}
