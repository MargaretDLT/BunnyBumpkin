using System.Collections.Generic; //needed for gravity area management system
using System.Linq; //helps with sorting gravity areas
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] //we need this or the system won't work so it is a hard requirement
public class GravityBody : MonoBehaviour
{
    //public variables
    [Tooltip("If checked, the body will automatically rotate to stand upright on gravity surfaces.")]
    public bool UseAutoOrientation = true;
    public float GravityForce = 800;

    //private variables
    private Rigidbody rigidBody;
    private List<GravityArea> gravityAreas = new List<GravityArea>();
    private Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody>();
        player = transform.GetComponent<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (!rigidBody.useGravity)
        {
            // Apply gravity force in the calculated direction
            rigidBody.AddForce(GravityDirection * (GravityForce * Time.deltaTime), ForceMode.Acceleration);

            if (UseAutoOrientation && GravityDirection != Vector3.zero)
            {

                // Calculate rotation needed to align object's up with gravity direction
                Quaternion upRotation = Quaternion.FromToRotation(transform.up, -GravityDirection);

                // Smoothly rotate toward the target orientation
                Quaternion newRotation = Quaternion.Slerp(rigidBody.rotation, upRotation * rigidBody.rotation, Time.fixedDeltaTime * 3f);
                rigidBody.MoveRotation(newRotation);
            }
            
        }
    }

    public Vector3 GravityDirection
    {
        get
        {
            if(gravityAreas.Count == 0)
            {
                return Vector3.zero;
            }
            gravityAreas.Sort((area1, area2) => area1.Priority.CompareTo(area2.Priority));
            return gravityAreas.Last().GetGravityDirection(this).normalized;
        }
    }

    public void AddGravityArea(GravityArea gravityArea)
    {
        // Enable gravity movement when entering first gravity area
        gravityAreas.Add(gravityArea);
        if (gravityAreas.Count == 1)
        {
            if (player != null)
            {
                player.DisableNormalGravity();
            }
        }
    }

    public void RemoveGravityArea(GravityArea gravityArea)
    {
        // Disables gravity movement when leaving last gravity area
        gravityAreas.Remove(gravityArea);
        if (gravityAreas.Count == 0)
        {
            if (player != null)
            {
                player.EnableNormalGravity();
            }
        }
    }
}
