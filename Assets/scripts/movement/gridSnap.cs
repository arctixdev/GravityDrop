using UnityEngine;

public class gridSnap : MonoBehaviour
{
    [SerializeField] private float maxRotationVariation;
    [SerializeField] private float maxSpeed;

    void Update()
    {
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        if (rb.velocity.magnitude < maxSpeed || UnityEngine.Mathf.Abs(rb.gameObject.transform.rotation.z) % 90 < maxRotationVariation)
        {
            rb.gameObject.transform.rotation = Quaternion.Euler(new Vector3(rb.gameObject.transform.rotation.x, rb.gameObject.transform.rotation.y, UnityEngine.Mathf.Round(rb.gameObject.transform.rotation.eulerAngles.z/90)*90));
        }
    }
}