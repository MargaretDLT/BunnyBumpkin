using UnityEngine;

// Copyright © 2025 Randy Angle
// Permission is granted to use this script in your student, private, or commercial game projects, provided this notice remains intact
// Commerical release means putting "Additional code by Rangle Angle" in the game credits.
public class FollowCamera : MonoBehaviour
{
	public Material transparentMat;
	public Material cachedMat;
	GameObject player;

	public Vector3 offset;
	public float offsetDistance;

	public GameObject obstructionObj;

	void Start()
	{
		player = GameObject.FindWithTag("Player");
		offset = transform.position - player.transform.position;
		offsetDistance = Vector3.Distance(transform.position, player.transform.position);
		obstructionObj = null;
		transform.LookAt(player.transform);
	}

	void LateUpdate()
	{
		transform.position = player.transform.position + offset;
		ViewObstructed();
	}

	void ViewObstructed()
	{
		RaycastHit hit;

		//if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, offsetDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))

		if (Physics.Raycast(player.transform.position, transform.position - player.transform.position, out hit, offsetDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
		{
			if (hit.collider.gameObject.tag != "MainCamera")
			{
				// obstructed, override distance
				transform.position = Vector3.Lerp(transform.position,hit.point,0.25f);
			}
		}
	}
}
