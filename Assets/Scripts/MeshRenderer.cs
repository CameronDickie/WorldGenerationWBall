using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRenderer : MonoBehaviour {

    public GameObject chunkPrefab;
    public int chunkSize;
    List<GameObject> chunks;
	void Start () {
        chunks = RenderSpawn();
	}
	
	void Update () {
		
	}

    private List<GameObject> RenderSpawn()
    {
        List<GameObject> s = new List<GameObject>();
        int chunksToRender = 2;
        for (int i = 0; i < chunksToRender; i++)
        {
            GameObject chunk = Instantiate(chunkPrefab);
            chunk.SetActive(true);
            /*
             * if i = 0, dont rotate
             * if i = 1, rotate pi/2
             * if i = 2, rotate pi
             * if i = 3, rotate 3*pi/2
             */
            s.Add(chunk);
        }
        return s;
    }
    private Mesh RenderChunk(int Size)
    {
        Mesh m = new Mesh();
        Vector3[] v = new Vector3[(Size + 1) * (Size + 1)]; //local vertices array
        //loop over each of the vertices, assigning them a position on the grid. We shall do this left --> right
        for (int z = 0, i = 0; z <= Size; z++)
        {
            for (int x = 0; x <= Size; x++)
            {
                //calculate height
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                v[i] = new Vector3(x, y, z);
                i++;
            }
        }
        int[] t = new int[Size * Size * 6]; //local triangles array
        int vert = 0;
        int tris = 0;
        for (int z = 0; z < Size; z++)
        {
            for (int x = 0; x < Size; x++)
            {
                //creating a square
                t[tris + 0] = vert + 0;
                t[tris + 1] = vert + Size + 1;
                t[tris + 2] = vert + 1;
                t[tris + 3] = vert + 1;
                t[tris + 4] = vert + Size + 1;
                t[tris + 5] = vert + Size + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
        //push these to the mesh
        m.vertices = v;
        m.triangles = t;
        return m;
    }
}
