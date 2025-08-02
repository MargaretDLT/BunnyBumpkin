using UnityEngine;

public class GravityDisable : MonoBehaviour
{
    private bool hasBeenTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenTriggered)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            GravityBody gravityBody = other.GetComponent<GravityBody>();

            if (gravityBody != null)
            {

                gravityBody.enabled = false;
                Rigidbody playerRb = other.GetComponent<Rigidbody>();
                if (playerRb != null)
                {
                    playerRb.useGravity = true;
                }

                hasBeenTriggered = true;
            }
        }
    }
}