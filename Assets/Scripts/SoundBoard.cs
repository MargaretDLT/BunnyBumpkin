using System.Collections.Generic;
using UnityEngine;

// SoundBoard - plays sound effects and music
// Create empty object in first scene (typically MENU) - attach this script
// add 2 AudioSources to the object, then Drag&Drop into the audioSFX and audioMusic in the inspector
// will persist in all scenes as a "DontDestroyOnLoad" object
// set default music in inspector gameMusic - call SoundBoard.Instance.PlayMusic(...) with new audioclip to change music
// in any object that needs sound effects, register the audioclips you want with int indexSFX = SoundBoard.Instance.AddSourdEffect(audioclip);
// when you want to play the SFX use SoundBoard.Instance.PlaySFX(indexSFX);
public class SoundBoard : MonoBehaviour
{
    public List<AudioClip> SoundEffects = new List<AudioClip>();

    public AudioSource audioSFX;
    public AudioSource audioMusic;
    private bool bPaused;								//Boolean to check if the game is paused or not

    public static SoundBoard Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
            Destroy(this.gameObject);
		}
        else
		{
            DontDestroyOnLoad(gameObject);
            Instance = this;
		}
	}

    void OnApplicationQuit()
    {
        DestroyImmediate(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if( (audioSFX == null) || (audioMusic == null))
		{
            Debug.Log("Missing audio source components");
		}
        bPaused = false;    // unpaused
    }

    // Update is called once per frame
    void Update()
    {

    }

    // call this to play the sound effect
    public void PlaySFX(int index, float volume = 1.0f)
	{
        if (!bPaused && (index < SoundEffects.Count))
        {
            audioSFX.PlayOneShot(SoundEffects[index], volume);
        }
    }

    // call this to change & play the music
    public void PlayMusic(AudioClip music)
    {
        //Debug.Log("Play Music: " + music.name);
        audioMusic.loop = true;         // always loop music
        audioMusic.volume = 0.25f;      // quite music
        audioMusic.clip = music;        // which clip to play
        if (!bPaused)
        {
            audioMusic.Play();
        }
    }

    // call to register (add) sound effect, returns index number of audioclip in library
    public int AddSoundEffect(AudioClip newSFX)
    {
        if(!SoundEffects.Contains(newSFX))
		{
            SoundEffects.Add(newSFX);
            //Debug.Log("Added SFX " + newSFX.name);
		}
        return SoundEffects.IndexOf(newSFX);
    }

    // call to pause audio
    public void DoPause()
	{
        audioSFX.Pause();
        audioMusic.Pause();
        bPaused = true;
	}

    // call to unpause audio
    public void UnPause()
	{
        audioSFX.UnPause();
        audioMusic.UnPause();
        bPaused = false;
	}

    public void ToggleMute()
    {
        audioSFX.mute = !audioSFX.mute;
        audioMusic.mute = !audioMusic.mute;
    }
}
