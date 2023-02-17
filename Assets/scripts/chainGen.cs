
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chainGen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject chain;

    public int chainLength;

    public float DistancePerChain;

    [SerializeField]
    bool lockFront;
    [SerializeField]
    bool lockEnd;
    [SerializeField]
    bool moveWithMouse;

    Rigidbody2D endrb;

    int chaincount = 0;

    Camera _cam;
    void Start()
    {
        _cam = Camera.main;
        genChain(transform.position);
    }

    void genChain(Vector3 startPos){
        chaincount++;
        GameObject head = Instantiate(new GameObject("Chain (" + chaincount + ")"), startPos, Quaternion.identity, transform);
        GameObject NewChain = Instantiate(chain, startPos, Quaternion.identity, head.transform);
        NewChain.transform.GetChild(0).GetComponent<HingeJoint2D>().connectedBody = null;
        NewChain.transform.GetChild(0).GetComponent<HingeJoint2D>().autoConfigureConnectedAnchor = false;
        NewChain.transform.GetChild(1).GetComponent<HingeJoint2D>().autoConfigureConnectedAnchor = false;
        Rigidbody2D rb = NewChain.transform.GetChild(1).GetComponent<Rigidbody2D>();
        for (int i = 1; i < chainLength; i++)
        {
            NewChain = Instantiate(chain, startPos + Vector3.down * i * DistancePerChain * transform.localScale.y, Quaternion.identity, head.transform);
            NewChain.transform.GetChild(0).GetComponent<HingeJoint2D>().connectedBody = rb;
            NewChain.transform.GetChild(0).GetComponent<HingeJoint2D>().autoConfigureConnectedAnchor = false;
            NewChain.transform.GetChild(1).GetComponent<HingeJoint2D>().autoConfigureConnectedAnchor = false;

            rb = NewChain.transform.GetChild(1).GetComponent<Rigidbody2D>();


        }
        if(lockEnd){
            endrb = transform.GetChild(transform.childCount - 1).GetChild(1).GetComponent<Rigidbody2D>();
            endrb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // this.gameObject.transform.GetChild(0).GetChild(0).position = _cam.ScreenToWorldPoint(Input.mousePosition);
        // Vector3 mspos = _cam.ScreenToWorldPoint(Input.mousePosition);
        // endrb.MovePosition(_cam.ScreenToWorldPoint(Input.mousePosition));
        if(moveWithMouse){
            transform.GetChild(transform.childCount - 1).GetChild(1).position = _cam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 4);
        }

        if(Input.GetMouseButtonDown(0)){
            genChain(_cam.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 4);
        }
        

    }
}
