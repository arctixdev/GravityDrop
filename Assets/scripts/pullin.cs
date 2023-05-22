using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pullin : MonoBehaviour
{
    // Start is called before the first frame update

    
    [SerializeField]
    private List<string> whiteListTags = new List<string>();
    
    private List<Rigidbody2D> rigidbody2Ds = new List<Rigidbody2D>();

    [SerializeField]
    private float radius;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Rigidbody2D rb in rigidbody2Ds)
        {
            rb.AddForce((rb.position - new Vector2(transform.position.x, transform.position.y).normalized));
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(whiteListTags.Contains(other.tag)){
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rigidbody2Ds.Add(rb);
            }
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rigidbody2Ds.Remove(rb);
        }
    }



}
