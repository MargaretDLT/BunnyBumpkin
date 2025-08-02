using UnityEngine;

public class Zipper : MonoBehaviour
{
	public float turboForce;

	// Start is called before the first frame update
	private void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter(Collider other)
	{
		// check if the tag of the object is "Player"
		if (other.gameObject.tag == "Player")
		{
			Rigidbody rb = other.GetComponent<Rigidbody>(); // get rigidbody of player
			rb.AddForce(transform.TransformDirection(Vector3.forward) * turboForce, ForceMode.Impulse);
		}
	}
}

