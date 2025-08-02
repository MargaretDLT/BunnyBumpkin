using UnityEngine;
using System.Collections;

public class ForwardBackwardMovement : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float backwardSpeed = 3f;
    public float forwardDuration = 2f;
    public float backwardDuration = 2f;
    public float stopDuration = 0.5f;

    private Rigidbody rb;
    private bool isMovementCycleActive = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            enabled = false;
            return;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isMovementCycleActive)
            {
                isMovementCycleActive = true;
                StartCoroutine(ExecuteMovementCycle());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {

    }

    IEnumerator ExecuteMovementCycle()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPositionForward = startPosition + transform.forward * (forwardSpeed * forwardDuration);
        float elapsedTime = 0f;

        while (elapsedTime < forwardDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPositionForward, elapsedTime / forwardDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPositionForward;

        yield return new WaitForSeconds(stopDuration);

        startPosition = transform.position;
        Vector3 targetPositionBackward = startPosition - transform.forward * (backwardSpeed * backwardDuration);
        elapsedTime = 0f;

        while (elapsedTime < backwardDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPositionBackward, elapsedTime / backwardDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPositionBackward;

        yield return new WaitForSeconds(stopDuration);

        isMovementCycleActive = false;
    }
}