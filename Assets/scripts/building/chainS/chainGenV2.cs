using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Mathematics;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using System.Threading;
using UnityEngine.UIElements;

public class chainGenV2 : MonoBehaviour
{
    [SerializeField] private GameObject[] handles;
    [SerializeField] private GameObject handlePrefab;
    [SerializeField] private GameObject plusPrefab;
    public int precision;
    public int pointAmount;
    public int depth;

    public int chainSegmentAmount;
    /// <summary>
    /// this is HIGLY temporary, a system for pushing them away from the line dynamicly WILL be implemented
    /// </summary>
    [SerializeField] private Vector3 plusDefaultOffset;
    //[SerializeField]
    private GameObject[] plusses;
    [SerializeField] private bool printDistances;
    [SerializeField] private bool generateDebugLines;
    [SerializeField] private bool useAsyncFunction;
    [SerializeField] private float chainUpdateInterval;
    private Vector2[] oldHandles;
    private int oldHandleCount;
    Vector2[] controlPositions;

    bool shouldReDraw;

    System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
    Vector3 plusModifyer;
    private void Start()
    {
        timer.Start();
        oldHandles = new Vector2[handles.Length];
        for (int i = 0; i < handles.Length; i++)
        {
            oldHandles[i] = handles[i].transform.position;
        }
        oldHandleCount = handles.Length;
        plusModifyer = new Vector3(1, -1, 1);
    }

    bool shouldUpdate;
    // will stop chainGen from running every update
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
                if(handles.Length - 1 >= i)
                {
                    handles[i].transform.position = controlPositions[i];
                    handles[i].transform.position += new Vector3(0, 0, -0.15f);
                }
            }
            regenPlusObjects(true);
            shouldUpdate = true;
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
                // reimport handle positions
                regenControlPoints();

                //generate the chain again
                //generateChain(controlPositions, precision, pointAmount, depth);
                //this is used instead so it won't update every frame but only every chainUpdateInterval milliseconds
                shouldUpdate = true;
                
                //Debug.Log("control position " + i +" are as follows: " + controlPositions[i]);
            }
        }
        if(printDistances)
        {
            shouldUpdate = true;
        }
        doUpdateCheck();
        if (shouldReDraw)
        {
            if(generateDebugLines) DrawLine(currentComputedPoints); shouldReDraw = false;
            Debug.Log("finished drawing debug lines");
            drawChain(currentComputedPoints, chainSegmentAmount);
            makeChainPhysics();
        }
    }

    void doUpdateCheck()
    {
        if(shouldUpdate && timer.ElapsedMilliseconds > chainUpdateInterval)
        {
            timer.Restart();
            shouldUpdate = false;
            if(useAsyncFunction) generateThreadedChain(controlPositions, precision, pointAmount, depth);
            else generateChain(controlPositions, precision, pointAmount, depth);
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
        shouldUpdate = true;
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


    //Vector2[] points;
    //int precision;
    public bool generatePersistantChain()
    {
        return true;
    }

    int currentNewestChainGenIndex;
    int currentNewestChainGenStartIndex = 0;
    void generateThreadedChain(Vector2[] points, int precision, int chainPointAmount, int depth = 1, bool isThreaded = true)
    {
        Thread chainTread = new Thread(() => generateChain(points, precision, chainPointAmount, depth, isThreaded, currentNewestChainGenStartIndex));
        Debug.Log("chainGen thread initialized, starting now with thread index: "+currentNewestChainGenStartIndex);
        chainTread.Start();
        currentNewestChainGenStartIndex++;
    }

    [SerializeField] private float chainSegmentLength;
    Vector2[] currentComputedPoints;
    [SerializeField] private float currentChainSegmentLength;
    //public bool generateChain(Vector2[] points, int precision, int searchDistance = 3)
    public bool generateChain(Vector2[] points, int precision, int chainPointAmount, int depth = 1, bool isThreaded = false, int threadIndex = 0, bool autoAdjustPointAmount = true)
    {
        if (precision < 1) return false;
        if (EventSystem.current.IsPointerOverGameObject()) return false;


        // add 1 to precesion because 1 is 'wasted' on the first point which is on 0 anyway what
        precision++;

        //int pointAmount = 5;

        if (points.Length < 2) return false;

        //check if thread is too old
        if (isThreaded && threadIndex < currentNewestChainGenIndex) return false;

        string timeString = "";
        System.Diagnostics.Stopwatch total = System.Diagnostics.Stopwatch.StartNew();
        System.Diagnostics.Stopwatch time1 = new();
        System.Diagnostics.Stopwatch time2 = new();

        string arcString = "";
        float[] arcLengths = generateArcLengths(points, out arcString);
        Vector2[] computedPoints = new Vector2[1];

        if(autoAdjustPointAmount)
        {
            //set amount of chain segments based on total curve length and the length of each chain segment
            chainSegmentAmount = (int)math.round(arcLengths[arcLengths.Length - 1] / currentChainSegmentLength);
            //set amount of points to a higher value dividable by chainsegmentamount
            pointAmount = chainSegmentAmount * 4;

            Debug.Log("generating chain with: "+chainSegmentAmount+" segments and: "+pointAmount+" points");
        }

        //check if thread is too old
        if (isThreaded && threadIndex < currentNewestChainGenIndex) return false;

        for (int i = 0; i < depth; i++)
        {
            computedPoints = generateInnerChain(points, precision, chainPointAmount, arcLengths, ref timeString, ref time1, ref time2);
            if (i == depth - 1) break;

            arcLengths = new float[computedPoints.Length];
            arcLengths[0] = 0f;
            for(int j = 1; j < computedPoints.Length; j++)
            {
                arcLengths[j] = arcLengths[j - 1] + Vector2.Distance(computedPoints[j - 1], computedPoints[j]);
                arcString += i + "." + j + ": " + arcLengths[j] + "\n";
            }
        }

        //check if thread is too old
        if (isThreaded && threadIndex < currentNewestChainGenIndex) return false;
        total.Stop();

        timeString = "total binary search time was: " + time1.ElapsedMilliseconds + " milliseconds" + " or " + time1.ElapsedMilliseconds / 1000 + " seconds" + " (or " + time1.ElapsedTicks + " ticks)" + "\n\n\n" + timeString;
        timeString = "total point generation time was: " + time2.ElapsedMilliseconds + " milliseconds" + " or " + time2.ElapsedMilliseconds / 1000 + " seconds" + " (or " + time2.ElapsedTicks + " ticks)" + "\n" + timeString;

        timeString = "finished biased bezier curve in " + (total.ElapsedMilliseconds / 1000) + " seconds " + " (or " + total.ElapsedMilliseconds + " milliseconds)" + "\n\n" + timeString;
        Debug.Log(timeString);

        //check if thread is too old
        if (isThreaded && threadIndex < currentNewestChainGenIndex) return false;

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
        //Debug.Log("arclengts are: " + "\n" + arcString);
        //check if thread is too old
        if (isThreaded && threadIndex < currentNewestChainGenIndex) return false;
        currentComputedPoints = computedPoints;
        shouldReDraw = true;

        //Debug.Log("chain generated, redrawing");

        if (isThreaded)
        {

            //set currentNewestChainGen to own value so other older threads doesn't overwrite
            currentNewestChainGenIndex = threadIndex;
            Debug.Log("chainGen thread finished, returning");
        }
        return true;
    }

    Vector2[] generateInnerChain(Vector2[] points, int precision, int chainPointAmount, float[] arcLengths, ref string timeString, ref System.Diagnostics.Stopwatch time1, ref System.Diagnostics.Stopwatch time2)
    {
        Vector2[] computedPoints = new Vector2[chainPointAmount + 1];
        for (int i = 0; i <= chainPointAmount; i++)
        {
            computedPoints[i] = biasedBezier.biasedPoint2(i / (float)chainPointAmount, points, arcLengths, ref timeString, ref time1, ref time2);
        }
        //Vector2[] computedPoints = bezeirCurve.PointList2(new List<Vector2>(points), precision).ToArray();
        //Debug.Log("minimum reselution, distance would be: " + Vector2.Distance(controlPositions[0], controlPositions[controlPositions.Length-1]));

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
            //Debug.Log("t is: " + t);
            arcLengths[i] = arcLengths[i - 1] + Vector2.Distance(
                bezeirCurve.Point2(lastArcT, points),
                bezeirCurve.Point2(t, points)
            );
            lastArcT = t;
            arcString += i + ": " + arcLengths[i] + "\n";
        }
        return arcLengths;
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

    Quaternion GetRotation(Vector3 position, GameObject rotatedObject, float rotationOffset = 0)
    {
        Vector3 objectPos = rotatedObject.transform.position;
        position.x = position.x - objectPos.x;
        position.y = position.y - objectPos.y;

        float angle = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(new Vector3(0, 0, angle + rotationOffset));
    }
    int intRound(float value)
    {
        return (int)math.round(value);
    }
    [SerializeField] private GameObject[] chainPrefabs;
    private GameObject[] currentChainSegments;
    private int lastChainSegments;
    void drawChain(Vector2[] curvePoints, int chainSegments)
    {
        if(currentChainSegments != null) for(int i = 1; i < currentChainSegments.Length; i++)
        {
            Destroy(currentChainSegments[i]);
        }
        currentChainSegments = new GameObject[chainSegments + 1];


        //Time.timeScale = 0;
        // what curvepoint to instansiate the chain segment at

        float constLevel = (curvePoints.Length / (float)chainSegments);
        for (int i = 1; i <= chainSegments; i++)
        {
            //Debug.Log("generating chain segment");
            int chainType = i % chainPrefabs.Length;
            GameObject instance = Instantiate(chainPrefabs[chainType], transform);
            instance.transform.SetParent(transform, false);
            instance.transform.position = curvePoints[math.min(intRound(constLevel * i), curvePoints.Length - 1)];
            Vector3 nextPos = new Vector3(0, 0, 0);
            if (i == 1) nextPos = curvePoints[math.min(intRound(constLevel * i - 1), curvePoints.Length - 1)];
            else nextPos = currentChainSegments[i - 1].transform.position;
            //Debug.Log(curvePoints[intRound(constLevel * i)] + " + " + nextPos);
            instance.transform.rotation = GetRotation(nextPos, instance, -90);

            currentChainSegments[i] = instance;
        }

        lastChainSegments = chainSegments;
    }
    bool makeChainPhysics()
    {
        HingeJoint2D[] joints = new HingeJoint2D[lastChainSegments + 1];
        for(int i = 1; i <= lastChainSegments; i++)
        {
            int chainType = i % chainPrefabs.Length;

            currentChainSegments[i].AddComponent<Rigidbody2D>();

            HingeJoint2D joint = currentChainSegments[i].AddComponent<HingeJoint2D>();
            joint.autoConfigureConnectedAnchor = false;
            joint.anchor = new Vector2(0, currentChainSegmentLength * (chainType == 0 ? 5f : 4.5f));
            if(i == 1)
            {
                Debug.Log("making welded point");
                joint.connectedAnchor = handles[0].transform.position;
            }
            else
            {
                joint.connectedBody = currentChainSegments[i - 1].GetComponent<Rigidbody2D>();
                joint.connectedAnchor = new Vector2(0, 0);
            }

            joints[i] = joint;
        }
        HingeJoint2D endJoint = currentChainSegments[currentChainSegments.Length - 1].AddComponent<HingeJoint2D>();

        endJoint.autoConfigureConnectedAnchor = false;
        endJoint.anchor = new Vector2(0, 0);

        Debug.Log("making welded point");
        endJoint.connectedAnchor = handles[handles.Length - 1].transform.position;

        Debug.Log(joints.Length);
        for(int i = 1; i < joints.Length; i++)
        {
            Debug.Log("enabling point");
            joints[i].enabled = true;
        }
        endJoint.enabled = true;

        return true;
    }
    void generateChainSegment(Vector2[] points, int precision)
    {

    }
}
