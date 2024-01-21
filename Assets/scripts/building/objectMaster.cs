using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectMaster : MonoBehaviour
{
    Camera mainCam;
    [SerializeField] private bool debugSelect;
    // Start is called before the first frame update
    void Start()
    {
         mainCam = Camera.main;
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
                            sel.select();
                        }
                    }
                }
                else
                {
                    sel.select();
                }
            }
        }
    }
}
