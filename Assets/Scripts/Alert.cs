using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

// UI Pop-up object
// Create an empty object that will get and show alerts...
// GameObject.Find("Alert").GetComponent<Alert>().SetAlertText("Hello, World!");
public class Alert : MonoBehaviour
{
	public AudioClip ButtonSFX;     // audio for button
	public int indexSFX;

	[HideInInspector]
	public GUIStyle HUDstyle;       // set the text style of the frame counter
	public string HUDtext;          // display text on the HUD
	[HideInInspector]
	public Rect HUDrect;            // area for HUD display
    public Font m_Font;

    AudioSource MyAudioSource;

	// Start is called before the first frame update
	void Start()
	{
		
		HUDstyle.alignment = TextAnchor.MiddleCenter;      // sets text flow left to right from top
		HUDstyle.fontSize = 60;                         // font size to 40 (for HD display
        HUDstyle.font = m_Font;
        HUDstyle.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);  // text color white White
		HUDstyle.normal.background = null;
		HUDstyle.wordWrap = true;

		// sets HUD display to bottom quarter of screen
		HUDrect = new Rect(0, Screen.height * 0.75f, Screen.width, Screen.height * 0.25f);

		//Fetch the AudioSource from the GameObject
		MyAudioSource = GetComponent<AudioSource>();

		indexSFX = SoundBoard.Instance.AddSoundEffect(ButtonSFX);
	}

	private void Update()
	{
		// any button or key will clear the alert
		if (Input.anyKeyDown)
		{
			// if text is not empty, play SFX and empty string
			if (HUDtext != "")
			{
				SoundBoard.Instance.PlaySFX(indexSFX);
				HUDtext = "";
			}
		}
	}

	public void SetAlertText(string AlertText)
	{
		HUDtext = AlertText;
	}

    // Display HUD values
    private void OnGUI()
	{
		// GUI font skin set by Pause.cs script

		// if text is not empty, display it
		if (HUDtext != "")
		{
			GUI.Box(HUDrect, "");                   // displays default GUI box without header
			GUI.Label(HUDrect, HUDtext, HUDstyle);  // shows text in box with defined style
		}
	}
}
