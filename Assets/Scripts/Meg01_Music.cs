using UnityEngine;

public class Meg01_Music : MonoBehaviour
{
    public AudioClip areaMusic;
    public AudioClip bossMusic;
    SoundBoard soundBoard;
    public Player player;
    private void OnTriggerEnter(Collider other)
    {
  
        if (other.gameObject.tag == "Player")
        {
            if (soundBoard != null)
            {
                soundBoard.DoPause();
            }
            SoundBoard.Instance.PlayMusic(areaMusic);
        }
        if (player.starCounter == 3)
        {
            if (soundBoard != null)
            {
                soundBoard.DoPause();
            }
            SoundBoard.Instance.PlayMusic(bossMusic);
        }
    }
}
