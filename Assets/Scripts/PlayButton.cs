using UnityEngine;
using UnityEngine.SceneManagement;

// Play Button script - to launch the scene in PlayerPrefs
public class PlayButton : MonoBehaviour
{
	public AudioClip ButtonSFX;
	public int indexSFX;
	public AudioClip gameMusic;

	// Start is called before the first frame update
	void Start()
	{
		indexSFX = SoundBoard.Instance.AddSoundEffect(ButtonSFX);
		// start the music for the menu
		SoundBoard.Instance.PlayMusic(gameMusic);
	}

	// Update is called once per frame
	void Update()
	{
		// detects PC or Mac keyboard JUMP or RETURN (Enter) button - also checks the controller START/PAUSE button
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Joystick1Button7))
		{
			SoundBoard.Instance.PlaySFX(indexSFX);
			SceneManager.LoadScene("Hub");
		}
	}

	// detects tap or mouse click events on the object
	void OnMouseDown()
	{
		SoundBoard.Instance.PlaySFX(indexSFX);
        SceneManager.LoadScene("Hub");
    }
}
