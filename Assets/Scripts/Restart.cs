using UnityEngine;
using UnityEngine.SceneManagement;

// Copyright © 2025 Randy Angle
// Permission is granted to use this script in your student, private, or commercial game projects, provided this notice remains intact
// Commerical release means putting "Additional code by Rangle Angle" in the game credits.
public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // when the BACK key is pressed
        if(Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Joystick1Button7))
		{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   // reload the current scene
        }

#if !UNITY_WEBGL
        // when the Escape key is pressed
        if(Input.GetKeyDown(KeyCode.Escape))
		{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   // reload the current scene
        }
#endif

    }
}
