using UnityEngine;
// Copyright © 2025 Randy Angle
// Permission is granted to use this script in your student, private, or commercial game projects, provided this notice remains intact
// Commerical release means putting "Additional code by Rangle Angle" in the game credits.
public class CarrotPhysics : MonoBehaviour
{
    public GameObject shockwavePrefab;
    public AudioClip soundFX;
    public int IndexSFX;
    public Player playerObject;
    public float collectionDelay = 1.0f;
    private Collider itemCollider;
    void Start()
    {
        IndexSFX = SoundBoard.Instance.AddSoundEffect(soundFX);
        playerObject = GameObject.FindFirstObjectByType<Player>();
        itemCollider = GetComponent<Collider>();
        if (itemCollider != null)
        {
            // Immediately disable the collider so it can't be picked up.
            itemCollider.enabled = false;
        }
        Invoke(nameof(EnableCollection), collectionDelay);

    }
    private void EnableCollection()
    {
        if (itemCollider != null)
        {
            itemCollider.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // play pickup sound
            SoundBoard.Instance.PlaySFX(IndexSFX);

            // add score
            playerObject.AddCarrot();

            GameObject particle = Instantiate(shockwavePrefab, transform.position, Quaternion.identity);
            Destroy(particle, 2f);
            Destroy(gameObject);
        }
    }
}



