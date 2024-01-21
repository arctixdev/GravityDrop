using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tagSystem : MonoBehaviour
{
    public string[] tags;

    public bool getTag(string tag)
    {
        foreach(var t in tags)
        {
            if (t == tag) return true;
        }
        return false;
    }
}
