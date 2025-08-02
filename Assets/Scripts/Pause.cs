using UnityEngine;
using UnityEngine.SceneManagement;

// Copyright © 2025 Randy Angle
// Permission is granted to use this script in your student, private, or commercial game projects, provided this notice remains intact
// Commerical release means putting "Additional code by Rangle Angle" in the game credits.

// Pause Mode
// attach to empty object or Main Camera
// handles pressing Backspace to display simple Pause Menu with instructions
// can add GUI.DrawTexture(...) to display images with text and buttons
public class Pause : MonoBehaviour
{
	public string HowToPlayText;
	bool bPaused;            //Boolean to check if the game is paused or not

	public GUIStyle PauseStyle;       // set the text style of the frame counter
	public GUIStyle HowToStyle;
	public GUIStyle ButtonStyle;       // set the text style of the frame counter
	public Texture2D ButtonImg;

	public AudioClip ButtonSFX;
	public int indexSFX;


	void Start()
	{
		HowToStyle.alignment = TextAnchor.UpperCenter;      // sets text flow left to right from top
		HowToStyle.fontSize = 40;                         // font size to 40 (for HD display
		HowToStyle.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);  // text color white White
		HowToStyle.wordWrap = true;

		ButtonStyle.alignment = TextAnchor.MiddleCenter;    // sets button text flow to middle centered
		ButtonStyle.fontSize = 40;                          // font size is 40 pixels
		ButtonStyle.fontStyle = FontStyle.Bold;
		ButtonStyle.normal.textColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);   // text color is solid white
		ButtonStyle.normal.background = (Texture2D)Resources.GetBuiltinResource(typeof(Texture2D), "GameSkin/button.png");  // use a default button
		ButtonStyle.normal.background = ButtonImg;  // use custom button

		// process string and display
		string temp;
		temp = HowToPlayText.Replace("\\n", "\n");
		temp = temp.Replace("\\t", "\t");
		HowToPlayText = temp;

		indexSFX = SoundBoard.Instance.AddSoundEffect(ButtonSFX);
	}

	// Update is called once per frame
	void Update()
	{
		// detects PC or Mac keyboard Backspace = Menu, Gamepad = start/pause, don't use ESCAPE on Unity Play WebGL
		if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Joystick1Button7) || ((Application.platform != RuntimePlatform.WebGLPlayer) && Input.GetKeyDown(KeyCode.Escape)))
		{
			if (bPaused)
			{
				UnPause();                              // stop pausing
				SoundBoard.Instance.PlaySFX(indexSFX);  // confirm audio
			}
			else
			{
				// no audio, because pausing
				DoPause();                              // begin pausing
			}
		}

		if (Input.GetButtonDown("Jump") && bPaused) // JUMP on pause menu
		{
			UnPause();
			SoundBoard.Instance.PlaySFX(indexSFX);
			SceneManager.LoadScene(0);  // quit to menu
		}
	}

	void DoPause()
	{
		//Set bPaused to true
		bPaused = true;
		//Set time.timescale to 0, this will cause animations and physics to stop updating
		Time.timeScale = 0;
		SoundBoard.Instance.DoPause();
	}

	void UnPause()
	{
		//Set bPaused to false
		bPaused = false;
		//Set time.timescale to 1, this will cause animations and physics to continue updating at regular speed
		Time.timeScale = 1;
		SoundBoard.Instance.UnPause();
	}

	void OnGUI()
	{
		if (bPaused)
		{
			//Calculate change aspects
			float resX = (float)(Screen.width) / 1920f;
			float resY = (float)(Screen.height) / 1080f;

			//Set matrix
			GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(resX, resY, 1));

			GUI.Box(new Rect(0, 0, 1920f, 1080f), "");                   // displays default GUI box without header

			GUI.Label(new Rect(10, 10, 1920f - 20f, 1080f * 0.75f), HowToPlayText, HowToStyle);

			if (GUI.Button(new Rect(Screen.width / 2 - 140, Screen.height * 0.75f + 10, 280, 80), "MENU", ButtonStyle))
			{
				UnPause();
				SoundBoard.Instance.PlaySFX(indexSFX);
				SceneManager.LoadScene(0);  // quit to menu
			}
			if (GUI.Button(new Rect(Screen.width / 2 - 140, Screen.height * 0.75f + 115, 280, 80), "RESUME", ButtonStyle))
			{
				UnPause();                  // resume game
				SoundBoard.Instance.PlaySFX(indexSFX);
			}
		}
	}
}
