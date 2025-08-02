using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public bool lockYAxis = true;
    private Transform playerTransform;
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        GetComponent<Collider>().isTrigger = true;
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerTransform != null)
            {
                if (lockYAxis)
                {

                    Vector3 targetPosition = playerTransform.position;
                    targetPosition.y = transform.position.y;
                    transform.LookAt(targetPosition);
                }
                else
                {
                    transform.LookAt(playerTransform);
                }
            }
        }
    }
}
