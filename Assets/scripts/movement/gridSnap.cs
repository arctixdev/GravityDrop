using unityEngine;

public class gridSnap : monoBehaviur
{
    [serelizefield] private float maxRotationVariation;
    [serelizefield] private float maxSpeed;

    void Update()
    {
        rigidbody rb = this.gameObject.getComponent<rigidbody>();
        if (rb.velocity < maxSpeed || unityEngine.Mathf.abs(rb.gameObject.transform.rotation.z) % 90 < maxRotationVariation)
        {
            rb.gameObject.transform.rotation = new Vector3(rb.gameObject.transform.rotation.x, rb.gameObject.transform.rotation.y, unityEngine.Mathf.Round(rb.gameObject.transform.rotation/90)*90))
        }
    }
}