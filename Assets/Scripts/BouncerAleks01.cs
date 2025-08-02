using UnityEngine;

public class BouncerAleks01 : MonoBehaviour
{
    public float bounceForce = 30f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        Debug.Log(gameObject.tag);
        // check if the tag of the object is "Player"
        if (other.gameObject.tag == "Player")
        {
            Rigidbody rb = other.GetComponent<Rigidbody>(); // get rigidbody of player
            rb.AddForce(transform.TransformDirection(Vector3.up) * bounceForce, ForceMode.Impulse);
        }
    }
}
