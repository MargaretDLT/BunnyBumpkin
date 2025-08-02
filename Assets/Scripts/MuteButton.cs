using UnityEngine;

public class MuteButton : MonoBehaviour
{
    void start()
    {
        
    }
    void OnMouseDown()
    {
        SoundBoard.Instance.ToggleMute();
    }

}
