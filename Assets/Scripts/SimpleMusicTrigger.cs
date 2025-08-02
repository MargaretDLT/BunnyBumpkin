using UnityEngine;

public class SimpleMusicTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioClip musicToPlay;
    private bool hasBeenTriggered = false;
    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasBeenTriggered && other.CompareTag("Player") && musicToPlay != null)
        {
            hasBeenTriggered = true;
            SoundBoard.Instance.PlayMusic(musicToPlay);
        }
    }
}
