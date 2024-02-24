using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class biasedBezier : MonoBehaviour
{
    public static Vector2 biasedPoint2(float t, Vector2[] controlPoints, float[] arcLengths, bool evenlySpaced = true)
    {
        //return legasyPoint2(biasedT, new List<Vector2>(controlPoints), arcLengths, 25, true);

        if (!evenlySpaced) return bezeirCurve.Point2(t, controlPoints);

        //UnityEngine.Debug.Log("setting up starting values");
        //float len = arcLengths[arcLengths.Length - 1];
        int totalArcLength = arcLengths.Length - 1;
        float targetLength = t * arcLengths[totalArcLength];
        int low = 0, high = totalArcLength, index = 0;
        //UnityEngine.Debug.Log("initiating binary search");
        while (low < high)
        {
            index = low + (((high - low) / 2) | 0);
            //UnityEngine.Debug.LogWarning("index is: " + (low + (((high - low) / 2) | 0)) + " without rounding it would be: " + (low + (high - low) / 2));
            if (arcLengths[index] < targetLength)
            {
                low = index + 1;
            }
            else
            {
                high = index;
            }
        }
        //    if (arcLengths[index] == targetLength)
        //    {
        //        //UnityEngine.Debug.LogWarning("Using stuff: " + index / pointAmount + " with presumed len: " + pointAmount + " and index: " + index + "\n" +" target length is: "+targetLength+" and t is: "+t);
        //        UnityEngine.Debug.Log("point made at t: " + index / (float)pointAmount);
        //        return Point2(index / (float)pointAmount, controlPoints);
        //    }

        if (arcLengths[index] > targetLength)
        {
            index--;
        }

        Debug.Log("binary seach found index " + index + ", floored to nearest arclength would give t: " + (index / (float)totalArcLength) + "\n" +
            "targetLength was: " + targetLength + " with a starting t of: " + t
            );
        float lengthBefore = arcLengths[index];
        if (lengthBefore == targetLength)
        {
            //UnityEngine.Debug.LogWarning("Using stuff: " + index / pointAmount + " with presumed len: " + pointAmount + " and index: " + index + "\n" +" target length is: "+targetLength+" and t is: "+t);
            Debug.Log("point made at t: " + index / (float)totalArcLength + " index is: " + index);
            return bezeirCurve.Point2(index / (float)totalArcLength, controlPoints);
        }

        else
        {
            // the amount targetDistance is bigger than the arcLength before it
            //float distanceLeft = targetLength - lengthBefore;

            // the distance between the arcLength before targetLength and the arcLength after targetLength
            //float totalDistanceToNextArcLength = (arcLengths[index + 1] - lengthBefore);

            // gets a number from 0 - 1 indicating how far to interpelate between lengthBefore and the next arcLength
            //float localT = distanceLeft / totalDistanceToNextArcLength;

            // gets the final t by adding the interpolated value to the found index and dividing by the total amount of values
            //float finalT = (index + localT) / totalArcLength;

            //UnityEngine.Debug.LogWarning("Using (advanced) stuff: " + (index + (targetLength - lengthBefore) / (arcLengths[index + 1] - lengthBefore)) / pointAmount + " with presumed len: " + pointAmount + " and index: " + index + "\n" + " target length is: " + targetLength + " lerp start length is: "+ lengthBefore + " meaning lerp distance should be: " + (targetLength-lengthBefore) + " and lerp percent is: " + (targetLength - lengthBefore) / (arcLengths[index + 1] - lengthBefore) + " and t is: " + t);
            UnityEngine.Debug.Log("(advanced) point made at t: " + (index + (targetLength - lengthBefore) / (arcLengths[index + 1] - lengthBefore)) / totalArcLength);
            return bezeirCurve.Point2((index + (targetLength - lengthBefore) / (arcLengths[index + 1] - lengthBefore)) / totalArcLength, controlPoints);
        }

        // old refurbished code
        /*
        int arcAmount = arcLengths.Length - 1;
        float targetLength = biasedT * arcLengths[arcAmount];
        int min = 0, max = arcAmount, index = 0;
        while (min < max)
        {
            index = min + (int)math.floor((max - min) / 2f);
            //index = min + (((max - min) / 2) | 0);
            float currentLength = arcLengths[index];
            if (currentLength < targetLength)
            {
                min = index + 1;
            }
            //else if (currentLength == targetLength) { Debug.Log("caught point early, returning found point"); return bezeirCurve.Point2(index / arcAmount, new List<Vector2>(controlPoints)); }
            else
            {
                max = index;
            }
        }
        //while (min < max)
        //{
        //    index = min + (((max - min) / 2) | 0);
            //UnityEngine.Debug.LogWarning("index is: " + (low + (((high - low) / 2) | 0)) + " without rounding it would be: " + (low + (high - low) / 2));
        //    if (arcLengths[index] < targetLength)
        //    {
        //        min = index + 1;

        //    }
        //    else
        //    {
        //        max = index;
        //    }
        //}
        float lengthBefore = arcLengths[index];
        if (lengthBefore > targetLength)
        {
            index--;
            lengthBefore = arcLengths[index];
        }

        if (lengthBefore == targetLength)
        {
            //Debug.LogWarning("found a on point length code is probable incorrect but continuing correctly anyway");
            return bezeirCurve.Point2(index / (float)arcAmount, new List<Vector2>(controlPoints));
        }
        else
        {
            // the amount targetDistance is bigger than the arcLength before it
            float distanceLeft = targetLength - lengthBefore;

            // the distance between the arcLength before targetLength and the arcLength after targetLength
            float totalDistanceToNextArcLength = (arcLengths[index + 1] - lengthBefore);

            // gets a number from 0 - 1 indicating how far to interpelate between lengthBefore and the next arcLength
            float localT = distanceLeft / totalDistanceToNextArcLength;

            // gets the final t by adding the interpolated value to the found index and dividing by the total amount of values
            float finalT = (index + localT) / arcAmount;

            Debug.LogWarning("Using (advanced) stuff: " + (index + (targetLength - lengthBefore) / (arcLengths[index + 1] - lengthBefore)) / arcAmount + " when using function that should give identical result, got: " + getBiasedT(lengthBefore, arcLengths[index + 1], targetLength, index, arcAmount) + " with presumed len: " + arcAmount + " and index: " + index + "\n" + " target length is: " + targetLength + " lerp start length is: "+ lengthBefore + " and next arc distance is: " + arcLengths[index + 1] + " meaning lerp distance should be: " + (targetLength-lengthBefore) + " and lerp percent is: " + (targetLength - lengthBefore) / (arcLengths[index + 1] - lengthBefore) + " and t is: " + biasedT);
            return bezeirCurve.Point2(finalT, new List<Vector2>(controlPoints));
            //return bezeirCurve.Point2((index + (targetLength - lengthBefore) / (arcLengths[index + 1] - lengthBefore)) / arcAmount, new List<Vector2>(controlPoints));
        }
        */
    }

    static float getBiasedT(float length1, float length2, float targetLength, int foundIndex, int totalIndexAmount)
    {
        // the amount targetDistance is bigger than the arcLength before it
        float distanceLeft = targetLength - length1;

        // the distance between the arcLength before targetLength and the arcLength after targetLength
        float totalDistanceToNextArcLength = (length2 - length1);

        // gets a number from 0 - 1 indicating how far to interpelate between lengthBefore and the next arcLength
        float localT = distanceLeft / totalDistanceToNextArcLength;

        // gets the final t by adding the interpolated value to the found index and dividing by the total amount of values
        float finalT = (foundIndex + localT) / totalIndexAmount;

        return finalT;
    }

    public static Vector2 legasyPoint2(float t, Vector2[] controlPoints, float[] arcLengths, int pointAmount, bool evenlySpaced)
    {
        if (!evenlySpaced) return bezeirCurve.Point2(t, controlPoints);

        //UnityEngine.Debug.Log("setting up starting values");
        //float len = arcLengths[arcLengths.Length - 1];
        int totalArcLength = arcLengths.Length - 1;
        float targetLength = t * arcLengths[totalArcLength];
        int low = 0, high = totalArcLength, index = 0;
        //UnityEngine.Debug.Log("initiating binary search");
        while (low < high)
        {
            index = low + (((high - low) / 2) | 0);
            //UnityEngine.Debug.LogWarning("index is: " + (low + (((high - low) / 2) | 0)) + " without rounding it would be: " + (low + (high - low) / 2));
            if (arcLengths[index] < targetLength)
            {
                low = index + 1;
            }
            else
            {
                high = index;
            }
        }
        //    if (arcLengths[index] == targetLength)
        //    {
        //        //UnityEngine.Debug.LogWarning("Using stuff: " + index / pointAmount + " with presumed len: " + pointAmount + " and index: " + index + "\n" +" target length is: "+targetLength+" and t is: "+t);
        //        UnityEngine.Debug.Log("point made at t: " + index / (float)pointAmount);
        //        return Point2(index / (float)pointAmount, controlPoints);
        //    }

        if (arcLengths[index] > targetLength)
        {
            index--;
        }

        Debug.Log("binary seach found index " + index + ", floored to nearest arclength would give t: " + (index / (float)totalArcLength) + "\n" + 
            "targetLength was: " + targetLength + " with a starting t of: " + t
            );
        float lengthBefore = arcLengths[index];
        if (lengthBefore == targetLength)
        {
            //UnityEngine.Debug.LogWarning("Using stuff: " + index / pointAmount + " with presumed len: " + pointAmount + " and index: " + index + "\n" +" target length is: "+targetLength+" and t is: "+t);
            Debug.Log("point made at t: " + index / (float)totalArcLength + " index is: " + index);
            return bezeirCurve.Point2(index / (float)totalArcLength, controlPoints);
        }

        else
        {
            //UnityEngine.Debug.LogWarning("Using (advanced) stuff: " + (index + (targetLength - lengthBefore) / (arcLengths[index + 1] - lengthBefore)) / pointAmount + " with presumed len: " + pointAmount + " and index: " + index + "\n" + " target length is: " + targetLength + " lerp start length is: "+ lengthBefore + " meaning lerp distance should be: " + (targetLength-lengthBefore) + " and lerp percent is: " + (targetLength - lengthBefore) / (arcLengths[index + 1] - lengthBefore) + " and t is: " + t);
            UnityEngine.Debug.Log("(advanced) point made at t: " + (index + (targetLength - lengthBefore) / (arcLengths[index + 1] - lengthBefore)) / totalArcLength);
            return bezeirCurve.Point2((index + (targetLength - lengthBefore) / (arcLengths[index + 1] - lengthBefore)) / totalArcLength, controlPoints);
        }
    }
}
