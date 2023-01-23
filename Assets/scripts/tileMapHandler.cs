using System;
using System.Security.Cryptography.X509Certificates;
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
                updateTile((BX * 2) + x - 2, (BY * 2) + y - 2, BX, BY);
                // placeTile(x, y, outSideTiles);
            }
        }
    }

    bool isCornerTile(Tile tile){
        return inSideTiles.Contains(tile) || tile == null;
    }

    public void removeBlock(){

    }

    bool checkTile(int x, int y, int BX, int BY){
        // Debug.Log(x - (x % 2));
        return (x - (x % 2) == BX * 2 && y - (y % 2) == BY * 2)|| !isCornerTile(grid.GetTile<Tile>(new Vector3Int(x, y, 0)));
        // return true;
    }

    void updateTile(int x, int y, int BX, int BY){
        int TilesAround = 0;

        for (int i = -1; i < 3; i += 2)
        {
            if(checkTile(x + i, y, BX, BY)) TilesAround ++;
            if(checkTile(x, y + i, BX, BY)) TilesAround ++;
        }
        

        Debug.Log(checkTile(x, y, BX, BY));
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
        grid.SetTile(new Vector3Int(x, y, 0), tiles[(Math.Abs(x % 2) + Math.Abs(y % 2 * 2))]);
    }
    void removeTile(int x, int y){
        grid.SetTile(new Vector3Int(x, y, 0), null);
    }

    

}

