using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPlane : MonoBehaviour {

    public bool drawGizmos = true;

    public TerrainModel _terrainModel;

    Vector3[] _verticies;
    int[] _tris;
    Vector2[] _uvs;
    int GridSizeX;
    int GridSizeY;
    float cellSize;
    Vector3 jumbleAmount;
    MeshFilter _meshFilter;

    void Awake()
    {
        InitializeModel();

        int resolution = 256;
        Texture2D texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, true);
        texture.name = "Procedural Texture";
        GetComponent<MeshRenderer>().material.mainTexture = texture;
        Render.FillTexture(resolution, texture);

        TerrainModel.JumbleVertz(_verticies, jumbleAmount);

        Render.DrawVerts(_meshFilter.mesh, _verticies, _tris, _uvs);

        StartCoroutine(DoThings());
    }

    void InitializeModel()
    {
        _terrainModel.CalculateModel();
        ReadSettings(_terrainModel);
    }

    private void ReadSettings(TerrainModel tModel)
    {
        _verticies = tModel.vertices;
        _tris = tModel.tris;
        _uvs = tModel.uvs;

        GridSizeX = tModel.GridSizeX;
        GridSizeY = tModel.GridSizeY;
        cellSize = tModel.cellSize;
        jumbleAmount = tModel.jumbleAmount;
        _meshFilter = tModel.meshFilter;
    }

    IEnumerator DoThings()
    {
        while (true)
        {
            Vector3[] newVerts = Grid2D.CreateVertices(GridSizeX, GridSizeY, cellSize);
            float time = 0;
            float lerpTimeLength = 1f;
            TerrainModel.JumbleVertz(newVerts, _terrainModel.jumbleAmount);
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
            yield return new WaitForSeconds(0.0f);
        }
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
                for (int i = 0; i < _tris.Length - 1; i+=3)
                {
                    Gizmos.DrawLine(_verticies[_tris[i]], _verticies[_tris[i+1]]);
                    Gizmos.DrawLine(_verticies[_tris[i+1]], _verticies[_tris[i+2]]);
                    Gizmos.DrawLine(_verticies[_tris[i+2]], _verticies[_tris[i]]);
                }
            }
        }
    }
}

