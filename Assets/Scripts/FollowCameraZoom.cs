using UnityEngine;

public class FollowCameraZoom : MonoBehaviour
{
    public Material transparentMat;
    public Material cachedMat;
    GameObject player;

    public Vector3 defaultOffset;
    private Vector3 currentOffset;
    public float offsetDistance;

    public GameObject obstructionObj;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        defaultOffset = transform.position - player.transform.position;
        currentOffset = defaultOffset;
        offsetDistance = Vector3.Distance(transform.position, player.transform.position);
        obstructionObj = null;
        transform.LookAt(player.transform);
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + currentOffset;
        ViewObstructed();
    }

    void ViewObstructed()
    {
        RaycastHit hit;

        if (Physics.Raycast(player.transform.position, transform.position - player.transform.position, out hit, offsetDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.gameObject.tag != "MainCamera")
            {
                transform.position = Vector3.Lerp(transform.position, hit.point, 0.25f);
            }
        }
    }

    public void SetCustomOffset(Vector3 newOffset)
    {
        currentOffset = newOffset;
    }

    public void ResetToDefaultOffset()
    {
        currentOffset = defaultOffset;
    }
}