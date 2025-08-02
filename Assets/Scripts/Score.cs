using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

// Copyright © 2025 Randy Angle
// Permission is granted to use this script in your student, private, or commercial game projects, provided this notice remains intact
// Commerical release means putting "Additional code by Rangle Angle" in the game credits.

public class Score : MonoBehaviour
{
    TextMesh ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        ScoreText = GetComponent<TextMesh>();
        ScoreText.text = "Stars: 0\nCarrots: 0";
    }

    // ScoreUpdate is called only when stars or carrots changes
    public void ScoreUpdate(int stars, int carrots)
    {
        {
            ScoreText.text = "Stars: " + stars.ToString() + "\nCarrots: " + carrots.ToString();
        }
    }
}
