using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;
public class tileMapHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Tilemap grid;

    public List<Tile> outSideTiles = new List<Tile>();
    public List<Tile> inSideTiles = new List<Tile>();
    public List<Tile> sqareTiles = new List<Tile>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addBlock(int BX, int BY){
        
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                updateTile((BX * 2) + x - 1, (BY * 2) + y - 1, BX, BY);
                // placeTile(x, y, outSideTiles);
            }
        }
    }

    bool isCornerTile(Tile tile){
        return inSideTiles.Contains(tile) || tile == null;
    }

    public void removeBlock(){

    }

    bool checkTile(int x, int y, int BX, int BY, bool place){
        // Debug.Log(x - (x % 2));
        if(x - BX is -1 or 0 && y - BY is -1 or 0) return place;
        return !isCornerTile(grid.GetTile<Tile>(new Vector3Int(x, y, 0)));
        // return true;
    }

    void updateTile(int x, int y, int BX, int BY){
        int TilesAround = 0;

        
        if(checkTile(x + -1, y, BX, BY)) TilesAround ++;
        if(checkTile(x + 1, y, BX, BY)) TilesAround ++;
        if(checkTile(x, y + -1, BX, BY)) TilesAround ++;
        if(checkTile(x, y + 1, BX, BY)) TilesAround ++;
        


        Debug.Log(Math.Floor(-0.6));
        if(!checkTile(x, y, BX, BY)){
            switch (TilesAround)
            {
                case <= 1:
                    removeTile(x, y);
                    break;
                case >= 2:
                    placeTile(x, y, inSideTiles);
                    break;
            }
            return;
        }
        switch (TilesAround){
            case >= 3:
                placeTile(x, y, sqareTiles);
                break;
            default:
                placeTile(x, y, outSideTiles);
                break;
            
        }
        return;



    }

    void placeTile(int x, int y, List<Tile> tiles){
        grid.SetTile(new Vector3Int(x-1, y-1, 0), tiles[(Math.Abs(x % 2) + Math.Abs(y % 2 * 2))]);
    }
    void removeTile(int x, int y){
        grid.SetTile(new Vector3Int(x, y, 0), null);
    }

    

}

