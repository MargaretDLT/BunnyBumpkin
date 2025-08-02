using UnityEngine;

public class UnlockSecretArea : MonoBehaviour
{
    public GameObject unlock; // object that will be destroyed once player collides with this 
    public GameObject beforeSwap; // the squirrel is not happy this shield is blocking your path
    public GameObject afterSwap; // squirrel's dialogue + character that will show when you unlock the shield 
    public AudioClip audio;
    AudioSource MyAudioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        afterSwap.SetActive(false);
        MyAudioSource = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        // Once the second star is collected, the shield deactivates, then Squirrel's model and dialogue changes 
        if (unlock == null)
        {
            Destroy(gameObject);
            Destroy(beforeSwap);
            afterSwap.SetActive(true);

            // if audio clip is designated, play it
            if (MyAudioSource != null)
            {
                MyAudioSource.Play();
            }

        }



    }

    

   
    
}
