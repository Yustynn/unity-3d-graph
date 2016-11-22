using UnityEngine;
using System.Collections;

// auto add MeshFilter, MeshRenderer if not present on object
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class ProceduralGrid : MonoBehaviour {


    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    // grid config
    public float cellSize = 1;
    public Vector3 gridOffset = new Vector3(0,0,0);
    public int gridSize = 10;

	// Use this for initialization
	void Awake () {
        mesh = GetComponent<MeshFilter>().mesh;
	}

    void Start()
    {
        MakeContiguousProceduralGrid();
        UpdateMesh();
    }
	
    void MakeContiguousProceduralGrid()
    {
        vertices = new Vector3[(gridSize + 1) * (gridSize + 1)];
        triangles = new int[gridSize * gridSize * 6];

        // tracker ints
        int v = 0, t = 0; // v == y of first nested for-loop, but this is clearer!

        float vertexOffset = cellSize * 0.5f; // turns out multiplication is cheaper than division

        // <= because we need the edge vertices at the end of the grid (so gridsize + 1)
        for (int x = 0; x <= gridSize; x++)
        {
            for (int y = 0; y <= gridSize; y++)
            {
                // vertices[v] = new Vector3(x * cellSize - vertexOffset, y * cellSize - vertexOffset, x / (y+1)); // rand z fn for now
                vertices[v] = new Vector3(x * cellSize - vertexOffset, (y*y + x*x) / (x * y + 1), y * cellSize - vertexOffset); // rand z fn for now
                v++;
            }
        }

        v = 0; // #reset!

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                triangles[t]   = v;
                triangles[t+1] = v + 1;
                triangles[t+2] = v + (gridSize + 1);
                triangles[t+3] = v + (gridSize + 1);
                triangles[t+4] = v + 1;
                triangles[t+5] = (v + 1) + (gridSize + 1) ;

                v++;
                t += 6;
            }

            v++; // idrg why yet
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
