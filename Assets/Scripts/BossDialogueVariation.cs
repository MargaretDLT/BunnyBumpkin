using UnityEngine;
using System.Collections;

public class BossDialogueVariation : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] dialogueLines;
    public Font dialogueFont;
    public int fontSize = 40;
    private GUIStyle dialogueStyle;
    private Rect dialogueRect;
    private string currentText = "";
    private bool isDialogueActive = false;
    public float minTimeBetweenLines = 1f;
    public float maxTimeBetweenLines = 3f;
    public float displayDuration = 3f;


    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        dialogueStyle = new GUIStyle();
        dialogueStyle.alignment = TextAnchor.MiddleCenter;
        dialogueStyle.fontSize = fontSize;
        dialogueStyle.font = dialogueFont;
        dialogueStyle.normal.textColor = Color.white;
        dialogueStyle.wordWrap = true;
        dialogueRect = new Rect(0, Screen.height * 0.75f, Screen.width, Screen.height * 0.25f);

    }
    private void OnGUI()
    {
        // Only draw the dialogue box if there is actually text to display.
        if (!string.IsNullOrEmpty(currentText))
        {
            GUI.Box(dialogueRect, "");
            GUI.Label(dialogueRect, currentText, dialogueStyle);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && dialogueLines.Length > 0 && !isDialogueActive)
        {
            StartCoroutine(RunDialogueSequence());
        }
    }

    private IEnumerator RunDialogueSequence()
    {
        isDialogueActive = true;

        foreach (string line in dialogueLines)
        {
            currentText = line;
            yield return new WaitForSeconds(displayDuration);
            currentText = "";
            float randomWaitTime = Random.Range(minTimeBetweenLines, maxTimeBetweenLines);
            yield return new WaitForSeconds(randomWaitTime);
        }

        currentText = "";
    }
}
