using UnityEngine;

public class gridSnap : MonoBehaviour
{
    [SerializeField] private float maxRotationVariation;
    [SerializeField] private float minRotationVariation;
    [SerializeField] private float maxSpeed;
    [SerializeField] private bool shouldDebugFix;

    [SerializeField] private float actualRotation;

    void Update()
    {
        float rot = this.transform.rotation.eulerAngles.z;
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
        actualRotation = 45 - Mathf.Abs((Mathf.Abs(rot) % 90)-45);
        if (rb.velocity.magnitude < maxSpeed && (45-maxRotationVariation) < Mathf.Abs((Mathf.Abs(rot) % 90)-45) && (45 - minRotationVariation) > UnityEngine.Mathf.Abs((Mathf.Abs(rot) % 90) - 45))
        {
            if(shouldDebugFix) Debug.Log("fixing rotation || "+rb.velocity.magnitude+" || "+rb.gameObject.transform.rotation.z+" ||");
            rb.gameObject.transform.rotation = Quaternion.Euler(new Vector3(rb.gameObject.transform.rotation.x, rb.gameObject.transform.rotation.y, UnityEngine.Mathf.Round(rot/90)*90));
        }
    }
}