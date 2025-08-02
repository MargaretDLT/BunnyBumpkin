using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// Copyright � 2025 Randy Angle
// Permission is granted to use this script in your student, private, or commercial game projects, provided this notice remains intact
// Commerical release means putting "Additional code by Rangle Angle" in the game credits.

public class Key : MonoBehaviour
{
	public AudioClip music;
    //for results pop up
	bool bActive;
    public Score score;
    public Player player;
	public Results results;
    public GameObject ScoreTextObject; //for HUD info
    public int stars;
    public int carrots;
    private bool showResults = false;
    private GUIStyle boxStyle;
    private GUIStyle labelStyle;
    //for key collection particle
    public GameObject particlePrefab;
    public Texture2D starIcon;
    public Texture2D carrotIcon;
    public Font patrickHandFont;

    private void Start()
    {
        //Margaret De La Torre added for results pop up
        score = FindFirstObjectByType<Score>();
        player = FindFirstObjectByType<Player>();
        results = FindFirstObjectByType<Results>();
        ScoreTextObject = GameObject.Find("ScoreReadout");
        bActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player")) && bActive == true)
        {
            // play key sound
            SoundBoard.Instance.PlayMusic(music);
            bActive = false;

            Player p = other.GetComponent<Player>();
            p.bWinLevel = true;
            p.speedMultiplier = 0f;
            p.rb.linearVelocity = Vector3.zero;




            // for key collect confetti particle 
            GameObject fx = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            fx.SetActive(true);
            ParticleSystem ps = fx.GetComponent<ParticleSystem>();
            ps.Play();

            // shows results overlay
            showResults = true;
        }
    }

    private void Update()
    {
        stars = player.starCounter;
        carrots = player.carrotCounter;
        // any button or key will clear the alert
        if (showResults == true)
        {
            if (Input.anyKeyDown)
            {
				SceneManager.LoadScene("Hub"); // hub scene is index #1
				Destroy(gameObject);
            }
        }   
    }

    private void OnGUI()
    {
        boxStyle = new GUIStyle(GUI.skin.box)
        {
            fontSize = 48,
            alignment = TextAnchor.UpperCenter,
            font = patrickHandFont
        };
        labelStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 36,
            normal = { textColor = Color.white },
            font = patrickHandFont 
        };
        Debug.Log("OnGUI is running...");
        if (showResults == true)
        {
            GUIStyle titleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 60,
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
                normal = { textColor = Color.yellow },
                font = patrickHandFont
            };
            GUI.Label(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 250, 600, 80), "WINNER", titleStyle);
            GUI.BeginGroup(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 170, 600, 350));
            GUI.Box(new Rect(0, 0, 600, 450), "Results!", boxStyle);

            // Star icon and label
            if (starIcon != null)
            {
                GUI.DrawTexture(new Rect(30, 100, 50, 50), starIcon, ScaleMode.ScaleToFit);
            }
            GUI.Label(new Rect(90, 100, 480, 60), $"Stars: {stars}", labelStyle);
            // Carrot icon and label
            if (carrotIcon != null)
            {
                GUI.DrawTexture(new Rect(30, 180, 50, 50), carrotIcon, ScaleMode.ScaleToFit);
            }
            GUI.Label(new Rect(90, 180, 480, 60), $"Carrots: {carrots}", labelStyle);
            GUI.EndGroup();
        }
    }
}
