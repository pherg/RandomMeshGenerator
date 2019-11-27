using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2D {
    public static Vector3[] CreateVertices(int xSize, int ySize, float cellSizeX, float cellSizeY, float z = 0.0f)
    {
        Vector3[] vertices = new Vector3[(xSize + 1) * (ySize + 1)];

        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x * cellSizeX, 0, y * cellSizeY);
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

    public static Vector2[] CreateUVs(int xSize, int ySize, float uvStart, float uvEnd)
    {
        Vector2[] uv = new Vector2[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                uv[i] = new Vector2(((float)x / xSize) * (uvEnd - uvStart) + uvStart, ((float)y / ySize) * (uvEnd - uvStart) + uvStart);
            }
        }
        return uv;
    }

    public static Vector2[] CreateUVs(GridNode[,] nodes)
    {
        int xSize = nodes.GetLength(0);
        int ySize = nodes.GetLength(1);

        Vector2[] uv = new Vector2[(xSize) * (ySize)];
        for (int i = 0, y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++, i++)
            {
                uv[i] = new Vector2(nodes[x,y].uvStart + nodes[x,y].uvEnd * (x % 2), nodes[x,y].uvStart + nodes[x,y].uvEnd * (y % 2));
                //uv[i] = new Vector2(((float)x / xSize) * (uvEnd - uvStart) + uvStart, ((float)y / ySize) * (uvEnd - uvStart) + uvStart);
            }
            // Account for  the final UV element without expanding past array size;
            //uv[i] = new Vector2(nodes[xSize-1, y].uvStart + nodes[xSize-1, y].uvEnd * (xSize % 2), nodes[xSize-1, y].uvStart + nodes[xSize-1, y].uvEnd * (y % 2));
        }
        return uv;
    }

    public struct GridNode
    {
        public float uvStart;
        public float uvEnd;

        GridNode(float uvStart, float uvEnd)
        {
            this.uvStart = uvStart;
            this.uvEnd = uvEnd;
        }
    }
}
