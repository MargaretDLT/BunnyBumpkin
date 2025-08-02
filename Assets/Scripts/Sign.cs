using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public AudioClip ButtonSFX;     // audio for button
    public int indexSFX;
    public string SignText; // Displays the character's dialogue
    private bool touchedSign = false; // Checks whether player is in contact with the character
    public Texture icon; // Display's a character's icon

    [HideInInspector]
    public GUIStyle HUDstyle;       // set the text style of the frame counter
    [HideInInspector]
    public string HUDtext;          // display text on the HUD
    [HideInInspector]
    public Rect HUDrect;            // area for HUD display

    private GUILayout layout;
    public Font m_Font;


    // This works closely in relation with GUIStyle. GUIContent defines what to render and GUIStyle defines how to render it.
    GUIContent content;

    AudioSource MyAudioSource; //  Audio that plays when player comes into contact with character

    // Start is called before the first frame update
    void Start()
    {
        HUDstyle.imagePosition = ImagePosition.ImageLeft; // Positions the character portrait to the left of the text
        HUDstyle.alignment = TextAnchor.MiddleCenter;      // sets text flow left to right from top
        HUDstyle.fontSize = 40;                         // font size to 40 (for HD display
        HUDstyle.font = m_Font;
        HUDstyle.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);  // text color white White
        HUDstyle.normal.background = null;
        HUDstyle.wordWrap = true;
        HUDstyle.padding = new RectOffset(20, 20, 0, 0); // Padding Left, Right, Top, Bottom


        // sets HUD display to bottom quarter of screen
        HUDrect = new Rect(0, Screen.height * 0.75f, Screen.width, Screen.height * 0.25f);

        //Fetch the AudioSource from the GameObject
        MyAudioSource = GetComponent<AudioSource>();

        indexSFX = SoundBoard.Instance.AddSoundEffect(ButtonSFX);
    }

    private void Update()
    {
   
    }

    // Display HUD values
    private void OnGUI()
    {
        // GUI font skin set by Pause.cs script

        // if text is not empty, display it
        if (HUDtext != "")
        {
         if (touchedSign)
            {

               
                GUI.Box(HUDrect, ""); // Displays clear box behind text and image

                GUILayout.BeginArea(HUDrect); // Begin drawing GUI that is the size of HUDrect
                GUILayout.BeginHorizontal(); // Begin positioning the icons and text horizontally

                // GUI.Label(HUDrect, HUDtext, HUDstyle);  // shows text in box with defined style << Old method 
                // GUI.Label(HUDrect, content, HUDstyle); // Displays HUD display, Character image + text, and sets style << Old method

                // Draw the icon and text if the character has an icon
                if (icon != null) 
                {
                    GUILayout.Label(icon, GUILayout.Width(180), GUILayout.Height(180)); // Manually sets size of image
                    GUILayout.Space(20); // space between image and text
                    // If the below is true, the enclosed UI elements can expand to fill the available horizontal width.
                    GUILayout.Label(HUDtext, HUDstyle, GUILayout.ExpandWidth(true));
                }
                else // Otherwise just print the text
                {
                    GUILayout.Label(HUDtext, HUDstyle, GUILayout.ExpandWidth(true));

                }


                GUILayout.EndHorizontal();
                GUILayout.EndArea(); // End of GUI area 
            } 
        } 
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered a trigger");
        if (other.gameObject.tag == "Player")
        {
            // if audio clip is designated, play it
            if (MyAudioSource != null)
            {
                MyAudioSource.Play();
            }
            touchedSign = true;

            // process string and display
            string temp;
            temp = SignText.Replace("\\n", "\n");
            temp = temp.Replace("\\t", "\t");
            HUDtext = temp;
        }
    }

    

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit a trigger");
        if (other.gameObject.tag == "Player")
        {
            // stop designated audio clip
            if (MyAudioSource != null)
            {
                MyAudioSource.Stop();
            }

            // clear trigger text because we left encounter
            HUDtext = "";
        }
    }

    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Entered a collision");
        Debug.Log("Collided with " + other.gameObject.name);
        if (other.gameObject.name == "Player")
        {
            // if audio clip is designated, play it
            if (MyAudioSource != null)
            {
                MyAudioSource.Play();
            }
            touchedSign = true;

            // process string and display
            string temp;
            temp = SignText.Replace("\\n", "\n");
            temp = temp.Replace("\\t", "\t");
            HUDtext = temp;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        Debug.Log("Exit a collision");
        if (other.gameObject.tag == "Player")
        {
            // stop designated audio clip
            if (MyAudioSource != null)
            {
                MyAudioSource.Stop();
            }

            // clear trigger text because we left encounter
            HUDtext = "";
        }
    }
    
}