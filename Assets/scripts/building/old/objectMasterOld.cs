using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
enum UIType
{
    slider,
    button,
    checkbox
}
[System.Serializable]
struct namedValueUI
{
    /// <summary>
    /// the type of gameobject should be changed by this, requires tag system
    /// </summary>
    public string changeId;
    public UIType type;
    public string change;
    public GameObject Object;
}
public class objectMaster : MonoBehaviour
{

    Camera mainCam;
    [SerializeField] private bool debugSelect;


    [SerializeField] private namedValueUI[] UIElements;
    // Start is called before the first frame update
    void Start()
    {
         mainCam = Camera.main;
    }

    void select(selectable sel)
    {
        sel.select();
        foreach(namedValueUI element in UIElements)
        {
            string name = element.changeId;
            tagSystem ts = sel.gameObject.GetComponent<tagSystem>();
            if (ts.getTag("ch_"+name))
            {
                if(ts.getTag("ch_freeBox"))
                {
                    //if(element.type == UIType.button)
                    //{

                    //}
                    if(element.type == UIType.slider)
                    {
                        if(element.change == "wheight") element.Object.GetComponent<valueHandler>().floatValueEvent += sel.setMainWheight;
                    }
                    //if(element.type == UIType.checkbox)
                    //{

                    //}
                }
                if (ts.getTag("ch_oneDirBox"))
                {
                    //if (element.type == UIType.button)
                    //{

                    //}
                    if (element.type == UIType.slider)
                    {
                        if (element.change == "wheight") element.Object.GetComponent<valueHandler>().floatValueEvent += sel.setMainWheight;
                    }
                    //if (element.type == UIType.checkbox)
                    //{

                    //}
                }
                if (ts.getTag("ch_chain"))
                {
                    //if (element.type == UIType.button)
                    //{

                    //}
                    if (element.type == UIType.slider)
                    {
                        //if (element.change == "strength") element.Object.GetComponent<valueHandler>().floatValueEvent +=
                    }
                    //if (element.type == UIType.checkbox)
                    //{

                    //}
                }
                if (ts.getTag("ch_fan"))
                {
                    //if (element.type == UIType.button)
                    //{

                    //}
                    //if (element.type == UIType.slider)
                    //{
                    //    element.Object.GetComponent<valueHandler>().floatValueEvent +=
                    //}
                    //if (element.type == UIType.checkbox)
                    //{

                    //}
                }
                if (ts.getTag("ch_boucePad"))
                {
                    //if (element.type == UIType.button)
                    //{

                    //}
                    //if (element.type == UIType.slider)
                    //{
                    //    element.Object.GetComponent<valueHandler>().floatValueEvent +=
                    //}
                    //if (element.type == UIType.checkbox)
                    //{

                    //}
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPos = Vector3.zero;
        if(Input.GetKey(KeyCode.Mouse0))
        {
            // get mouse -> grid pos.
            if (mainCam == null) mainCam = Camera.main;
            worldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int gridPos = (Vector2Int)Vector3Int.RoundToInt(worldPos / 2.5f);
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (debugSelect) Debug.Log("casting for object");
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if(hit.collider != null)
            {
                if(debugSelect) Debug.Log("hit object. finding selectable component");
                selectable sel;
                bool t = hit.collider.gameObject.TryGetComponent<selectable>(out sel);
                if(!t)
                {
                    if (debugSelect) Debug.Log("didnt find selectable component. finding selecteble child component");
                    selectableChild selC;
                    bool t2 = hit.collider.gameObject.TryGetComponent<selectableChild>(out selC);
                    if (t2)
                    {
                        if (debugSelect) Debug.Log("found selectableChild component");
                        bool t3 = selC.master.TryGetComponent<selectable>(out sel);
                        if(t3)
                        {
                            select(sel);
                        }
                    }
                }
                else
                {
                    select(sel);
                }
            }
        }
    }
}
