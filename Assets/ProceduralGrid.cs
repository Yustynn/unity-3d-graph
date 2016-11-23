﻿using UnityEngine;
using System.Collections;

// auto add MeshFilter, MeshRenderer if not present on object
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class ProceduralGrid : MonoBehaviour {


    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    // grid config
    public int cellsPerUnit = 1;
    public Vector3 gridOffset = new Vector3(0,0,0);
    public int sizeX = 10;
    public int sizeY = 10;

	// Use this for initialization
	void Awake () {
        mesh = GetComponent<MeshFilter>().mesh; // aww yisss Awake > Start for variable value initialization
	}

    void Start()
    {
        MakeContiguousProceduralGrid();
        UpdateMesh();
    }
	
    void MakeContiguousProceduralGrid()
    {
        vertices = new Vector3[(sizeX + 1) * cellsPerUnit * (sizeY + 1) * cellsPerUnit];
        triangles = new int[sizeX * cellsPerUnit * sizeY * cellsPerUnit * 6];

        // tracker ints
        int v = 0, t = 0; // v == y of first nested for-loop, but this is clearer!
        
        // some calculations
        float cellSize = 1 / (float)cellsPerUnit;
        Vector3 vertexOffset = new Vector3((float)sizeX * 0.5f, 0, (float)sizeX * 0.5f); // turns out multiplication is cheaper than division
        int numCellsX = sizeX * cellsPerUnit;
        int numCellsY = sizeY * cellsPerUnit;

        // <= because we need the edge vertices at the end of the grid (so gridsize + 1)
        for (float x = 0; x <= sizeX; x += cellSize)
        {
            for (float y = 0; y <= sizeY; y += cellSize)
            {
                float numerator = y * y + x * x;
                float denominator = (x * y + 1);
                if (denominator == 0) denominator = 0.000001f; // we can't divide by 0! Just for sanity
                float z = numerator / denominator;

                vertices[v] = new Vector3(x, z, y) - vertexOffset; // rand z fn for now, and yes, x,z,y
                v++;
            }
        }

        v = 0; // #reset for more looping!

        for (int x = 0; x < numCellsX; x++)
        {
            for (int y = 0; y < numCellsY; y++)
            {
                triangles[t]   = v;
                triangles[t+1] = v + 1;
                triangles[t+2] = v + (numCellsX + 1);
                triangles[t+3] = v + (numCellsX + 1);
                triangles[t+4] = v + 1;
                triangles[t+5] = (v + 1) + (numCellsX + 1) ;

                v++;
                t += 6;
            }

            v++; // idrg why yet, but something to do with the +1 to sizeX, sizeY
        }
    }

	// Update is called once per frame
	void UpdateMesh () {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals(); // for ligthining to not be stupid
	}
}
