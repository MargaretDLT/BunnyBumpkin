using UnityEngine;

public class LookInPlace : MonoBehaviour
{
    GameObject player;
    GameObject m_camera;
    bool hasFallen;

    // Scripts
    FollowCamera followCamera;

    void Start()
    {
        followCamera = Camera.main.GetComponent<FollowCamera>();
        player = GameObject.Find("Player");
        m_camera = Camera.main.gameObject;
    }

    void Update()
    {
        // If fallen out of level...
        if (hasFallen)
        {
			m_camera.transform.LookAt(player.transform.position);
        }
    }

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
        {
            hasFallen = true;
            followCamera.enabled = false;
        }
	}
}
