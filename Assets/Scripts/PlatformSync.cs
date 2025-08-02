using System.IO;
using UnityEngine;

public class PlatformSync : MonoBehaviour
{
    Rigidbody rb;
    Rigidbody otherRB;
    float diffX;
    float lastX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        diffX = rb.position.x - lastX;
        lastX = rb.position.x;
    }

	private void OnTriggerStay(Collider other)
	{
        if (other.gameObject.tag == "Player")
        {
            otherRB = other.GetComponent<Rigidbody>();
            otherRB.MovePosition(new Vector3(otherRB.position.x - diffX, otherRB.position.y, otherRB.position.z));
        }
	}
}
