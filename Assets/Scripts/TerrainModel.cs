using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TerrainModel {
    public bool drawGizmos = true;
    public MeshFilter meshFilter;
    public Vector3 jumbleAmount = new Vector3(0.5f, 0.5f, 0.5f);
    public float cellSize = 1;
    
    public int GridSizeX = 5;
    public int GridSizeY = 5;

    public Vector3[] vertices { get; private set; }
    public int[] tris { get; private set; }
    public Vector2[] uvs { get; private set; }

    public void CalculateModel()
    {
        vertices = Grid2D.CreateVertices(GridSizeX, GridSizeY, cellSize);    
        tris = Grid2D.CreateTris(GridSizeX, GridSizeY);
        uvs = Grid2D.CreateUVs(GridSizeX, GridSizeY);
    }

    public static void JumbleVertz(Vector3[] verts, Vector3 jumbleAmount)
    {
        for (int i = 0; i < verts.Length; ++i)
        {
            verts[i] = new Vector3(verts[i].x + Random.Range(-jumbleAmount.x, jumbleAmount.x),
                                    verts[i].y + Random.Range(-jumbleAmount.y, jumbleAmount.y),
                                    verts[i].z + Random.Range(-jumbleAmount.z, jumbleAmount.z));
        }
    }
}
