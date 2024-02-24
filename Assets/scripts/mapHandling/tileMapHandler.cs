using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;
public class tileMapHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Tilemap grid;
    public Tilemap collisonGrid;

    public TileBase ruleTile;
    public Tile collisonTile;

    public void Start()
    {
        changeBlock(2, 2, true);
    }

    public void changeBlock(int BX, int BY, bool place){
        TileBase tile = place ? ruleTile : null;
        int x = BX * 2;
        int y = BY * 2;
        grid.SetTile(new Vector3Int(x, y), tile);
        grid.SetTile(new Vector3Int(x + 1, y), tile);
        grid.SetTile(new Vector3Int(x, y + 1), tile);
        grid.SetTile(new Vector3Int(x + 1, y + 1), tile);

        collisonGrid.SetTile(new Vector3Int(x, y), place ? collisonTile : null);
    }

    public void clearMap(){
        grid.ClearAllTiles();
    }

    

}

