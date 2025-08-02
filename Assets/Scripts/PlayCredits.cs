using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayCredits : MonoBehaviour
{

    public void LoadSceneByName()
    {
        SceneManager.LoadScene("Credits");
    }
}
