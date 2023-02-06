using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoSave : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public float delay;

    [SerializeField]
    public float startDelay;

    public gridplacement script;
    void Start()
    {
        script.InvokeRepeating("hellooo", startDelay, delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
