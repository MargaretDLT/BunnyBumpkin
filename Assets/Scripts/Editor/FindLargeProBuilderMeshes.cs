using UnityEditor;
using UnityEngine.ProBuilder;
using UnityEngine;

public class FindLargeProBuilderMeshes : EditorWindow
{
    [MenuItem("Assets/Find PB Meshes")]
    public static void FindLargePBMeshes()
    {
        ProBuilderMesh[] meshFilters = FindObjectsByType<ProBuilderMesh>(FindObjectsSortMode.None);
        foreach (ProBuilderMesh meshFilter in meshFilters)
        {
            MeshFilter filter = meshFilter.GetComponent<MeshFilter>();
            Mesh mesh = filter.sharedMesh;

            if (mesh != null)
            {
                Bounds bounds = meshFilter.GetComponent<MeshRenderer>().bounds;
                if (bounds.size.magnitude > 500)
                {
                    Debug.Log($"<color=red>BIG!</color> Found very large Renderer with mesh name on GameObject '{mesh.name}': magnitude: {bounds.size.magnitude} ({filter.gameObject.name})");
                }
                if (mesh.name.Contains ("pb_Mesh-254054")) {
                    Debug.Log(filter.gameObject.name);
                }
            }
        }
    }
}