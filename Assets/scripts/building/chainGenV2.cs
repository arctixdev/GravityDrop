using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chainGenV2 : MonoBehaviour
{
    void generateChainSegment()
    {

    }
    [SerializeField] private float chainSegmentLength;
    public bool generateChain(Vector2[] points, int precision, int searchDistance = 3)
    {
        if(points.Length < 2) return false;
        Vector2[] computedPoints = bezeirCurve.PointList2(new List<Vector2>(points), 1 / precision).ToArray();
        int aproxemateChainSegments = 1000;
        bool shifter = true;
        for(int i = 0; i < aproxemateChainSegments; i++)
        {
            (int, float) min = new(-1, -1f);
            for(int j = 0; j < searchDistance; j++)
            {
                if(i+j < computedPoints.Length)
                {
                    float dis = Vector2.Distance(computedPoints[i], computedPoints[i + j]);
                    if (Mathf.Abs(chainSegmentLength - dis) < min.Item2) min = new(i-j, dis);
                }
            }
        }
        return true;
    }
}
