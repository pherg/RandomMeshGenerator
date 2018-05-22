using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Render {

    public static void DrawVerts(Mesh mesh, Vector3[] verts, int[] tris, Vector2[] uvs)
    {
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }
}
