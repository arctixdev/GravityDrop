using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;
public class mapLoader : MonoBehaviour
{
    [SerializeField] List<GameObject> blockPrefabs;
    [SerializeField] Transform OtherBlocksParent;
    HashSet<List<int>> mapList = new HashSet<List<int>>();

    [SerializeField] tileMapHandler tileMapHandler;

    [SerializeField] float zPos;


    public HashSet<List<int>> importMapFromFile(string MapName){
        StartCoroutine(importMapFromFileIE(MapName));
        return mapList;

    }

    IEnumerator importMapFromFileIE(string MapName)
    {
        clearMap();
        Debug.Log("importing map with name: " + MapName + " and info: " + SaveSystem.ReadString(MapName.Replace(" ", "-")));
        yield return null;
        yield return null;
        importMapAsString(SaveSystem.ReadString(MapName.Replace(" ", "-")));
    }

    void clearChildren(Transform parent){
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }

    public HashSet<List<int>> clearMap(){
        clearChildren(OtherBlocksParent);
        tileMapHandler.clearMap();

        mapList = new HashSet<List<int>>();
        return mapList;
    }

    void addBlock(int x, int y, int blockType, int Rotation){
        Vector3 worldPosition = new Vector3(x * 2.5F, y * 2.5F, zPos);


        List<int> block = new List<int>() {
                                x, 
                                y,
                                blockType,
                                Rotation
                            };

        if(mapList.Any(x => x.SequenceEqual(block))) return;


        mapList.Add(block);


        // MapString = exportMapAsString();
        
        if(blockType != 0){
            worldPosition += Vector3.back * 2;
            Instantiate(blockPrefabs[blockType], worldPosition, Quaternion.Euler(0, 0, Rotation * 90), OtherBlocksParent);
            return;
        }
        
        tileMapHandler.changeBlock(x, y, true);

            // if(child.)
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

}
