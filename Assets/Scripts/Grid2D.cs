using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2D {

    int _x;
    int _y;

    public void SetSize(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public static Vector3[] CreateVertices(int x, int y, float cellSize)
    {
        Vector3[] verts = new Vector3[(x+1) * (y+1)];
        for(int i = 0; i < x; ++i)
        {
            for(int j = 0; j < y; ++j)
            {

            }
        }
        verts[0] = new Vector3(0, 0, 0);
        verts[1] = new Vector3(0, 0, 1);
        verts[2] = new Vector3(1, 0, 0);
        return verts;
    }

    public static int[] CreateTris()
    {
        int[] tris = new int[3];
        tris[0] = 0;
        tris[1] = 1;
        tris[2] = 2;
        return tris;
    }
}
