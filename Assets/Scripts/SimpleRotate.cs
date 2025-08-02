using UnityEngine;

// Copyright © 2025 Randy Angle
// Permission is granted to use this script in your student, private, or commercial game projects, provided this notice remains intact
// Commerical release means putting "Additional code by Rangle Angle" in the game credits.
public class SimpleRotate : MonoBehaviour
{
    public Vector3 speed;

    void Update()
    {
        transform.Rotate(speed * Time.deltaTime);    
    }
}
