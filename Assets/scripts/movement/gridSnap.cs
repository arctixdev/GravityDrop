using UnityEngine;

public class gridSnap : MonoBehaviour
{
    [SerializeField] private float maxRotationVariation;
    [SerializeField] private float minRotationVariation;
    [SerializeField] private float maxSpeed;
    [SerializeField] private bool shouldDebugFix;

    void Update()
    {
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
        if (rb.velocity.magnitude < maxSpeed && UnityEngine.Mathf.Abs(rb.gameObject.transform.rotation.z) % 90 < maxRotationVariation && UnityEngine.Mathf.Abs(rb.gameObject.transform.rotation.z) % 90 > minRotationVariation)
        {
            if(shouldDebugFix) Debug.Log("fixing rotation || "+rb.velocity.magnitude+" || "+rb.gameObject.transform.rotation.z+" ||");
            rb.gameObject.transform.rotation = Quaternion.Euler(new Vector3(rb.gameObject.transform.rotation.x, rb.gameObject.transform.rotation.y, UnityEngine.Mathf.Round(rb.gameObject.transform.rotation.eulerAngles.z/90)*90));
        }
    }
}