using UnityEngine;

public class RollBall : MonoBehaviour
{
    public float speedNormal;
    public float speedTurbo;
    public float jumpNormalHeight;
    public float jumpSuperHeight;
    private float speedMultiplier;
    public float jumpNormalForce;
    public float jumpSuperForce;
    public float gravityScale = 1.0f;
    private Rigidbody rb;
    public bool bJumping;
    public bool bIsGrounded;
    public bool bRiding;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpNormalForce = Mathf.Sqrt(jumpNormalHeight * Physics.gravity.y * -2.0f) * rb.mass;
        jumpSuperForce = Mathf.Sqrt(jumpSuperHeight * Physics.gravity.y * -2.0f) * rb.mass;
        speedMultiplier = speedNormal;
        bJumping = false;
        bIsGrounded = false;
        bRiding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bIsGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * jumpNormalForce, ForceMode.Impulse);
                bJumping = true;
				if (bRiding)    // kludge to make platform jumping work
				{
					rb.AddForce(Vector3.up * jumpNormalForce, ForceMode.Impulse);
				}
			}

			if (Input.GetButtonDown("Fire1"))
            {
                rb.AddForce(Vector3.up * jumpSuperForce, ForceMode.Impulse);
                bJumping = true;
				if (bRiding)    // kludge to make platform jumping work
				{
					rb.AddForce(Vector3.up * jumpSuperForce, ForceMode.Impulse);
				}
			}
        }

        if(Input.GetButtonUp("Jump") || Input.GetButtonUp("Fire1"))
		{
            bJumping = false;
		}

        if(rb.linearVelocity.y < 0)
		{
            bJumping = false;
		}
    }

    private void FixedUpdate()
	{
        // controller movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speedMultiplier * Time.fixedDeltaTime);

        if (bJumping == false)
        {
            // custom gravity
            Vector3 gravity = gravityScale * Physics.gravity;
            rb.AddForce(gravity, ForceMode.Acceleration);
        }
        bIsGrounded = false;
	}

	private void OnCollisionStay(Collision collision)
	{
        bIsGrounded = true;
        if(collision.gameObject.tag == "Platform")
        {
            bRiding = true;
        }
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.tag == "Platform")
		{
			bRiding = false;
		}
	}
}
