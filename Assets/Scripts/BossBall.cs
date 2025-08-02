using UnityEngine;

public class BossBall : MonoBehaviour
{
	public float AlertDistance;
	public float moveSpeed;
	public float playerBumpForce = 15f;
	public float bossBumpForce = 5f;
	public float stunTimer = 1f;
	public AudioClip soundFX;
	public int IndexSFX;
	public float speedNormal;
	public GameObject keyPrefab;
	public Vector3 startingPos;

	// local
	GameObject TargetObject;				// reference to player or target
	Rigidbody enemyRB;						// enemy rigidbody
	bool bWaiting;                          // enemy is waiting
	bool bStunned;                          // State after bumping into player

	Vector3 lookDir;

	// Start is called before the first frame update
	void Start()
    {
		//TargetObject = GameObject.FindGameObjectWithTag("Player");
		TargetObject = GameObject.Find("Player");		// This line is here as long as the boss ball is also tagged as player
		enemyRB = GetComponent<Rigidbody>();
		IndexSFX = SoundBoard.Instance.AddSoundEffect(soundFX);
		bWaiting = true;
		startingPos = transform.position;


	}

    // Update is called once per frame
    void Update()
    {
		// find the distance to the Player object
		float targetDistance = Vector3.Distance(transform.position, TargetObject.transform.position);

		// stay at locaiton until Player moves into AlertDistancee
		if (targetDistance <= AlertDistance)
		{
			//Debug.Log("Boss altered");
			bWaiting = false;

			//Debug.Log("Boss is tracking TD " + targetDistance + " AD " + AlertDistance);
			// look at target X & Z, but enemy's Y
			Vector3 myLookVec = transform.position - TargetObject.transform.position;
			myLookVec.y = transform.position.y;
			myLookVec.Normalize();

			lookDir = myLookVec;		// For getting the direction of the bump

			//transform.LookAt(myLookVec);

			if (!bStunned)
			{
				enemyRB.AddForce(-myLookVec * moveSpeed, ForceMode.Force);
			}
			//enemyRB.linearVelocity = transform.TransformDirection(Vector3.forward * moveSpeed);
		}
		else
		{
			if (bWaiting == false)
			{
				if (targetDistance > AlertDistance)
				{
					Debug.Log("Boss Dead");
					SoundBoard.Instance.PlaySFX(IndexSFX);
					GameObject key = Instantiate(keyPrefab, startingPos, Quaternion.identity);
					Destroy(gameObject);
				}
			}
		}
	}

	private void OnTriggerEnter (Collider other)		// For bumping into the player
	{
		if (other.gameObject.tag == "Player")
		{
			AlertDistance += 5f;
			//Debug.Log("Player hit!");
			bStunned = true;
			Invoke("StunnedState", stunTimer);

			Rigidbody playerRb = other.gameObject.GetComponent<Rigidbody>();

			enemyRB.AddForce(lookDir * bossBumpForce, ForceMode.Impulse);			// Push boss ball back
			playerRb.AddExplosionForce(playerBumpForce, transform.position, 1.0f, 0.0f, ForceMode.Impulse);		// Push player back
		}
	}

	public void StunnedState()			// Cooldown for movement to give player a chance to strike back
	{
		Debug.Log("No longer stunned!");
		bStunned = false;
	}
		
}


