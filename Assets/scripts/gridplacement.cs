using System;
using System.Xml.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class gridplacement : MonoBehaviour
{
    public GameObject blockPrefab;
    public GameObject middlePiece;
    public GameObject cornerPrefab;
    public Vector2 size;

    public Transform parent;
    public Transform cornerParent;

    public float zPos;

    public Vector3[] Xsides;
    public Vector3[] Ysides;
    public Vector3[] sides;

    public List<Vector2> blockPlacements = new List<Vector2>();

    List<List<int>> mapList = new List<List<int>>();

    public int blockType = 0;

    public string MapString;


    int n;


    // Start is called before the first frame update
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
        if(Input.GetMouseButton(0)){
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            addBlock(Mathf.RoundToInt(worldPosition.x / 2.5f), Mathf.RoundToInt(worldPosition.y / 2.5f), blockType, 0);

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
        Debug.Log(MapString);
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

    void addBlock(int x, int y, int blockType, int Rotation){
        // Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = new Vector3();
        worldPosition.x = x * 2.5F;
        worldPosition.y = y * 2.5F;

        worldPosition.z = zPos;



        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);

            if(child.position == worldPosition){
                return;
            }

        }

        mapList.Add(new List<int>() {
            x,
            y,
            blockType,
            Rotation});

        blockPlacements.Add(new Vector2(x, y));

        MapString =  exportMapAsString();

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);
            
            foreach (Vector3 side in sides)
            {
                if(worldPosition + side == child.position){
                    Instantiate(middlePiece, worldPosition + side/2 + Vector3.back, Quaternion.identity, parent);
                }
                else{
                    if(side.x != 0){
                        foreach(Vector3 Yside in Ysides){
                            if(worldPosition + side + Yside == child.position){
                                // Debug.Log(worldPosition + side + Yside);

                                GameObject corner = Instantiate(cornerPrefab, worldPosition + side, Quaternion.identity, cornerParent);

                                corner.transform.LookAt((worldPosition + worldPosition + side + Yside) / 2);
                                
                            }
                        }
                    }
                }

            }
        }

            

            // if(child.)
        GameObject newObject = Instantiate(blockPrefab, worldPosition, Quaternion.identity, parent);
    }
}
