using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TerrainModel {
    public bool drawGizmos = true;
    public MeshFilter meshFilter;
    public Vector3 jumbleAmount = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector2 cellSize = Vector2.one;

    public int GridSizeX = 5;
    public int GridSizeY = 5;

    public Vector3[] vertices { get; private set; }
    public int[] tris { get; private set; }
    public Vector2[] uvs { get; private set; }

    public void CalculateModel()
    {
        vertices = Grid2D.CreateVertices(GridSizeX, GridSizeY, cellSize.x, cellSize.y);    
        tris = Grid2D.CreateTris(GridSizeX, GridSizeY);
        uvs = Grid2D.CreateUVs(GridSizeX, GridSizeY, 0.4f, 0.6f);
        //Grid2D.GridNode[,] gridNodes = new Grid2D.GridNode[GridSizeX+1, GridSizeY+1]; // +1 to account for final UV
        //for(int x=0; x<GridSizeX+1; ++x)
        //{
        //    for(int y=0; y<GridSizeY+1; ++y){
        //        gridNodes[x,y].uvStart = 0.4f;
        //        gridNodes[x,y].uvEnd = 0.5f;
        //    }
        //}
        //uvs = Grid2D.CreateUVs(gridNodes);
    }

    public static void JumbleVertz(Vector3[] verts, Vector3 jumbleAmount)
    {
        Vector3 totalJumble = Vector3.zero;
        for (int i = 0; i < verts.Length; ++i)
        {
            totalJumble += new Vector3(Random.Range(-jumbleAmount.x, jumbleAmount.x),
                                    Random.Range(-jumbleAmount.y, jumbleAmount.y),
                                    Random.Range(-jumbleAmount.z, jumbleAmount.z));
            verts[i] = verts[i] + totalJumble;
        }
    }
}
