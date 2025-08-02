using UnityEngine;

public class GravityCameras : MonoBehaviour
{
    private GameObject cameraObject;
    public Vector3 cameraPosition = new Vector3(0, 5, -5);
    public Vector3 cameraRotation = new Vector3(45, 0, 0);
    public Vector3 originalCameraPosition = new Vector3(0, 5, -5);
    public Vector3 originalCameraRotation = new Vector3(45, 0, 0);
    private FollowCamera followCameraScript;
    public bool inverseZoneTrigger = false;
    public bool normalZoneTrigger = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Make sure this collider is a trigger
        GetComponent<Collider>().isTrigger = true;
        cameraObject = Camera.main.gameObject;

        // Store original camera position/rotation
        if (cameraObject != null)
        {
            
            followCameraScript = cameraObject.transform.GetComponent<FollowCamera>();
            followCameraScript.offset = originalCameraPosition;
            cameraObject.transform.localRotation = Quaternion.Euler(originalCameraRotation);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (cameraObject != null)
            {
                // Set camera to new position and rotation
                followCameraScript.offset = cameraPosition;
                cameraObject.transform.localRotation = Quaternion.Euler(cameraRotation);
                


            }
            if (inverseZoneTrigger)
            {
                player.inverseGravityMovements = true;
                //player.UseExperimentalGravity = true;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (cameraObject != null)
            {
                // Set camera to new position and rotation
                followCameraScript.offset = originalCameraPosition;
                cameraObject.transform.localRotation = Quaternion.Euler(originalCameraRotation);
                player.inverseGravityMovements = false;
                //player.UseExperimentalGravity = false;

            }
        }
    }
}
