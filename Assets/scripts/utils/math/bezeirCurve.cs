using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class bezeirCurve : MonoBehaviour
{


    public static Vector2 Point2(float t, List<Vector2> controlPoints)
    {
        int N = controlPoints.Count - 1;
        if (N > 16)
        {
            UnityEngine.Debug.Log("You have used more than 16 control points. The maximum control points allowed is 16.");
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

        return p;
    }
    public static Vector2 Point2(float t, List<Vector2> controlPoints, float[] arcLengths, bool evenlySpaced)
    {
        if(!evenlySpaced) return Point2(t, controlPoints);

        UnityEngine.Debug.Log("setting up starting values");
        float len = arcLengths[arcLengths.Length - 1];
        float targetLength = t * len;
        int low = 0, high = arcLengths.Length - 1, index = 0;
        UnityEngine.Debug.Log("initiating binary search");
        //while (low < high)
        //{
        //    index = low + (((high - low) / 2) | 0);
        //    if (arcLengths[index] < targetLength)
        //    {
        //        low = index + 1;
        
        //    }
        //    else
        //    {
        //        high = index;
        //    }
        //}
        if (arcLengths[index] > targetLength)
        {
            index--;
        }

        UnityEngine.Debug.Log("binary seach found index " + index);
        float lengthBefore = arcLengths[index];

        if (lengthBefore == targetLength)
        {
            return Point2(index / len, controlPoints);

        }
        else
        {
            return Point2((index + (targetLength - lengthBefore) / (arcLengths[index + 1] - lengthBefore)) / len, controlPoints);
        }
    }

    public static List<Vector2> PointList2(List<Vector2> controlPoints, int pointAmount)
    {
        int interval = 1 / pointAmount;
        List<Vector2> points = new List<Vector2>();
        for (float t = 0.0f; t <= 1.0f + interval - 0.0001f; t += interval)
        {
            points.Add(Point2(t, controlPoints));
        }

        return points;
    }
    public static List<Vector2> PointList2(List<Vector2> controlPoints, bool evenlySpaced, int pointAmount, int precision = 100)
    {
        float interval = 1 / pointAmount;
        float[] arcLenths = new float[precision];
        UnityEngine.Debug.Log("beginning arclength generation");
        float counter = Time.realtimeSinceStartup;
        arcLenths[0] = 0f;
        for(int i = 1; i < precision; i++)
        {
            float index = 1 / precision * i;
            arcLenths[i] = arcLenths[i - 1] + Vector2.Distance(Point2(index - 1, controlPoints), Point2(index, controlPoints));
        }
        UnityEngine.Debug.Log("finished arclength generation in " + (Time.realtimeSinceStartup - counter) + "seconds");
        UnityEngine.Debug.Log("beginning point generation");
        counter = Time.realtimeSinceStartup;
        List<Vector2> points = new List<Vector2>();
        int it = 0;
        for (float t = 0.0f; t <= 1.0f && it < 500; t += interval)
        {
            //Process currentProcess1 = Process.GetCurrentProcess();
            //currentProcess1.Refresh();
            //long totalMemoryUsedByProcess1 = currentProcess1.WorkingSet64;
            //if(totalMemoryUsedByProcess1 > 10_000_000_000) break;

            //if (t > interval * 500) break;

            points.Add(Point2(t, controlPoints, arcLenths, true));
            it++;
        }
        UnityEngine.Debug.Log("finished arclength generation in " + (Time.realtimeSinceStartup - counter) + "seconds with " + it + " iterations");

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
