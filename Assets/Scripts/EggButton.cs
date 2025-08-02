using UnityEngine;
using UnityEngine.SceneManagement;

// Copyright © 2025 Randy Angle
// Permission is granted to use this script in your student, private, or commercial game projects, provided this notice remains intact
// Commerical release means putting "Additional code by Rangle Angle" in the game credits.

// Easter Egg Button script - used to add 1000 to the Grind Currency in the PlayerPrefs
public class EggButton : MonoBehaviour
{
    public AudioClip EggSFX;
    public int indexSFX;

    // Start is called before the first frame update
    void Start()
    {
        indexSFX = SoundBoard.Instance.AddSoundEffect(EggSFX);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // detects tap or mouse click events on the object
	void OnMouseDown()
	{
        SoundBoard.Instance.PlaySFX(indexSFX);
		SceneManager.LoadScene("SecretMenu");
	}
}
