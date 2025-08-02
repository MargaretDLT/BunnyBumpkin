using UnityEngine;

public class CameraOffsetTrigger : MonoBehaviour
{
    public Vector3 newCameraOffset; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FollowCameraZoom followCameraZoom = Camera.main.GetComponent<FollowCameraZoom>();

            if (followCameraZoom != null)
            {
                
                followCameraZoom.SetCustomOffset(newCameraOffset);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FollowCameraZoom followCameraZoom = Camera.main.GetComponent<FollowCameraZoom>();

            if (followCameraZoom != null)
            {
               
                followCameraZoom.ResetToDefaultOffset();
            }
        }
    }
}