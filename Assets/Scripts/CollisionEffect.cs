using UnityEngine;

public class CollisionEffect : MonoBehaviour
{
    public AudioClip collisionSound;
    public GameObject collisionParticlesPrefab;

    private int collisionSoundIndex;

    void Awake()
    {
        if (collisionSound != null && SoundBoard.Instance != null)
        {
            collisionSoundIndex = SoundBoard.Instance.AddSoundEffect(collisionSound);
        }
        else
        {
            Debug.LogWarning("Collision sound or SoundBoard instance is missing for " + gameObject.name, this);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (SoundBoard.Instance != null && collisionSoundIndex != -1)
            {
                SoundBoard.Instance.PlaySFX(collisionSoundIndex);
            }

            if (collisionParticlesPrefab != null)
            {
                GameObject instantiatedParticles = Instantiate(collisionParticlesPrefab, transform.position, Quaternion.identity);
                ParticleSystem ps = instantiatedParticles.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    ps.Play();
                    Destroy(instantiatedParticles, ps.main.duration + ps.main.startLifetime.constantMax + 0.1f);
                }
                else
                {
                    Debug.LogWarning("The assigned 'collisionParticlesPrefab' does not contain a ParticleSystem component!", this);
                    Destroy(instantiatedParticles, 5f);
                }
            }
        }
    }
}