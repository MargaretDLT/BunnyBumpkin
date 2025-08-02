using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
	public float gizmoRadius = 0.5f; // Radius of the gizmo sphere
	public Color gizmoColor = Color.green; // Color of the gizmo

	private void OnDrawGizmos()
	{
		// Set the color for the gizmo
		Gizmos.color = gizmoColor;

		// Draw a sphere at the spawn point's position
		Gizmos.DrawSphere(transform.position, gizmoRadius);
	}
}
