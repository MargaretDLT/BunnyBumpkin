using UnityEngine;

public class StarCounterChecker : MonoBehaviour
{
    public GameObject objectToDestroy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null && player.starCounter > 0)
            {
                if (objectToDestroy != null)
                {
                    Destroy(objectToDestroy);
                }

                Destroy(gameObject);
            }
        }
    }


}
