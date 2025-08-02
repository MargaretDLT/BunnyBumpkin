// SpherePursuit.cs

using UnityEngine;

public class SpherePursuit : MonoBehaviour
{
    public Transform target;          
    public float moveSpeed = 5f;      
    public float rotationSpeed = 100f; 
    public float stoppingDistance = 1.5f; 

    private Rigidbody rb;           

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("SpherePursuit: No Rigidbody found on this GameObject. Please add one.");
            enabled = false; 
        }
        if (target == null)
        {
            Debug.LogWarning("SpherePursuit: Target not assigned. Please assign the player's Transform in the Inspector.");
        }
    }

    void FixedUpdate() 
    {
        if (target != null)
        {
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            float distance = Vector3.Distance(transform.position, target.position);

            if (distance > stoppingDistance)
            {
                
                Vector3 moveForce = directionToTarget * moveSpeed;
                rb.AddForce(moveForce, ForceMode.Force);

               
                Vector3 rotationAxis = Vector3.Cross(Vector3.up, directionToTarget);
                rb.AddTorque(rotationAxis * rotationSpeed * moveSpeed * Time.fixedDeltaTime, ForceMode.Force);
            }
            else
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Sphere knocked into the player!");
       
        }
    }
}
