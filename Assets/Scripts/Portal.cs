using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
	//public int PortalSceneIndex;			// which scene does this door load
	public string PortalName;	
	public GameObject shockwavePrefab;  // set to particle or visual effect
	public AudioClip MySFX;             // set to sound effect to play when goal reached
	int AudioIndex;                     // index value of my sound effect

	private void Start()
	{
		AudioIndex = SoundBoard.Instance.AddSoundEffect(MySFX);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;

			SoundBoard.Instance.PlaySFX(AudioIndex);

			if (shockwavePrefab != null)
			{
				Instantiate(shockwavePrefab, transform.position, Quaternion.identity);  // play particle or visual effect
			}

			SceneManager.LoadScene(PortalName); // loads the classroom
		}
	}
}
