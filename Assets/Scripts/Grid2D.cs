using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2D {

    public static Vector3[] CreateVertices(int xSize, int ySize, float cellSize)
    {
        Vector3[] vertices = new Vector3[(xSize + 1) * (ySize + 1)];

        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x * cellSize, 0, y * cellSize);
            }
        }

        return vertices;
    }

    public static int[] CreateTris(int xSize, int ySize)
    {
        int[] tris = new int[xSize * ySize * 6];

        for (int ti = 0, y = 0, v = 0; y < ySize; ++y, ++v)
        {
            for (int x = 0; x < xSize; ++x, ti += 6, ++v)
            {
                tris[ti] = v;
                tris[ti + 1] = v + xSize + 1;
                tris[ti + 2] = v + 1;
                tris[ti + 3] = v + 1;
                tris[ti + 4] = v + xSize + 1;
                tris[ti + 5] = v + xSize + 2;
            }
        }
        return tris;
    }

    public static Vector2[] CreateUVs(int xSize, int ySize)
    {
        Vector2[] uv = new Vector2[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
            }
        }
        return uv;
    }
}
