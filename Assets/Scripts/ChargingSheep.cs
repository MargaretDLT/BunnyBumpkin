using UnityEngine;
using System.Collections;

public class ChargingSheep : MonoBehaviour
{
    public string playerTag = "Player";
    private Transform playerTransform;

    public float detectionRange = 10f;
    public float chargeSpeed = 6f;

    private Rigidbody rb;
    private bool isCharging = false;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object with tag '" + playerTag + "' not found! Please ensure your player has the correct tag.");
            enabled = false;
            return;
        }

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on ChargingSheep! Please add one to this GameObject.");
            enabled = false;
        }
    }

    void Update()
    {
        if (playerTransform == null || rb == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (!isCharging)
            {
                StopAllCoroutines();
                StartCoroutine(ChargePlayerContinuously());
            }
        }
        else
        {
            if (isCharging)
            {
                StopAllCoroutines();
                isCharging = false;
               
                rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            }
        }
    }

    IEnumerator ChargePlayerContinuously()
    {
        isCharging = true;

        while (playerTransform != null && rb != null && Vector3.Distance(transform.position, playerTransform.position) <= detectionRange)
        {
            Vector3 lookDirection = playerTransform.position - transform.position;
            lookDirection.y = 0;
            if (lookDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }

            Vector3 moveDirection = (playerTransform.position - transform.position).normalized;
            moveDirection.y = 0;

            
            rb.linearVelocity = moveDirection * chargeSpeed + new Vector3(0, rb.linearVelocity.y, 0);

            yield return null;
        }

        if (rb != null)
        {
            
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
        isCharging = false;
    }
}