using UnityEngine;

// Copyright © 2024 Randy Angle
// Permission is granted to use this script in your student, private, or commercial game projects, provided this notice remains intact
// Commerical release means putting "Additional code by Rangle Angle" in the game credits.

// Updates the Framerate readout display once every 1-second
// attach to a Legacy TextMesh object for HUD
// if the lag is too much at 1-second, shorten to 0.5 or 0.25 second
public class Framerate : MonoBehaviour
{
    public float frameRate;      // holds the average frame rate
    TextMesh fpsText;

    // Start is called before the first frame update
    void Start()
    {
        frameRate = 0.0f;
        fpsText = GetComponent<TextMesh>();
        fpsText.text = "FPS: ---";
        InvokeRepeating("FPSUpdate", 1.0f, 1.0f);
    }

	private void Update()
	{
        // continously updates frame rate every frame
        frameRate = Time.frameCount / Time.time;
    }
	private void OnDestroy()
    {
        CancelInvoke();
    }

    // Update is called once per second
    void FPSUpdate()
    {
        fpsText.text = string.Format("FPS: {0:0}", frameRate) + '\n' + Screen.width + 'x' + Screen.height;
    }
}
