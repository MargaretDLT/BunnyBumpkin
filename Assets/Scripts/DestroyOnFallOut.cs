using UnityEngine;

public class DestroyOnFallOut : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FallOut")
        {
            Debug.Log("Collided with FallOut! Destroying: " + this.gameObject.name);
            Destroy(this.gameObject);
        }
    }
}
