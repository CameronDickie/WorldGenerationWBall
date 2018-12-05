using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour {

    /*
     * Meshes are shapes which contain vertices
     * we can store these in an array, and we can fill these points in with triangles
     * In unity, these triangles are drawn clockwise (0, 1, 2)
     */
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    MeshCollider collider;

    public int xSize = 32;
    public int zSize = 32;
    public float hillHeight;
    
	// Use this for initialization
	void Start () {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();

        collider = GetComponent<MeshCollider>();
	}
    private void Update()
    {
        UpdateMesh();
    }
    

    private void CreateShape()
    {

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        //loop over each of the vertices, assigning them a position on the grid. We shall do this left --> right
        for (int z = 0, i = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.Sin(x);
                //float y = Mathf.PerlinNoise(x * .3f, z * .3f) * hillHeight;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                //creating a square

                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }
    /*
    IEnumerator CreateShape()
    {

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        //loop over each of the vertices, assigning them a position on the grid. We shall do this left --> right
        for (int z = 0, i = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                //creating a square

                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
                yield return new WaitForSeconds(.01f);
            }
            vert++;

        }
    }
    */
    private void OnDrawGizmos()
    {
        if (vertices == null) return;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        if(collider.sharedMesh == null || collider.sharedMesh != mesh)
        {
            collider.sharedMesh = mesh;
        }

        mesh.RecalculateNormals();
    }
}
