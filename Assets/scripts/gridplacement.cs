using System.Xml.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class gridplacement : MonoBehaviour
{
    public GameObject blockPrefab;
    public GameObject middlePiece;
    public GameObject cornerPrefab;
    public Vector2 size;

    public Transform parent;
    public Transform cornerParent;

    public float zPos;

    public Vector3[] Xsides;
    public Vector3[] Ysides;
    public Vector3[] sides;


    int n;


    // Start is called before the first frame update
    void Start()
    {
        sides = Xsides.Union(Ysides).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)){
            addBlock();

        }
    }

    void addBlock(){
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.x = Mathf.Round(worldPosition.x / 2.5f) * 2.5F;
        worldPosition.y = Mathf.Round(worldPosition.y / 2.5f) * 2.5F;
        worldPosition.z = zPos;



        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);

            if(child.position == worldPosition){
                return;
            }

        }
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);
            
            foreach (Vector3 side in sides)
            {
                if(worldPosition + side == child.position){
                    Instantiate(middlePiece, worldPosition + side/2 + Vector3.back, Quaternion.identity, parent);
                }
                else{
                    if(side.x != 0){
                        foreach(Vector3 Yside in Ysides){
                            if(worldPosition + side + Yside == child.position){
                                Debug.Log(worldPosition + side + Yside);

                                GameObject corner = Instantiate(cornerPrefab, worldPosition + side, Quaternion.identity, cornerParent);

                                corner.transform.LookAt((worldPosition + worldPosition + side + Yside) / 2);
                                
                            }
                        }
                    }
                }

            }
        }

            

            // if(child.)
        GameObject newObject = Instantiate(blockPrefab, worldPosition, Quaternion.identity, parent);
    }
}
