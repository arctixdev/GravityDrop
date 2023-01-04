using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smmothCamRot : MonoBehaviour
{
    // Start is called before the first frame update

    public int rotOffset;
    public float time;

    public float rot;
    void Start()
    {
        rot = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
            rot += 180;
            iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(0, 270, rot), "time", time, "easetype", iTween.EaseType.easeInOutCubic));
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.D)) {
            rot -= 90;
            iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(0, 270, rot), "time", time, "easetype", iTween.EaseType.easeInOutCubic));
        }
        // if(Input.GetKeyDown(KeyCode.DownArrow)) {
        //     iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(0, 270, transform.eulerAngles.z+rotOffset+0), "time", time, "easetype", iTween.EaseType.easeInOutCubic));
        // }
        if(Input.GetKeyDown(KeyCode.RightArrow)  || Input.GetKeyDown(KeyCode.A)) {
            rot += 90;
            iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(0, 270, rot), "time", time, "easetype", iTween.EaseType.easeInOutCubic));
        }
        rot = rot % 360;
    }
    // float rotateFloat(float rotateAmount)
    // {
    //     if(rot )
    // }
}
