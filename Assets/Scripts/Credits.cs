using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public AudioClip EggSFX;
    public int indexSFX;

    // Start is called before the first frame update
    void Start()
    {
        indexSFX = SoundBoard.Instance.AddSoundEffect(EggSFX); 
    }

    // detects tap or mouse click events on the object
    void OnMouseDown()
    {
        SoundBoard.Instance.PlaySFX(indexSFX);
        SceneManager.LoadScene("Credits");
    }
    
}
