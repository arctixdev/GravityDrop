using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        spawnPoint = transform.position;

        dead = false;
        
    }



    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("killPlayer")){
            Debug.Log("killing player");
            
            die();
        }
    }

    void die(){
        dead = true;
        killtimer = killDelay;
        rb.velocity = Vector2.zero;
        rb.simulated = false;

        Instantiate(deathParticleSystem, transform.position, Quaternion.identity);
        trailRenderer.Clear();
        transform.localScale = Vector3.zero;

        StartCoroutine(death());

    }

    void enableRb(){
        rb.simulated = true;
        dead = false;
    }

    IEnumerator death()
    {

        transform.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(0.5f);

        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = spawnPoint;
        trailRenderer.Clear();

        yield return new WaitForSeconds(1);


        iTween.ScaleTo(gameObject, iTween.Hash("x", 1, "y", 1, "z", 1, "time", 1, "oncomplete", "enableRb", "oncompletetarget", gameObject));
        transform.rotation = Quaternion.Euler(0, 0, 0);

    }

}

