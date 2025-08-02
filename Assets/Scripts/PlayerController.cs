using UnityEngine;
using UnityEngine.InputSystem;
//using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 1; // Adjust in Inspector
   // public TextMeshProUGUI countText; // Drag in Inspector
   // public GameObject winTextObject; // Drag in Inspector

    private Rigidbody rb;
   // private int count;
    private Vector3 startPosition;

    private float movementX;
    private float movementY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
     //   count = 0;
       // SetCountText();
      //  winTextObject.SetActive(false);
        startPosition = transform.position;
        Debug.Log(startPosition);
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
           // count += 1;
         //   SetCountText();
        }
    }

    //void SetCountText()
    //{
    //    countText.text = "Count: " + count.ToString();
    //    if (count >= 6)
    //    {
    //      //  winTextObject.SetActive(true);
    //      //  Destroy(GameObject.FindGameObjectWithTag("Enemy"));
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the current object
            // Destroy(gameObject);
            // Update the winText to display "You Lose!"
            //winTextObject.gameObject.SetActive(true);
            //winTextObject.GetComponent<TextMeshProUGUI>().text = "OUCH!";
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            transform.position = startPosition;
            transform.rotation = Quaternion.identity; //resets rotation which aligns it with keyboard/expectation.
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //winTextObject.gameObject.SetActive(false);
        }
    }

}
