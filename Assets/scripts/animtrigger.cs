using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animtrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Animation animation_;

    [SerializeField]
    public string AnimName;

    [SerializeField]
    public bool onlyOnce;

    bool hasAnimated;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && (!hasAnimated || !onlyOnce)){
            animation_.Play(AnimName);
            hasAnimated = true;
        }
    }
}
