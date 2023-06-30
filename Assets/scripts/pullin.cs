using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pullin : MonoBehaviour
{
    // Start is called before the first frame update

    
    [SerializeField]
    private List<string> TagMask = new List<string>();
    [SerializeField]
    private bool UseTagMask = false;
    [SerializeField]
    private List<Rigidbody2D> rigidbody2Ds = new List<Rigidbody2D>();

    [SerializeField]
    private float radius;
    [SerializeField]
    private float power;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Rigidbody2D rb in rigidbody2Ds)
        {
            rb.AddForce((new Vector2(transform.position.x, transform.position.y) - rb.position).normalized * power);
            rb.velocity *=  0.99f;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(!UseTagMask || TagMask.Contains(other.tag)){
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
