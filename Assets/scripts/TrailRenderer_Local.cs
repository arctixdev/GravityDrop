using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class TrailRenderer_Local : MonoBehaviour {
 
    // Author: Eric Hodgson 2017
 
    // NOTE: This script should be placed on the parent of the object that's moving.
    //  It should be drawn in the parent's local space, and move/rotate with the parent.
    //  If the trail script is on the moving object, the entire trail will move and
    //   rotate with the object itself, instead of leaving a trail behind in it's
    //   parent's coordinate system.
    [SerializeField] Transform objToFollow;   //  Object that is leaving the trail
 
 
    private LineRenderer myLine;    //  internal pointer to LineRenderer Component
 
    [SerializeField] float distIncrement = 0.1f;      // How far should the object move before leaving a new trail point
    private Vector3 lastPosition;           // internal log of last trail point... could also use myLine.GetPosition(myLine.positionCount)
 
    [SerializeField] bool limitTrailLength = false;   // Toggle this to make trail be a finite number of segments
    [SerializeField] int maxPositions = 10;           // Set the number of segments here
 
    // Use this for initialization
    void Start() {
        // Get a pointer to the LineRenderer component so we can manipulate it
        myLine = GetComponent<LineRenderer>();
        // ....and make sure it's set to use local space
        myLine.useWorldSpace = false;
        // Reset the trail
        Reset();

        // InvokeRepeating("shortenLine", 0.03f, 0.03f);
    }
 
    public void Reset() {
        if(!this.enabled) return;
        // Wipe out any old positions in the LineRenderer
        myLine.positionCount = 0;
        // Then set the first position to our object's current local position
        AddPoint(objToFollow.localPosition);
    }
 
    // Add a new point to the line renderer on demand
    void AddPoint(Vector3 newPoint) {
        // Increase the number of positions to render by 1
        myLine.positionCount += 1;
        // Set the new, last item in the Vector3 list to our new point
        myLine.SetPosition(myLine.positionCount - 1, newPoint);
 
        // Check to see if the list is too long
        if (limitTrailLength && myLine.positionCount > maxPositions) {
            // ...and discard old positions if necessary
            TruncatePositions(maxPositions);
        }
 
        // Log this position as the last one logged
        lastPosition = newPoint;
    }
 
 
    // Shorten position list to the desired amount, discarding old values
    void TruncatePositions(int newLength) {
        // Create a temporary list of the desired length
        Vector3[] tempList = new Vector3[newLength];
        // Calculate how many extra items will need to be cut out from the original list
        int nExtraItems = myLine.positionCount - newLength;
        // Loop through original list and add newest X items to temp list
        for (int i=0; i<newLength; i++) {
            // shift index by nExtraItems... e.g., if 2 extras, start at index 2 instead of index 0
            tempList[i] = myLine.GetPosition(i + nExtraItems);
        }
 
        // Set the LineRenderer's position list length to the appropriate amount
        myLine.positionCount = newLength;
        // ...and use our tempList to fill it's positions appropriately
        myLine.SetPositions(tempList);
    }

    void shortenLine(int amount = 1){
        
        int len = myLine.positionCount - amount;
        if(len < 0) return;
        Vector3[] tempList = new Vector3[len];
        // Calculate how many extra items will need to be cut out from the original list
        
        // Loop through original list and add newest X items to temp list
        for (int i=0; i<len; i++) {
            // shift index by nExtraItems... e.g., if 2 extras, start at index 2 instead of index 0
            tempList[i] = myLine.GetPosition(i + amount);
        }
 
        // Set the LineRenderer's position list length to the appropriate amount
        myLine.positionCount = len;
        // ...and use our tempList to fill it's positions appropriately
        myLine.SetPositions(tempList);
    }
 
 
    // Update is called once per frame
    void Update () {
        // Get the current position of the object in local space
        Vector3 curPosition = objToFollow.localPosition;
        // Check to see if object has moved far enough
        if (Vector3.Distance(curPosition, lastPosition) > distIncrement) {
            // ..and add the point to the trail if so
            AddPoint(curPosition);
           
        }
        else {
            shortenLine();
        }

        // shortenLine();
    }
}