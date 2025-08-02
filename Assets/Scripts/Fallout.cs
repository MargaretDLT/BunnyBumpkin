using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

// Copyright © 2024 Randy Angle
// Permission is granted to use this script in your student, private, or commercial game projects, provided this notice remains intact
// Commerical release means putting "Additional code by Rangle Angle" in the game credits.

public class Fallout : MonoBehaviour
{
	public ParticleSystem DeathParticle;
	AudioSource MyAudioSource;
	CameraShake cameraShake;
	public AudioClip soundFX;
	public int IndexSFX;
    public Key keyOnGUI;
    //for results pop up
    public Score score;
    public Player player;
    public Results results;
    public GameObject ScoreTextObject; //for HUD
    public int stars;
    public int carrots;
    private bool showResults = false;
    private GUIStyle boxStyle;
    private GUIStyle labelStyle;
    public Texture2D starIcon;
    public Texture2D carrotIcon;
    public Font patrickHandFont;

    private void Start()
	{
		IndexSFX = SoundBoard.Instance.AddSoundEffect(soundFX);

		GameObject camera;
		camera = GameObject.Find("Main Camera");
		cameraShake = camera.GetComponent<CameraShake>();

        score = FindFirstObjectByType<Score>();
        player = FindFirstObjectByType<Player>();
        results = FindFirstObjectByType<Results>();
        ScoreTextObject = GameObject.Find("ScoreReadout");
    }

    private void Update()
    {
        stars = player.starCounter;
        carrots = player.carrotCounter;
        if (showResults && Input.anyKeyDown)
        {
            LoadHub();
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
            GUI.Label(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 250, 600, 80), "FALL OUT", titleStyle);
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // play deathbox sound
            SoundBoard.Instance.PlaySFX(IndexSFX);

            if (DeathParticle != null)
            {
                DeathParticle.Play();
            }
            // shows results overlay
            showResults = true;

            //shake camera
            StartCoroutine(cameraShake.Shake(0.15f, 0.4f));
        }
    }
    void LoadHub()
    {
        SceneManager.LoadScene("Hub");  // Load your hub scene (index 1)
    }
    void Reload()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
