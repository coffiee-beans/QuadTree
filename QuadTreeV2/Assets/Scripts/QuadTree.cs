using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTree : MonoBehaviour
{
    public float width;
    public float length;
    public int depth;
    public int maxDepth;

    public bool divide;
    public GameObject quad;
    [HideInInspector] public bool maxDepthreched = false;

    public float threshold;
    public Material mat;

    public int count = 2;
    Vector2 refrence = Vector2.zero;
    Mesh mesh;
    Vector3[] verts;
    int[] tris;

    void Start()
    {
        refrence.x = -length / 2;
        refrence.y = -width / 2;

        verts = new Vector3[(count + 1) * (count + 1)];
        mesh = new Mesh();

        CreateShape();
        UpdateMesh();

        if (GetComponent<MeshRenderer>() == null)
        {
            this.gameObject.AddComponent<MeshRenderer>().sharedMaterial = mat;

        }
         
        if (GetComponent<MeshFilter>() == null)
        {
            this.gameObject.AddComponent<MeshFilter>().sharedMesh = mesh;        }
        else
        {
            GetComponent<MeshFilter>().sharedMesh = mesh;
        }
    }

    void Update()
    {
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, this.transform.position) < threshold)
        {
            if (!maxDepthreched)
            {
                divide = true;
                if (this.transform.childCount == 0)
                {
                    QuadTreeinit("NW", new Vector2(this.transform.position.x - length / 4, this.transform.position.z + width / 4));
                    QuadTreeinit("NE", new Vector2(this.transform.position.x + length / 4, this.transform.position.z + width / 4));
                    QuadTreeinit("SW", new Vector2(this.transform.position.x - length / 4, this.transform.position.z - width / 4));
                    QuadTreeinit("SE", new Vector2(this.transform.position.x + length / 4, this.transform.position.z - width / 4));
                }
            }
        }
        else
        {
            divide = false;
            if (transform.childCount > 0)
            {
                GetComponent<MeshRenderer>().enabled = true;
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    Destroy(this.transform.GetChild(0).gameObject);
                }
            }            
        }

        CreateShape();
        UpdateMesh();

        if(divide)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            this.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    void QuadTreeinit(string name, Vector2 position)
    {
        GameObject p = Instantiate(quad);

        p.GetComponent<QuadTree>().threshold = threshold / 2;
        p.GetComponent<QuadTree>().depth = depth - 1;
        p.gameObject.name = name + " : " + (depth - 1).ToString();

        if (depth > 0)
        {
            p.GetComponent<QuadTree>().divide = true;
            p.GetComponent<QuadTree>().maxDepthreched = false;
        }
        else
        {
            p.GetComponent<QuadTree>().divide = false;
            p.GetComponent<QuadTree>().maxDepthreched = true;
        }

        p.GetComponent<QuadTree>().width = width / 2;
        p.GetComponent<QuadTree>().length = length / 2;

        p.transform.parent = this.transform;
        p.transform.position = new Vector3(position.x, 0, position.y);
    }


    void CreateShape()
    {

        refrence.x = -length / 2;
        refrence.y = -width / 2;

        for (int x = 0, i = 0; x <= count; x++)
        {
            for (int z = 0; z <= count; z++)
            {
                verts[i] = new Vector3((x * (length/count)) + refrence.x, 0, (z * (width/count)) + refrence.y);
                i++;
            }
        }

        tris = new int[count * count * 6];
        int vert = 0;
        int tri = 0;

        for (int z = 0; z < count; z++)
        {
            for (int x = 0; x < count; x++)
            {
                tris[tri + 5] = vert + 0;
                tris[tri + 4] = vert + count + 1;
                tris[tri + 3] = vert + 1;
                tris[tri + 2] = vert + 1;
                tris[tri + 1] = vert + count + 1;
                tris[tri + 0] = vert + count + 2;

                vert++;
                tri += 6;
            }
            vert++;
        }

    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
    }
}

