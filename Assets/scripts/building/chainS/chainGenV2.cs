using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Mathematics;

public class chainGenV2 : MonoBehaviour
{
    [SerializeField] private GameObject[] handles;
    [SerializeField] private GameObject handlePrefab;
    [SerializeField] private GameObject plusPrefab;
    public int precision;
    public int pointAmount;
    /// <summary>
    /// this is HIGLY temporary, a system for pushing them away from the line dynamicly WILL be implemented
    /// </summary>
    [SerializeField] private Vector3 plusDefaultOffset;
    [SerializeField] private GameObject[] plusses;
    [SerializeField] private bool printDistances;
    private Vector2[] oldHandles;
    private int oldHandleCount;
    Vector2[] controlPositions;

    Vector3 plusModifyer;
    private void Start()
    {
        oldHandles = new Vector2[handles.Length];
        for (int i = 0; i < handles.Length; i++)
        {
            oldHandles[i] = handles[i].transform.position;
        }
        oldHandleCount = handles.Length;
        plusModifyer = new Vector3(1, -1, 1);
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
                y + new Vector2(1, -1),
                y + new Vector2(-1, -1),
                y + new Vector2(0, -2)
            };
            for (int i = 0; i < controlPositions.Length; i++)
            {
                if(handles.Length - 1 >= i) handles[i].transform.position = controlPositions[i];
            }
            regenPlusObjects(true);
            generateChain(controlPositions, precision, pointAmount);
            //List<Vector2> l = bezeirCurve.PointList2(new List<Vector2>(x), 10);
            //for(int i = 0; i < l.Count; i++)
            //{
            //    Debug.Log(l[i]);
            //}
        }
        for (int i  = 0; i < handles.Length; i++)
        {
            //Debug.Log("testing for nulls (true for exists and false for not): " + (handles[i] != null) + ", " + (oldHandles[i] != null));
            if ((Vector2)handles[i].transform.position != oldHandles[i])
            {
                regenControlPoints();
                generateChain(controlPositions, precision, pointAmount);
                //Debug.Log("control position " + i +" are as follows: " + controlPositions[i]);
            }
        }
        if(printDistances)
        {
            generateChain(controlPositions, precision, pointAmount);
        }
    }
    void regenControlPoints()
    {
        if (handles.Length != oldHandleCount)
        {
            oldHandleCount = handles.Length;
            oldHandles = new Vector2[handles.Length];
        }
        controlPositions = new Vector2[handles.Length];
        for(int i = 0; i < handles.Length; i++)
        {
            controlPositions[i] = handles[i].transform.position;
            oldHandles[i] = handles[i].transform.position;
            regenPlusObjects(false);
            //generateChain(controlPositions, precision);
        }
    }

    void regenPlusObjects(bool doFullReset)
    {
        if(plusses == null) plusses = new GameObject[2];
        for (int i = 0; i < plusses.Length; i++)
        {
            if(doFullReset)
            {
                if (plusses[i]) { GameObject.Destroy(plusses[i].gameObject); Debug.Log("deleting old plus"); }
                else Debug.LogWarning("plus didnt exist, couldnt delete");
            }
        }
        if(doFullReset)
        {
            plusses = new GameObject[2];
            plusses[0] = GameObject.Instantiate(plusPrefab, handles[0].transform.position + Vector3.Scale(plusDefaultOffset, plusModifyer), Quaternion.identity, this.transform);
            plusses[0].GetComponent<plusIcon>().myChain = this;
            plusses[1] = GameObject.Instantiate(plusPrefab, handles[handles.Length-1].transform.position + plusDefaultOffset, Quaternion.identity, this.transform);
            plusses[1].GetComponent<plusIcon>().myChain = this;
        }
        else
        {
            try
            {
                GameObject currentPlus = plusses[0];
                if (currentPlus)
                {
                    currentPlus.transform.position = handles[0].transform.position + Vector3.Scale(plusDefaultOffset, plusModifyer);
                }
                currentPlus = plusses[plusses.Length - 1];
                if (currentPlus)
                {
                    currentPlus.transform.position = handles[handles.Length - 1].transform.position + plusDefaultOffset;
                }
            }
            catch { regenPlusObjects(true); }
        }
    }
    public void makeNewHandle(int place)
    {
        Debug.Log("request recieved: making new handle");
        GameObject[] oldHandles = handles;
        handles = new GameObject[handles.Length+1];
        int totalLength = handles.Length;
        if (place == -1) place = totalLength - 1;
        for (int i = 0, pusher = 0; i < totalLength; i++)
        {
            if(i == place)
            {
                Debug.LogWarning("found gen point: " + i);
                pusher++;
                handles[i] = Instantiate(handlePrefab, oldHandles[math.min(i, oldHandles.Length - 1)].transform.position + Vector3.right, Quaternion.identity, transform);
            }
            else
            {
                handles[i] = oldHandles[i-pusher];
            }
        }
        regenPlusObjects(false);
        regenControlPoints();
        generateChain(controlPositions, precision, pointAmount);
    }

    public int findMyObjectIndex(GameObject searchedObject)
    {
        for(int i = 0; i < handles.Length;i++)
        {
            if (handles[i] == searchedObject) return i;
        }
        for(int i = 0; i < plusses.Length; i++)
        {
            if (plusses[i] == searchedObject) return i;
        }
        return -1;
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
    public bool generateChain(Vector2[] points, int precision, int chainPointAmount, int depth = 1)
    {
        if (precision < 1) return false;
        if (EventSystem.current.IsPointerOverGameObject()) return false;

        // add 1 to precesion because 1 is 'wasted' on the first point which is on 0 anyway what
        precision++;

        //int pointAmount = 5;

        if (points.Length < 2) return false;

        string arcString = "";
        float[] arcLengths = generateArcLengths(points, out arcString);
        Vector2[] computedPoints = new Vector2[1];
        for (int i = 0; i < depth; i++)
        {
            computedPoints = generateInnerChain(points, precision, chainPointAmount, arcLengths);
            if (i == depth - 1) break;


        }
        Debug.Log("arclengts are: " + "\n" + arcString);
        if (generateDebugLines)
        {
            generateDebugCurve(computedPoints);
        }
        return true;
    }

    Vector2[] generateInnerChain(Vector2[] points, int precision, int chainPointAmount, float[] arcLengths)
    {
        Vector2[] computedPoints = new Vector2[chainPointAmount + 1];
        for (int i = 0; i <= chainPointAmount; i++)
        {
            computedPoints[i] = biasedBezier.biasedPoint2(i / (float)chainPointAmount, points, arcLengths);
        }
        //Vector2[] computedPoints = bezeirCurve.PointList2(new List<Vector2>(points), precision).ToArray();
        //Debug.Log("minimum reselution, distance would be: " + Vector2.Distance(controlPositions[0], controlPositions[controlPositions.Length-1]));
        if (printDistances)
        {
            string x = "";
            for (int i = 1; i < computedPoints.Length; i++)
            {
                x += "distance " + i + " is: " + Vector2.Distance(computedPoints[i], computedPoints[i - 1]) + "\n";
            }
            Debug.Log(x);
            printDistances = false;
        }
        return computedPoints;
    }

    float[] generateArcLengths(Vector2[] points, out string arcString)
    {
        float[] arcLengths = new float[precision];
        arcLengths[0] = 0f;
        float lastArcT = 0f;
        arcString = "";
        for (int i = 1; i < precision; i++)
        {
            // generate t values 0 through 1 with [precesion] points
            float t = i / (float)precision;
            Debug.Log("t is: " + t);
            arcLengths[i] = arcLengths[i - 1] + Vector2.Distance(
                bezeirCurve.Point2(lastArcT, points),
                bezeirCurve.Point2(t, points)
            );
            lastArcT = t;
            arcString += i + ": " + arcLengths[i] + "\n";
        }
        return arcLengths;
    }

    void generateDebugCurve(Vector2[] computedPoints)
    {
        //Debug.Log("drawing the following lines:");
        for (int i = 0; i < computedPoints.Length; i++)
        {
            //Debug.Log(computedPoints[i]);
        }
        DrawLine(computedPoints);
    }

    LineRenderer lr;
    void DrawLine(Vector2[] points)
    {
        //LineRenderer lr = lineRenderer;
        //if(!newGameObject)
        //{
        //}
        if (!(lr = gameObject.GetComponent<LineRenderer>())) lr = gameObject.AddComponent<LineRenderer>();
        //lr.useWorldSpace = false;
        lr.widthMultiplier = 0.03f;
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
