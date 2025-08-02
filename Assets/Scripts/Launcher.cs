using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRate = 3f;
    public Transform playerTarget;
    private float fireTimer;
    public Transform spawnPoint;
    public BoxCollider deathBox;
    private bool isDestroyed = false;
    public float minLookDistance = 2f;
    public float minFireDistance = 15f;
    private float playerDistance = 0f;
    public AudioClip destructionSound;
    public GameObject destructionVFX;
    private int sfxIndex;
    public GameObject rewardPrefab;
    public float rewardLaunchForce = 10f;

    void Start()
    {
        // Register the sound effect with our SoundBoard manager so it can be played.
        if (destructionSound != null)
        {
            sfxIndex = SoundBoard.Instance.AddSoundEffect(destructionSound);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDestroyed)
        {
            return;
        }
        if (projectilePrefab == null)
        {
            return;
        }

        if (playerTarget != null)
        {
            playerDistance = Vector3.Distance(transform.position, playerTarget.position);
            
            if (playerDistance < minFireDistance)
            {
                if (playerDistance > minLookDistance)
                {
                    transform.LookAt(playerTarget);
                }
                fireTimer -= Time.deltaTime;
                if (fireTimer <= 0f)
                {
                    SpawnProjectile();

                    fireTimer = fireRate;
                }
            }
        }else
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f)
            {
                SpawnProjectile();

                fireTimer = fireRate;
            }

        }
        

    }

    void SpawnProjectile()
    {
        Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDestroyed) return;
        

        if (other.CompareTag("Player"))
        {
            if (destructionSound != null)
            {
                SoundBoard.Instance.PlaySFX(sfxIndex);
            }

            if (destructionVFX != null)
            {
                GameObject vfx = Instantiate(destructionVFX, transform.position, Quaternion.identity);
                Destroy(vfx, 2f);
            }

            if (rewardPrefab != null)
            {
                GameObject rewardObject = Instantiate(rewardPrefab, transform.position, Quaternion.identity);
                Rigidbody rewardRb = rewardObject.GetComponent<Rigidbody>();
                if (rewardRb != null)
                {
                    Vector3 launchDirection = Vector3.up;
                    launchDirection.x = Random.Range(-0.5f, 0.5f);
                    launchDirection.z = Random.Range(-0.5f, 0.5f);
                    rewardRb.AddForce(launchDirection.normalized * rewardLaunchForce, ForceMode.Impulse);
                }
            }
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
