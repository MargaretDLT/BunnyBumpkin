using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Open_Door : MonoBehaviour
{
    public GameObject door;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(door);
        }
    }
}
