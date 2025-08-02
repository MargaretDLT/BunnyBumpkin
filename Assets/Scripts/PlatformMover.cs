using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public Vector3 platformMove;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = platformMove; 
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = platformMove;
    }

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.tag != "Player")
        {
            platformMove.x = platformMove.x * -1.0f;
            rb.linearVelocity = platformMove;
        }
	}
}
