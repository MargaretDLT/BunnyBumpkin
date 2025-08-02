using UnityEngine;

public class DeathShield : MonoBehaviour
{

    public GameObject shield; // Houses the red death shield

    // ********* Enemies that will be destroyed when the shield triggers ********************
    public GameObject TargetOne; // Ram # 1 
    public GameObject TargetTwo; // Ram # 2

    // ******************* Allies **************************************
    public GameObject ColobusBefore;
    public GameObject ColobusAfter;
    public GameObject SquidBefore;
    public GameObject SquidAfter;
    public GameObject CatBefore;
    public GameObject CatAfter;

    // ************* Animator components for characters ******************
    private Animator colobusAnimator;
    private Animator catAnimator;
    private Animator squidAnimator;
    private Animator colobusBeforeAnimator;

    private bool hasTriggered; // checks whether shield has been triggered or not

    // Plays the shield's audio source 
    public AudioClip audio;
    AudioSource MyAudioSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MyAudioSource = GetComponent<AudioSource>();

        // Shield is inactive and hasn't been triggered 
        shield.SetActive(false);
        hasTriggered = false;

        

        // Hides character models that will display after rams are destroyed 

        SquidAfter.SetActive(false);
        ColobusAfter.SetActive(false);
        CatAfter.SetActive(false);

        // Character animations 
        colobusBeforeAnimator =ColobusBefore.GetComponent<Animator>();

        colobusAnimator = ColobusAfter.GetComponent<Animator>();
        catAnimator = CatAfter.GetComponent<Animator>();
        squidAnimator = SquidAfter.GetComponent<Animator>();

        // Default animations with Before models 

        colobusBeforeAnimator.speed = 0.5f;
        colobusBeforeAnimator.Play("Idle");




    }

    // Update is called once per frame
    void Update()
    {
        

        
    }


    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Triggered the Death Shield");

        if (other.CompareTag("Player") && hasTriggered == false)
        {
            hasTriggered = true;
            // if audio clip is designated, play it
            if (MyAudioSource != null)
            {
                MyAudioSource.Play();
            }


            Debug.Log("Player entered the death shield w/ Trigger");
            shield.SetActive(true); // activate the shield 

            // Destroys all the rams and sheep 
            TargetOne.SetActive(false); 
            TargetTwo.SetActive(false);

            // Swaps out character models needed for after rams are destroyed and plays animation
            ColobusBefore.SetActive(false);
            SquidBefore.SetActive(false);
            CatBefore.SetActive(false);


            SquidAfter.SetActive(true);
            ColobusAfter.SetActive(true);
            CatAfter.SetActive(true);

            catAnimator.Play("05_Jump_Cat_Copy");
            squidAnimator.speed = 0.75f;
            squidAnimator.Play("Bounce");
            colobusAnimator.speed = 0.5f;
            colobusAnimator.Play("Spin");



            shield.SetActive(false);
            Destroy(gameObject, 2f);




        }

    }
}
