using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tagSystem : MonoBehaviour
{
    public string[] tags;
    /// <summary>
    /// usefull if a tag is on multiple childs and they all need to point to a single object (not needed)
    /// </summary>
    public GameObject parrent;

    public bool getTag(string tag)
    {
        foreach(var t in tags)
        {
            if (t == tag) return true;
        }
        return false;
    }
}
