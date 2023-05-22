using System;
using System.Collections;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 spawnPoint;
    

    public TrailRenderer trailRenderer;

    public GameObject deathParticleSystem;

    public float killDelay;

    public float killtimer;

    public Rigidbody2D rb;

    bool dead = false;

    public bool froze;

    public Animation anim;
    void Start()
    {
        
        spawnPoint = transform.position;

        dead = false;
        
    }



    // Update is called once per frame

    float closest(float n, float q){
        return (Mathf.Round(n / q) * q);
    }
    void Update()
    {
        // Debug.Log(45 - Math.Abs((Math.Abs(263.1082)  % 90 - 45)));
        if(rb.angularVelocity == 0 &&
        ((45 - Mathf.Abs(Math.Abs(rb.rotation) % 90 - 45)) is < 90 and > 0.4f) &&
        !froze &&
        rb.velocity == Vector2.zero &&
        !dead){
            // StartCoroutine(freeze());
            Debug.Log("player fix");
            StartCoroutine(fix());

            // rb.SetRotation(closest(rb.rotation, 90));
            // transform.rotation = Quaternion.identity;
        }

        if(froze){
            // rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            // rb.AddTorque(1f);
        }
        else {
            // rb.constraints = RigidbodyConstraints2D.None;
            
        }

        
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("killPlayer")){
            Debug.Log("killing player");
            
            die();
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("spawnPoint")){
            spawnPoint = new Vector3(other.transform.position.x, other.transform.position.y, transform.position.z);
        }
    }

    void die(){
        if (dead) return;
        rb.angularVelocity = 0;
        rb.velocity = Vector2.zero;
        rb.simulated = false;

        trailRenderer.Clear();
        // transform.localScale = Vector3.zero;

        StartCoroutine(death());

    }

    void enableRb(){
        rb.simulated = true;
        dead = false;
    }

    IEnumerator death()
    {
        dead = true;

        Instantiate(deathParticleSystem, transform.position, Quaternion.identity);
        anim.Play("die");
        yield return new WaitForSeconds(0.7f);

        transform.localScale = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = spawnPoint;
        trailRenderer.Clear();

        yield return new WaitForSeconds(1);

        


        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        iTween.ScaleTo(gameObject, iTween.Hash("x", 1, "y", 1, "z", 1, "time", 1, "oncomplete", "enableRb", "oncompletetarget", gameObject));
        transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    IEnumerator freeze(){
        froze = true;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.freezeRotation = true;

        yield return new WaitForSeconds(1f);

        rb.constraints = RigidbodyConstraints2D.None;
        rb.freezeRotation = false;
        rb.WakeUp();

        yield return new WaitForSeconds(1f);
        froze = false;

    }
    IEnumerator fix(){
        rb.simulated = false;
        froze = true;

        yield return null;
        rb.simulated = true;

        yield return null;

        froze = false;

    }

}

