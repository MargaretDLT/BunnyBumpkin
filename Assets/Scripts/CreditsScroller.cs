using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScroller : MonoBehaviour
{
    public float scrollSpeed = 100f;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        if (rectTransform == null)
        {
            Debug.LogError("RectTransform is missing on this GameObject.");
        }
    }

    void Update()
    {
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
            Debug.Log("Scrolling... Y=" + rectTransform.anchoredPosition.y);
        }
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("MainMenu"); // load the MENU scene at index 0
        }
        
    }
}
