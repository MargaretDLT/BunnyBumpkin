using UnityEngine;

public class Bumper : MonoBehaviour
{
	public float bumperForce;
	private float bumperRadius;

	// Start is called before the first frame update
	private void Start()
	{
		bumperRadius = gameObject.transform.localScale.x;
		// Debug.Log("Radius = " + bumperRadius);
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
			// Debug.Log("Kaboom");
			Rigidbody rb = other.GetComponent<Rigidbody>(); // get rigidbody of player
			rb.AddExplosionForce(bumperForce, transform.position, bumperRadius, 0.0f, ForceMode.Impulse);
		}
	}
}
