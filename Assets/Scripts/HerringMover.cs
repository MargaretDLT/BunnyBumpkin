using UnityEngine;

public class HerringMover : MonoBehaviour
{
    public float moveMultiplier = 1.0f;
    public float moveDistance = 5.0f;

    private bool plusDirection = true;
    private float xStart;


    void Start()
    {
        xStart = transform.position.x;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * moveMultiplier * Time.deltaTime);

        if ((plusDirection) & (transform.position.x > xStart + moveDistance))
        {
            plusDirection = false;
            transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
        }
        else if ((!plusDirection) & (transform.position.x < xStart - moveDistance))
        {
            plusDirection = true;
            transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
        }
    }

}