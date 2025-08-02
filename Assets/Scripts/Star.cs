using UnityEngine;

// Copyright © 2025 Randy Angle
// Permission is granted to use this script in your student, private, or commercial game projects, provided this notice remains intact
// Commerical release means putting "Additional code by Rangle Angle" in the game credits.

public class Star : MonoBehaviour
{
	public GameObject shockwavePrefab;
	public AudioClip soundFX;
	public int IndexSFX;
	public Player playerObject;

	private void Start()
	{
		IndexSFX = SoundBoard.Instance.AddSoundEffect(soundFX);
		playerObject = GameObject.FindFirstObjectByType<Player>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			// play pickup sound
			SoundBoard.Instance.PlaySFX(IndexSFX);

			// add score
			playerObject.AddStar();

			GameObject particle = Instantiate(shockwavePrefab, transform.position, Quaternion.identity);
			Destroy(particle, 2f);
			Destroy(gameObject);
		}
	}
}


