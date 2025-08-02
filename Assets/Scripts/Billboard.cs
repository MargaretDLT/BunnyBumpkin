using UnityEngine;
using System.Collections;

// place on game object to always keep it facing the camera
public class Billboard : MonoBehaviour 
{
	private void Start()
	{

	}

	void LateUpdate () 
	{
		transform.LookAt(Camera.main.transform.position, Vector3.up);
	}
}
