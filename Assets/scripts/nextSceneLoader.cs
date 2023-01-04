using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextSceneLoader : MonoBehaviour
{
    [SerializeField]
    public GameObject obj;
    Camera cam;

    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        if(!IsObjectInView(obj)){
            if(IsInside(obj, gameObject)){
                Debug.Log("Next scene please");
                LoadNextScene();
            }
        }
    }

    bool IsObjectInView(GameObject obj)
    {
        Vector3 viewportPoint = cam.WorldToViewportPoint(obj.transform.position);
        return viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
    }

    bool IsInside(GameObject inner, GameObject outer)
    {
        // Get the center point of the inner game object
        Vector2 innerCenter = inner.GetComponent<Renderer>().bounds.center;

        // Get the bounding box of the outer game object
        Bounds outerBounds = outer.GetComponent<Renderer>().bounds;

        // Convert the bounding box to a 2D bounding box
        Bounds2D outerBounds2D = new Bounds2D(outerBounds.min, outerBounds.max);

        // Check if the center point of the inner game object is inside the outer bounding box
        return outerBounds2D.Contains(innerCenter);
    }

    class Bounds2D
    {
        public Vector2 min;
        public Vector2 max;

        public Bounds2D(Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
        }

        public bool Contains(Vector2 point)
        {
            return point.x >= min.x && point.x <= max.x && point.y >= min.y && point.y <= max.y;
        }
    }

    void LoadNextScene()
    {
        // Get the current scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Load the next scene
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }


}
