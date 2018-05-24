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

    public static void FillTexture(int resolution, Texture2D texture)
    {
        float stepSize = 1f / resolution;
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                texture.SetPixel(x, y, new Color(x * stepSize, y * stepSize, 0f));
            }
        }
        texture.Apply();
    }
}
