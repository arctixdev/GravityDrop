using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSelector : MonoBehaviour
{
    Camera mainCam;
    [SerializeField]
    private bool debugSelect;
    private void Update() {
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
                selectableObject selectableComponent;
                bool t = hit.collider.gameObject.TryGetComponent<selectableObject>(out selectableComponent);
                if(!t)
                {
                    //if (debugSelect) Debug.Log("didnt find selectable component. finding selecteble child component");
                    //selectableChildOld selC;
                    //bool t2 = hit.collider.gameObject.TryGetComponent<selectableChildOld>(out selC);
                    //if (t2)
                    //{
                    //    if (debugSelect) Debug.Log("found selectableChild component");
                    //    bool t3 = selC.master.TryGetComponent<selectableOld>(out sel);
                    //    if(t3)
                    //    {
                    //        select(sel);
                    //    }
                    //}
                }
                else
                {
                    selectableComponent.select(true);
                }
            }
        }
    }
}
