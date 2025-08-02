using UnityEngine;

public class ToTheMoon : MonoBehaviour
{
    AudioSource MyAudioSource;

    // ******************* Allies **************************************

    public GameObject taipanLab;
    public GameObject colobusCC;

    private Animator colobusAnimator;
    private Animator taipanAnimator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MyAudioSource = GetComponent<AudioSource>();

        colobusAnimator = colobusCC.GetComponent<Animator>();
        taipanAnimator = taipanLab.GetComponent<Animator>();
        taipanAnimator.speed = 0.5f;
        taipanAnimator.Play("Clicked");
        colobusAnimator.Play("Idle");

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
