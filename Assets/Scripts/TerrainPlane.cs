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
    Vector2 cellSize;
    Vector3 jumbleAmount;
    MeshFilter _meshFilter;

    void Awake()
    {
        InitializeModel();

        //int resolution = 256;
        //Texture2D texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, true);
        //texture.name = "Procedural Texture";
        //GetComponent<MeshRenderer>().material.mainTexture = texture;
        //Render.FillTexture(resolution, texture);

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
        Vector3[] initialVerts = new Vector3[_verticies.Length];
        while (true)
        {

            Vector3[] newVerts = Grid2D.CreateVertices(GridSizeX, GridSizeY, cellSize.x, cellSize.y);
            float time = 0;
            float lerpTimeLength = 1f;
            TerrainModel.JumbleVertz(newVerts, _terrainModel.jumbleAmount);
            float t = 0;
            System.Array.Copy(_verticies, initialVerts, _verticies.Length);
            do
            {
                time += Time.deltaTime;
                t = time / lerpTimeLength;
                for (int i = 0; i < newVerts.Length; ++i)
                {
                    _verticies[i] = Vector3.Lerp(initialVerts[i], newVerts[i], t);
                }
                Render.DrawVerts(_meshFilter.mesh, _verticies, _tris, _uvs);
                yield return null;
            } while (time <= lerpTimeLength);
            Render.DrawVerts(_meshFilter.mesh, newVerts, _tris, _uvs);
            yield return new WaitForSeconds(2.0f);
            
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
                Gizmos.DrawSphere(_verticies[i] + transform.position, 0.1f);
            }
            if (_tris != null)
            {
                for (int i = 0; i < _tris.Length - 1; i+=3)
                {
                    Gizmos.DrawLine(_verticies[_tris[i]] + transform.position, _verticies[_tris[i+1]] + transform.position);
                    Gizmos.DrawLine(_verticies[_tris[i+1]] + transform.position, _verticies[_tris[i+2]] + transform.position);
                    Gizmos.DrawLine(_verticies[_tris[i+2]] + transform.position, _verticies[_tris[i]] + transform.position);
                }
            }
        }
    }
}

