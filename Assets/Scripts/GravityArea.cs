using UnityEngine;

//what would games be without colliders????? HARD REQUIREMENT! ty for not removing, Dustin.
[RequireComponent(typeof(Collider))] 
public abstract class GravityArea : MonoBehaviour
{
    //Editable in the Editor but unaccessible to everything else
    [SerializeField] private int priority;
    public int Priority => priority; //other classes should be able to read this variable

    private GameObject cameraObject;
    private Vector3 originalCameraObjectPosition;
    private Quaternion originalCameraObjectRotation;
    private FollowCamera followCameraScriptRef;

     

    void Start()
    {
        cameraObject = Camera.main.gameObject;

        transform.GetComponent<Collider>().isTrigger = true; //just in case designers are using, it will turn it on if they forget.
        originalCameraObjectPosition = cameraObject.transform.localPosition;
        originalCameraObjectRotation = cameraObject.transform.localRotation;
        followCameraScriptRef = cameraObject.transform.GetComponent<FollowCamera>();
        originalCameraObjectPosition = followCameraScriptRef.offset;
        originalCameraObjectRotation = cameraObject.transform.rotation;

    }

    public abstract Vector3 GetGravityDirection(GravityBody gravityBody);

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out GravityBody gravityBody))
        {
            gravityBody.AddGravityArea(this);
            if(other.TryGetComponent(out Player player))
            {
                player.bIsGrounded = true;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out GravityBody gravityBody))
        {
            gravityBody.RemoveGravityArea(this);

            if (other.TryGetComponent(out Player player))
            {
                player.bIsGrounded = false;
                //followCameraScriptRef.offset = originalCameraObjectPosition;
                //cameraObject.transform.localRotation = originalCameraObjectRotation;
            }

        }
    }
}
