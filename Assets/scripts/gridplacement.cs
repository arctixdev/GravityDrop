
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
 using UnityEngine.UI;

public class gridplacement : MonoBehaviour
{
    public List<GameObject> blockPrefabs;
    public GameObject connector;
    public GameObject cornerPrefab;
    public Vector2 size;

    public Transform parent;
    public Transform connectorParent;
    public Transform cornerParent;

    public float zPos;

    public Vector3[] Xsides;
    public Vector3[] Ysides;
    public Vector3[] sides;

    public List<Vector2> blockPlacements = new List<Vector2>();

    List<List<int>> mapList = new List<List<int>>();

    public int blockType = 0;

    public string MapString;

    public ToggleGroup toggleGroup;

    public bool disablePhysicsOnStart = true; 

    List<block> oldblockpos = new List<block>();

    bool isSimRunning;

    public GameObject player;

    

    [SerializeField]
    public GameObject editorCam;
    [SerializeField]
    public GameObject playingCam;




    // Start is called before the first frame update

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(disablePhysicsOnStart){
            Physics2D.autoSimulation = false;
        }
    }
    void Start()
    {
        
        importMapAsString(MapString);

        sides = Xsides.Union(Ysides).ToArray();
        List<List<int>> list = exportMap();


        foreach (List<int> row in list)
        {
            foreach (int element in row)
            {
                // Do something with the element
                Debug.Log(element);
            }
        }



    }

    

    // Update is called once per frame
    void Update()
    {
        // blockType += Mathf.RoundToInt(Input.mouseScrollDelta.y);

        if(!EventSystem.current.IsPointerOverGameObject()){
            
            if(Input.GetMouseButton(0) ){
                
                Vector3 mousePosition = Input.mousePosition;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                int x = Mathf.RoundToInt(worldPosition.x / 2.5f);
                int y = Mathf.RoundToInt(worldPosition.y / 2.5f);
                if(blockType == 3){
                    removeBlock(x, y);
                }
                else {
                    addBlock(x, y, blockType, 0);
                }

            }
        }
    }

    List<List<int>> exportMap(){

        return mapList;
    }
    String exportMapAsString(){
        String MapString = "";
        foreach (List<int> row in mapList)
        {
            foreach (int element in row)
            {
                // Do something with the element
                
                MapString += element;
                MapString += ' ';

            
            
            }
            MapString += ',';
        }
        // Debug.Log(MapString);
        return MapString;
    }

    void importMapAsString(string mapStr){
        foreach (string bstring in mapStr.Split(','))
        {
            if(bstring == "") continue;

            string[] elements = bstring.Split(' ');
            int[] numbers = new int[elements.Length];

            for (int i = 0; i < elements.Length; i++)
            {
                if(elements[i] == "") continue;
                numbers[i] = int.Parse(elements[i]);
            }
            addBlock(
                numbers[0],
                numbers[1],
                numbers[2],
                numbers[3]
            );
        }

    }

    void removeBlock(int x, int y){
        for (int b = 0; b < parent.transform.childCount; b++)
        {
            Transform block = parent.transform.GetChild(b);

            if(block.position.x == x * 2.5f && block.position.y == y * 2.5f){

                if(block.CompareTag("roundetBlock")){
                    Debug.Log("is roundet block");

                    for (int i = 0; i < connectorParent.transform.childCount; i++)
                    {
                        Transform connector = connectorParent.transform.GetChild(i);
    
                        if(Mathf.Abs(connector.position.x - block.position.x) < 1.3 && Mathf.Abs(connector.position.y - block.position.y) < 1.3){
                            Destroy(connector.gameObject);
                        }
                    }
                    for (int i = 0; i < cornerParent.transform.childCount; i++)
                    {
                        Transform corner = cornerParent.transform.GetChild(i);

                        if(Mathf.Abs(corner.position.x - block.position.x) < 1.3 && Mathf.Abs(corner.position.y - block.position.y) < 1.3){
                            int count = 0;
                            
                            for (int d = 0; d < parent.transform.childCount; d++)
                            {
                                Transform bb = parent.transform.GetChild(d);
            
                                if(Mathf.Abs(bb.position.x - corner.position.x) < 1.3 && Mathf.Abs(bb.position.y - corner.position.y) < 1.3){
                                    count ++;
                                }
                                if(count > 3){
                                    break;
                                }
                            }
                            
                            if(count == 3) {
                                Destroy(corner.gameObject);

                            }


                        }
                    }


                    


                }

                
                Destroy(block.gameObject);

                var itemToRemove = mapList.Single(r => r[0] == x && r[1] == y);
                mapList.Remove(itemToRemove);

            }
        }

    }



    void addBlock(int x, int y, int blockType, int Rotation){
        // Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = new Vector3();
        worldPosition.x = x * 2.5F;
        worldPosition.y = y * 2.5F;

        worldPosition.z = zPos;


        
        // for (int i = 0; i < parent.transform.childCount; i++)
        // {
        //     Transform child = parent.transform.GetChild(i);

        //     if(child.position == worldPosition){
        //         return;
        //     }

        // }
        List<int> block = new List<int>() {
                                x, 
                                y,
                                blockType,
                                Rotation
                            };
        // foreach (List<int> row in mapList)
        // {
        //     Debug.Log("-----------");
        //     if(row.SequenceEqual(block)) {
                
        //         return;
        //     }
        // }

        if(mapList.Any(x => x.SequenceEqual(block))) return;

        mapList.Add(block);

        blockPlacements.Add(new Vector2(x, y));

        MapString = exportMapAsString();
        
        // 0 -1 0 0 ,0 -2 0 0 ,0 -3 0 0 ,-2 -2 0 0 ,-1 -2 0 0 ,1 -2 0 0 ,2 -2 0 0 ,2 -3 0 0 ,2 -4 0 0 ,2 -5 0 0 ,1 -5 0 0 ,0 -5 0 0 ,-1 -5 0 0 ,-2 -5 0 0 ,-2 -4 0 0 ,-2 -3 0 0 ,-1 -3 0 0 ,1 -3 0 0 ,1 -4 0 0 ,0 -4 0 0 ,-1 -4 0 0 ,0 -6 0 0 ,0 -7 0 0 ,0 -8 0 0 ,2 -8 0 0 ,1 -8 0 0 ,-1 -8 0 0 ,-2 -8 0 0 ,-2 -9 0 0 ,-1 -9 0 0 ,0 -9 0 0 ,1 -9 0 0 ,2 -9 0 0 ,3 -8 0 0 ,3 -9 0 0 ,-3 -8 0 0 ,-3 -9 0 0 ,-4 -9 0 0 ,-5 -9 0 0 ,-6 -9 0 0 ,-4 -8 0 0 ,-6 -10 0 0 ,-6 -11 0 0 ,-6 -12 0 0 ,4 -9 0 0 ,4 -10 0 0 ,5 -10 0 0 ,4 -11 0 0 ,4 -12 0 0 ,5 -11 0 0 ,5 -12 0 0 ,-2 -10 0 0 ,-1 -10 0 0 ,-1 -11 0 0 ,-1 -12 0 0 ,-1 -13 0 0 ,-2 -12 0 0 ,-2 -11 0 0 ,-2 -13 0 0 ,0 -4 0 0 ,0 -4 0 0 ,0 -4 0 0 ,0 -4 0 0 ,0 -4 0 0 ,0 -4 0 0 ,
        if(blockType != 0) worldPosition += Vector3.back * 2;
        GameObject newObject = Instantiate(blockPrefabs[blockType], worldPosition, Quaternion.identity, parent);
        if(blockType != 0) return;
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);
            
            foreach (Vector3 side in sides)
            {
                if(worldPosition + side == child.position){
                    Instantiate(connector, worldPosition + side/2 + Vector3.back, Quaternion.identity, connectorParent);
                }
                
                else if(side.x != 0){
                    foreach(Vector3 Yside in Ysides){
                        if(worldPosition + side + Yside == child.position){
                            // Debug.Log(worldPosition + side + Yside);

                            GameObject corner = Instantiate(cornerPrefab, worldPosition + (side + Yside) / 2, Quaternion.identity, cornerParent);

                            // corner.transform.LookAt((worldPosition + worldPosition + side + Yside) / 2);
                            
                        }
                    }
                }
                

            }
        }

            

            // if(child.)
    }

    public void updateSelectedItem(){

        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        Debug.Log(toggle.transform.GetSiblingIndex());
        blockType = toggle.transform.GetSiblingIndex();
    }
    
    public void StartOrStopSim(){
        if(isSimRunning) StopSim();
        else StartSim();
    }

    void StartSim(){

        

        swichCam(true);
        oldblockpos.Clear();

        for (int b = 0; b < parent.transform.childCount; b++)
        {
            GameObject block = parent.transform.GetChild(b).gameObject;

            if(block.GetComponent<Rigidbody2D>() != null){
                oldblockpos.Add(new block(block.gameObject));

            }

        }

        oldblockpos.Add(new block(player));

        Physics2D.autoSimulation = true;


    }
    void StopSim(){


        swichCam(false);
        Physics2D.autoSimulation = false;
        foreach (block b in oldblockpos)
        {
            b.resetTransform();
        }
    }

    void swichCam(bool isPlaying){
        isSimRunning = isPlaying;
        editorCam.SetActive(!isPlaying);
        playingCam.SetActive(isPlaying);
    }
}








class block{

    private Vector3 pos;
    private Quaternion rot;
    private GameObject gameObject;
    public block( GameObject igameObject){
        pos = igameObject.transform.position;
        rot = igameObject.transform.rotation;
        gameObject = igameObject;
    }

    public void resetTransform(){
        gameObject.transform.position = pos;
        gameObject.transform.rotation = rot;

        // Debug.Log("eff");

    }
}