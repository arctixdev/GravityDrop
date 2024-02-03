using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;

public class chainGenV2 : MonoBehaviour
{
    [SerializeField] private GameObject[] handles;
    private Vector2[] oldHandles;
    private int oldHandleCount;
    Vector2[] controlPositions;
    private void Start()
    {
        oldHandles = new Vector2[handles.Length];
        for (int i = 0; i < handles.Length; i++)
        {
            oldHandles[i] = handles[i].transform.position;
        }
        oldHandleCount = handles.Length;
    }

    private void Update()
    {
        Camera mainCam = Camera.main;
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (mainCam == null) mainCam = Camera.main;
            Vector2 y = mainCam.ScreenToWorldPoint(Input.mousePosition);
            controlPositions = new Vector2[]
            {
                y,
                y + new Vector2(0, -1),
                //y + new Vector2(-1, -1),
                //y + new Vector2(0, -2)
            };
            for (int i = 0; i < controlPositions.Length; i++)
            {
                if(handles.Length - 1 >= i) handles[i].transform.position = controlPositions[i];
            }
            generateChain(controlPositions, 100);
            //List<Vector2> l = bezeirCurve.PointList2(new List<Vector2>(x), 10);
            //for(int i = 0; i < l.Count; i++)
            //{
            //    Debug.Log(l[i]);
            //}
        }
        for(int i  = 0; i < handles.Length; i++)
        {
            if ((Vector2)handles[i].transform.position != oldHandles[i])
            {
                //GameObject[] newValues = handles;
                controlPositions[i] = handles[i].transform.position;
                oldHandles[i] = handles[i].transform.position;
                generateChain(controlPositions, 100);
                Debug.Log("control position " + i +" are as follows: " + controlPositions[i]);
            }
            if(handles.Length != oldHandleCount)
            {
                oldHandleCount = handles.Length;
                for (int j = 0; j < handles.Length; j++)
                {
                    oldHandles = new Vector2[handles.Length];
                    oldHandles[j] = handles[j].transform.position;
                }
            }
        }
    }

    void generateChainSegment(Vector2[] points, int precision)
    {

    }

    //Vector2[] points;
    //int precision;
    public bool generatePersistantChain()
    {
        return true;
    }

    [SerializeField] private bool generateDebugLines;
    [SerializeField] private float chainSegmentLength;
    //public bool generateChain(Vector2[] points, int precision, int searchDistance = 3)
    public bool generateChain(Vector2[] points, int precision)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return false;

        if (points.Length < 2) return false;
        Vector2[] computedPoints = bezeirCurve.PointList2(new List<Vector2>(points), true, precision).ToArray();
        //Vector2[] computedPoints = bezeirCurve.PointList2(new List<Vector2>(points), precision).ToArray();
        Debug.Log("minimum distance reselution would be: " + Vector2.Distance(controlPositions[0], controlPositions[controlPositions.Length-1]));

        if (generateDebugLines)
        {
            //Debug.Log("drawing the following lines:");
            for(int i = 0; i < computedPoints.Length; i++)
            {
                //Debug.Log(computedPoints[i]);
            }
            DrawLine(computedPoints);
        }
        return true;
    }

    LineRenderer lr;
    //[SerializeField] private LineRenderer lineRenderer;
    void DrawLine(Vector2[] points)
    {
        //LineRenderer lr = lineRenderer;
        //if(!newGameObject)
        //{
        //}
        if (!(lr = gameObject.GetComponent<LineRenderer>())) lr = gameObject.AddComponent<LineRenderer>();
        //lr.useWorldSpace = false;
        lr.widthMultiplier = 0.1f;
        //lr.gameObject.transform.position = points[0];
        Vector3[] v3Points = new Vector3[points.Length];
        lr.positionCount = points.Length;
        for(int i = 0; i < points.Length; i++)
        {
            v3Points[i] = points[i];
            v3Points[i].z = 2f;
            //Debug.Log("linerenderer point: " + v3Points[i]);
            lr.SetPosition(i, v3Points[i]);
        }
        //GameObject.Destroy(myLine, duration);
    }
}
