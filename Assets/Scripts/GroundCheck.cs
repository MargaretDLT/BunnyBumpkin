using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    public float offsetDistance;
    public BoxCollider boxCollider;
    private Player playerScript;


    void Start()
    {
        if (player != null)
        {
            playerScript = player.GetComponent<Player>();
        }
        else
        {
            print("no Player found");
        }
        offset = transform.position - player.transform.position;
        offsetDistance = Vector3.Distance(transform.position, player.transform.position);
    }
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

    void OnTriggerEnter(Collider other)
    {
        //print("triggerEnter");
            updatePlayerGroundState(true);
        
        
        
    }

    void OnTriggerExit(Collider other)
    {
        //print("triggerExit");
            updatePlayerGroundState(false);

    }

    void updatePlayerGroundState (bool isGround)
    {
        //print(isGround);
        playerScript.SetGrounded(isGround);
    }

}
