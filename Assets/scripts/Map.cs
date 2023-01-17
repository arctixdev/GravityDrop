using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Map
{
    // Start is called before the first frame update
    public int[,] Blocks;
    public Map (int[,] IBlocks){
        Blocks = IBlocks;
    }


}
