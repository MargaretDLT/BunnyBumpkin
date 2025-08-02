using System.Net.NetworkInformation;
using UnityEngine;

public class BossFlame : MonoBehaviour
{
    public float bounceForce;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Boss")
        {
            Rigidbody rb = other.GetComponent<Rigidbody>(); // get rigidbody of player
            rb.AddForce(transform.TransformDirection(Vector3.up) * bounceForce, ForceMode.Impulse);
        }
    }
}
