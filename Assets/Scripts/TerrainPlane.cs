﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPlane : MonoBehaviour {
    public bool drawGizmos = true;
    public MeshFilter _meshFilter;
    public Vector3 jumbleAmount = new Vector3(0.5f, 0.5f, 0.5f);
    public float cellSize = 1;
    Vector3[] _verticies;
    int[] _tris;
    Vector2[] _uvs;
    public int GridSizeX = 5;
    public int GridSizeY = 5;

    void Awake()
    {
        _verticies = TerrainPlane.CreateVertices(GridSizeX, GridSizeY, cellSize);
        JumbleVertz(_verticies, jumbleAmount);
        _tris = TerrainPlane.CreateTris(GridSizeX, GridSizeY);
        _uvs = TerrainPlane.CreateUVs(GridSizeX, GridSizeY);
        Render.DrawVerts(_meshFilter.mesh, _verticies, _tris, _uvs);

        StartCoroutine(DoThings());
    }

    IEnumerator DoThings()
    {
        while (true)
        {
            Vector3[] newVerts = TerrainPlane.CreateVertices(GridSizeX, GridSizeY, cellSize);
            float time = 0;
            float lerpTimeLength = 1f;
            JumbleVertz(newVerts, jumbleAmount);
            float t = 0;
            while (time <= lerpTimeLength)
            {
                t = Time.deltaTime / lerpTimeLength;
                
                for (int i = 0; i < newVerts.Length; ++i)
                {
                    _verticies[i] = Vector3.Lerp(_verticies[i], newVerts[i], t);
                }
                yield return null;
                
                time += Time.deltaTime;
                Render.DrawVerts(_meshFilter.mesh, _verticies, _tris, _uvs);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void JumbleVertz(Vector3[] verts, Vector3 jumbleAmount)
    {
        for(int i = 0; i < verts.Length; ++i)
        {
            verts[i] = new Vector3( verts[i].x + Random.Range(-jumbleAmount.x, jumbleAmount.x), 
                                    verts[i].y + Random.Range(-jumbleAmount.y, jumbleAmount.y),
                                    verts[i].z + Random.Range(-jumbleAmount.z, jumbleAmount.z));
        }
    }

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
        
        for (int ti = 0, y = 0, v = 0; y < ySize; ++y, ++v){
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
        Vector2[] uv = new Vector2[(xSize+1)*(ySize+1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                uv[i] = new Vector2(x / xSize, y / ySize);
            }
        }
        return uv;
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmos)
        {
            return;
        }
        Gizmos.color = Color.black;
        if(_verticies != null)
        {
            for (int i = 0; i < _verticies.Length; i++)
            {
                Gizmos.DrawSphere(_verticies[i], 0.1f);
            }
            if (_tris != null)
            {
                for (int i = 0; i < _tris.Length - 1; i++)
                {
                    Gizmos.DrawLine(_verticies[_tris[i]], _verticies[_tris[i + 1]]);
                }
            }
        }        
    }
}

