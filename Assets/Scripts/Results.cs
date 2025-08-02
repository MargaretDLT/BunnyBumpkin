using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

//Margaret De La Torre made for results pop up
public class Results : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject panel;  // assign your results panel GameObject here

    private void Start()
    {
        panel.SetActive(false);  // Hide panel initially
    }

    public void ShowResults(int stars, int carrots)
    {
        scoreText.text = $"Stars: {stars}\nCarrots: {carrots}";
        panel.SetActive(true);  // Show panel
    }
}
