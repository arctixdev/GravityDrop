using System;
using System.Xml.Schema;
using System.Collections;
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


    int n;


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
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);

            if(child.position.x == x * 2.5f && child.position.y == y * 2.5f){
                Destroy(child.gameObject);
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
}
