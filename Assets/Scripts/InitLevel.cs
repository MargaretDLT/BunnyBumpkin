using UnityEngine;

public class InitLevel : MonoBehaviour
{
	public AudioClip gameMusic;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		SoundBoard.Instance.PlayMusic(gameMusic);
	}
}
