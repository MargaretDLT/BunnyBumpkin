using Unity.VisualScripting;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
	public float speedNormal;
	public float speedTurbo;
	public float jumpNormalHeight;
	public float jumpSuperHeight;
	public float speedMultiplier;
	public float jumpNormalForce;
	public float jumpSuperForce;
	public float jumpForce;
	public float gravityScale = 1.0f;
	public Rigidbody rb;
	public bool bJumping;
	public bool bIsGrounded;
	public bool bRiding;
	public AudioClip soundFX;
	public int IndexSFX;
	public int starCounter;
	public int carrotCounter;
	public Score scoreObject;
	public float gameTimer;      // countup timer
	public bool bSecretZone;
	public bool bBossFight;
	public bool bWinLevel;
	public Transform bossTransform;
	public GameObject bossPrefab;
	public GameObject secretZone;
	//public GameObject bossArena;	// For those who used this variable to spawn your boss arena,
	// it should be instead spawned when you unlock the secret zone

	
	[System.NonSerialized]
	public bool UseExperimentalGravity = false;
	public Transform secretTransform;
	private GravityBody gravityBody;
    [System.NonSerialized]
    public bool inverseGravityMovements = false;
    public bool bSpawnSFXandVFXStar2 = false;
    public AudioClip star2SFX;
    public GameObject star2VFX;
    public bool bSpawnSFXandVFXStar3 = false;
    public AudioClip star3SFX;
    public GameObject star3VFX;
    private int star2SfxIndex;
    private int star3SfxIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
	{
		IndexSFX = SoundBoard.Instance.AddSoundEffect(soundFX);
        if (star2SFX != null) { star2SfxIndex = SoundBoard.Instance.AddSoundEffect(star2SFX); }
        if (star3SFX != null) { star3SfxIndex = SoundBoard.Instance.AddSoundEffect(star3SFX); }
        scoreObject = GameObject.FindFirstObjectByType<Score>();
		GameObject go = GameObject.Find("BossSpawn");
		if(go != null)
		{
			bossTransform = go.transform;
		}
		secretZone = GameObject.Find("SecretZone");

		rb = GetComponent<Rigidbody>();
		gravityBody = GetComponent<GravityBody>(); //experimental Dustin
		jumpNormalForce = Mathf.Sqrt(jumpNormalHeight * Physics.gravity.y * -2.0f) * rb.mass;
		jumpSuperForce = Mathf.Sqrt(jumpSuperHeight * Physics.gravity.y * -2.0f) * rb.mass;
		jumpForce = jumpNormalForce;
		speedMultiplier = speedNormal;
		
		bJumping = false;
		bIsGrounded = false;
		bRiding = false;
		starCounter = 0;
		carrotCounter = 0;
		gameTimer = 0.0f;
		bSecretZone = false;
		bBossFight = false;
		bWinLevel = false;

		//experimental Dustin Code
		if (UseExperimentalGravity)
		{
			DisableNormalGravity();
		}
		else
		{
            EnableNormalGravity();
        }
	}

	public void AddStar()
	{
		starCounter++;
		scoreObject.ScoreUpdate(starCounter, carrotCounter);
        
        if (starCounter == 2)
		{
            
            if (secretZone != null)
			{
				if (bSpawnSFXandVFXStar2)
				{

                    if (star2SFX != null) { SoundBoard.Instance.PlaySFX(star2SfxIndex); }
					if (star2VFX != null)
					{
						GameObject vfx = Instantiate(star2VFX, secretTransform.position, Quaternion.identity);
                        Destroy(vfx, 2f);
					}
				}
                // 2 stars, unlock Secret Zone
                bSecretZone = true;
				Debug.Log("Secret Zone opened!");
				
				Destroy(secretZone);
			}
		}

		if(starCounter == 3)
		{
			if (bossPrefab != null)
			{
                if (bSpawnSFXandVFXStar3)
                {
                    if (star3SFX != null) { SoundBoard.Instance.PlaySFX(star3SfxIndex); }
                    if (star3VFX != null && bossTransform != null)
                    {
                        GameObject vfx = Instantiate(star3VFX, bossTransform.position, Quaternion.identity);
                        Destroy(vfx, 2f);
                    }
                }
                // 3 stars, spawn boss fight
                bBossFight = true;
				Instantiate(bossPrefab, bossTransform.position, bossTransform.rotation);
				Debug.Log("Boss Spawned");
			}
		}


		if (starCounter > 0)
		{
			jumpForce = jumpSuperForce;	// unlock Super Jump
		}
	}

	public void AddCarrot()
	{
		carrotCounter++;
		scoreObject.ScoreUpdate(starCounter, carrotCounter);
	}

    // Update is called once per frame
    void Update()
	{

		// increment gameTimer
		gameTimer += Time.deltaTime;

		
		// check grounded and check jumping
		if (bIsGrounded)
		{
			//this code makes you jump when you press the space bar
			if (Input.GetButtonDown("Jump"))
			{
				if (!UseExperimentalGravity)
				{
					//this launches you into the air
					rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
				}
				else
				{
                    rb.AddForce(-gravityBody.GravityDirection * jumpForce, ForceMode.Impulse);
                }
				//this part makes bjumping true
				bJumping = true;
				
				if (bRiding)    // kludge to make platform jumping work
				{
					rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
				}
				// play jump sound
				SoundBoard.Instance.PlaySFX(IndexSFX);
			}
		}
		
		if (Input.GetButtonUp("Jump"))
		{
			bJumping = false;
		}

		if (!bIsGrounded && (rb.linearVelocity.y < 0))
		{
			
			bJumping = false;
		}
	
	}

	private void FixedUpdate()
	{
		// controller movement
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
		if (!UseExperimentalGravity)
		{
            rb.AddForce(movement * speedMultiplier * Time.fixedDeltaTime);
		}
		else
		{
			Vector3 cameraForward = Camera.main.transform.forward;
			Vector3 cameraRight = Camera.main.transform.right;

			cameraForward.y = 0;
			cameraRight.y = 0;
			cameraForward.Normalize();
			cameraRight.Normalize();
			if (inverseGravityMovements)
			{
				cameraForward = cameraForward * -1;
			}
			Vector3 movement2 = (cameraForward * moveVertical) + (cameraRight * moveHorizontal);

			rb.AddForce(movement2 * speedMultiplier * Time.fixedDeltaTime);
		}
		

		
		if (bJumping == false && UseExperimentalGravity == false)
		{
			
			// custom gravity
			Vector3 gravity = gravityScale * Physics.gravity;
			rb.AddForce(gravity, ForceMode.Acceleration);
		}
		
		//bIsGrounded = false;
		
	}

	public void SetGrounded(bool grounded)
	{
		bIsGrounded = grounded;
		//print("bIsGrounded = " + bIsGrounded);
	}

	private void OnCollisionStay(Collision collision)
	{
		bIsGrounded = true;
		if (collision.gameObject.tag == "Platform")
		{
			bRiding = true;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		bIsGrounded = false;
		if (collision.gameObject.tag == "Platform")
		{
			bRiding = false;
		}
	}

	public void EnableNormalGravity()
	{
		rb.useGravity = true;
        UseExperimentalGravity = false;
    }

	public void DisableNormalGravity()
	{
		rb.useGravity = false;
		UseExperimentalGravity = true;

    }

}

