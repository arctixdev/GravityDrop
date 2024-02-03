using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;

public class chainGenV2 : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 y = Input.mousePosition;
            Vector2[] x =
            {
                y,
                y + new Vector2(0, -1),
                y + new Vector2(0, -2),
                y + new Vector2(0, -3)
            };
            generateChain(x, 100);
        }
    }

    void generateChainSegment()
    {

    }

    [SerializeField] private bool generateDebugLines;
    [SerializeField] private float chainSegmentLength;
    //public bool generateChain(Vector2[] points, int precision, int searchDistance = 3)
    public bool generateChain(Vector2[] points, int precision)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return false;

        if (points.Length < 2) return false;
        Vector2[] computedPoints = bezeirCurve.PointList2(new List<Vector2>(points), true, precision).ToArray();

        if(generateDebugLines)
        {
            for (int i = 1; i <= computedPoints.Length; i++)
            {
                DrawLine(computedPoints[i - 1], computedPoints[i]);
            }
        }
        return true;
    }

    [SerializeField] private Material material;
    void DrawLine(Vector3 start, Vector3 end)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        LineRenderer lr = myLine.AddComponent<LineRenderer>();
        lr.material = material;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        //GameObject.Destroy(myLine, duration);
    }
}
