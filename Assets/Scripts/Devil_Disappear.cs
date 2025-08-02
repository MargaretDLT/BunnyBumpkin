using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Devil_Disappear : MonoBehaviour
{
    public GameObject devil;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(devil);
        }
    }
}
