using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Unity.Collections;
using UnityEditor.Overlays;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

using UnityEngine.Tilemaps;
public class tileMapHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Tilemap grid;
    public Tilemap collisonGrid;

    public List<CustomRuleTile> tileRules;

    public Dictionary<int, Tile> tileRulesDict = new();



    // public ArrayLayout hh = new ArrayLayout(2, 2);



    // public List<ArrayLayout> tileRules2 = new List<ArrayLayout>();

    public Tile collisonTile;

    public int toBinary(bool[] arr)
    {
        int binary = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            binary = binary << 1;
            if (arr[i])
                binary += 1;

        }

        return binary;

    }

    public void Start()
    {
        print(toBinary(new bool[4] { true, true, true, true }));
        foreach (CustomRuleTile rule in tileRules)
        {
            tileRulesDict.Add(toBinary(rule.rules.rows[0].row.Concat(rule.rules.rows[1].row).ToArray()), rule.tile);
        }



        print(tileRulesDict[3]);

        changeBlock(2, 2, true);
        changeBlock(3, 2, true);
        changeBlock(2, 3, true);

        print(tileRules[0].rules.rows.Length);
        print(tileRules[0].rules.rows[0].row[0]);

        // print(tileRules[0].rules.rows.Length);

    }

    public void changeBlock(int x, int y, bool place)
    {

        // grid.SetTile(new Vector3Int(x, y), tile);
        // grid.SetTile(new Vector3Int(x + 1, y), tile);
        // grid.SetTile(new Vector3Int(x, y + 1), tile);
        // grid.SetTile(new Vector3Int(x + 1, y + 1), tile);
        collisonGrid.SetTile(new Vector3Int(x, y), place ? collisonTile : null);
        print(collisonGrid.HasTile(new Vector3Int(x, y)));


        changeVisualTile(x, y, place);
        changeVisualTile(x + 1, y, place);
        changeVisualTile(x, y + 1, place);
        changeVisualTile(x + 1, y + 1, place);

    }

    public bool checkTile(int x, int y)
    {
        return collisonGrid.HasTile(new Vector3Int(x, y));
    }

    public void changeVisualTile(int x, int y, bool place)
    {
        if (!place)
        {
            grid.SetTile(new Vector3Int(x, y), null);
            return;
        }
        bool[] blocksAround = {
            checkTile(x - 1, y),
            checkTile(x, y),
            checkTile(x - 1, y - 1),
            checkTile(x, y - 1)
        };

        print(toBinary(blocksAround));

        Tile correctTile = tileRulesDict[toBinary(blocksAround)];

        grid.SetTile(new Vector3Int(x, y), correctTile);
    }

    public void clearMap()
    {
        // grid.ClearAllTiles();
    }



}

[System.Serializable]
public class CustomRuleTile : ISerializationCallbackReceiver
{
    public ArrayLayout rules = new ArrayLayout();
    public Tile tile;

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {

        if (rules.rows.Length == 0)
            rules = new ArrayLayout(2, 2);
    }

}



[System.Serializable]
public class ArrayLayout
{

    [System.Serializable]
    public class rowData
    {
        public bool[] row;

        public rowData(int size)
        {
            row = new bool[size];
        }
    }

    public rowData[] rows = new rowData[2];

    public ArrayLayout(int rowCount = 2, int colomnCount = 2)
    {
        rows = new rowData[rowCount];
        for (int i = 0; i < rowCount; i++)
        {
            rows[i] = new rowData(colomnCount);
        }
    }
}


