using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRender : MonoBehaviour {

    public int chunksToRender;
    public int chunkSize;
    
    List<Mesh> meshs;
    public List<GameObject> world;
    //HOW I HANDLE CHUNK GENERATION
    /*
     * First, generate the list of mesh's using RenderChunk(size) that are going to be used for the terrain
     * Next, Instantiate new GameObject with the terrains componentents. This gameobject must include:
     * Mesh Filter
     * Mesh Renderer
     * Mesh Collider
     * Rigidbody (isKinematic = false, freezePosition on (x,y,z))
     * 
     * 
     * After this we must connect the heights of the chunks to make sure that they are level. 
     * This means that the height at (x[0], z[a]) and (z[0], x[a]) should equal the adacent blocks height 
     */
    void Start () {
        world = SpawnChunks(chunksToRender);
	}
	
    public List<GameObject> SpawnChunks(int numOfChunks)
    {
        Debug.Log("ran");
        List<GameObject> chunks = new List<GameObject>();
        meshs = GenerateSpawnMesh(chunksToRender);
        for (int i = 0; i < meshs.Count; i++)
        {
            GameObject curChunk = new GameObject();
            curChunk.name = "Mesh #" + i;
            curChunk.AddComponent(typeof(MeshFilter));
            curChunk.GetComponent<MeshFilter>().mesh = meshs[i];
            curChunk.AddComponent(typeof(MeshRenderer));
            //here is where you should determine the material of the Renderer
            curChunk.AddComponent(typeof(MeshCollider));
            curChunk.GetComponent<MeshCollider>().sharedMesh = meshs[i];
            curChunk.GetComponent<MeshCollider>().convex = true;
            curChunk.AddComponent(typeof(Rigidbody));
            Rigidbody rb = curChunk.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            rb.useGravity = false;
            curChunk.GetComponent<Rigidbody>().Equals(rb);

            //find the positions at which this chunk will be placed
            Vector3 position = new Vector3(0 + chunkSize*i, 0, 0);
            Quaternion rotation = new Quaternion();
            curChunk.transform.position = position;
            curChunk.transform.rotation = rotation;
            chunks.Add(curChunk);
        }
        return chunks;
    }
    private List<Mesh> GenerateSpawnMesh(int len)
    {
        List<Mesh> sp = new List<Mesh>();
        for (int i = 0; i < len; i++)
        {
            Mesh curMesh = RenderChunk(chunkSize);
            sp.Add(curMesh);
        }
        return sp;
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
    private List<Mesh> RenderSpawnMesh()
    {
        
        List<Mesh> spawn = new List<Mesh>();
        int chunksToRender = 2;
        for (int i = 0; i < chunksToRender; i++)
        {
            Mesh chunk = RenderChunk(chunkSize);
            spawn.Add(chunk);
        }
        return null;
    }
}
