using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Bounce a a GameObject with a Collider, a Triggering Collider, 
 * and a Rigidbody around a space delimited by Colliders.
 * 
 * Expects Gravity of Rigidbody to be off, 
 * though turning it might have interesting effects...
 */
public class BounceBall : MonoBehaviour
{
    public float speed = 10;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameObject.tag)
        {
            case "BounceVertical":
                bounceVertical();
                break;
            case "BounceHorizontalX":
                bounceHorizontalX();
                break;
            case "BounceHorizontalZ":
                bounceHorizontalZ();
                break;
            case "BounceRandomHorizontal": // Enemy patrol
                bounceRandomHorizontal();
                break;
            case "BounceRandom":
                bounceRandom();
                break;
            case "NoBounce":
                stopBounceVelocity();
                break;
            default:
                //Debug.Log("Not a ball.");
                break; 
        }
    }

    /**
     * Note: Could just implement one function that bounces on a supplied axis,
     * and a boolean for random, and a float for speed. 
     * I decided this is easier to debug, and easier to understand.
     */

    /**
     * As implemented, objects are not very suckable, resisting the pull. 
     * So, once they are being sucked, we change their label in Vacuum.cs to NoBounce. 
     * Then this function is calle to set velocity to 0.
     * IMPORTANT: The ball is now and forever under control of the Vacuum algorithms/physics.
     */
    void stopBounceVelocity()
    {
        rb.linearVelocity = new Vector3(0, 0, 0);
        this.tag = "Untagged";
    }

    /**
     * Bounce vertically between two colliders.
     */
    void bounceVertical()
    {
        if (rb.linearVelocity.y <= 0) rb.linearVelocity = new Vector3(0, -speed, 0);
        if (rb.linearVelocity.y > 0) rb.linearVelocity = new Vector3(0, speed, 0);
    }
    
    /**
     * Bounce horizontally between two colliders on the X axis.
     */
    void bounceHorizontalX()
    {
        if (rb.linearVelocity.x <= 0) rb.linearVelocity = new Vector3(-speed, 0, 0);
        if (rb.linearVelocity.x > 0) rb.linearVelocity = new Vector3(speed, 0, 0);
    }

    /**
     * Bounce horizontally between two colliders on the Z axis.
     */
    void bounceHorizontalZ()
    {
        if (rb.linearVelocity.z <= 0) rb.linearVelocity = new Vector3(0, 0, -speed);
        if (rb.linearVelocity.z > 0) rb.linearVelocity = new Vector3(0, 0, speed);
    }

    /**
     * Basically, patrol and bounce off walls.
     * The randomized speed vector alters the direction.
     */
    void bounceRandomHorizontal()
    {
        if (rb.linearVelocity.z <= 0) rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, Random.Range(-speed, -speed/2));
        if (rb.linearVelocity.z > 0) rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, Random.Range(speed/2, speed));

        if (rb.linearVelocity.x <= 0) rb.linearVelocity = new Vector3(Random.Range(-speed, -speed / 2),0, rb.linearVelocity.z);
        if (rb.linearVelocity.x > 0) rb.linearVelocity = new Vector3(Random.Range(speed / 2, speed), 0, rb.linearVelocity.z);
    }


    /*
     * Bounce in random directions. 
     * The randomized speed vector alters the direction.
     */
    void bounceRandom()
    {
        if (rb.linearVelocity.z <= 0) rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, Random.Range(-speed, -speed / 2));
        if (rb.linearVelocity.z > 0) rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, Random.Range(speed / 2, speed));

        if (rb.linearVelocity.x <= 0) rb.linearVelocity = new Vector3(Random.Range(-speed, -speed / 2), rb.linearVelocity.y, rb.linearVelocity.z);
        if (rb.linearVelocity.x > 0) rb.linearVelocity = new Vector3(Random.Range(speed / 2, speed), rb.linearVelocity.y, rb.linearVelocity.z);

        if (rb.linearVelocity.y <= 0) rb.linearVelocity = new Vector3(rb.linearVelocity.x, Random.Range(-speed, -speed / 2), rb.linearVelocity.z);
        if (rb.linearVelocity.y > 0) rb.linearVelocity = new Vector3(rb.linearVelocity.x, Random.Range(speed / 2, speed), rb.linearVelocity.z);
    }
}
