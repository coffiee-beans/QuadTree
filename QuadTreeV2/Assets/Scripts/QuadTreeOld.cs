using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTreeOld : MonoBehaviour
{
    public float width;
    public float length;
    //public QuadTreePlane[] plane;
    public int depth;

    public bool divide;
    public GameObject quad;

    private void Start()
    {

    }

    void Update()
    {
        if (divide)
        {
            if (this.transform.childCount == 0)
            {
                //plane = new QuadTreePlane[4];
                // plane[0] = new QuadTreePlane(length / 2, width / 2, new Vector2(this.transform.position.x - length / 4, this.transform.position.z + width / 4));
                GameObject p1 = Instantiate(quad);
                p1.GetComponent<QuadTree>().divide = false;
                p1.gameObject.name = "NW";
                p1.transform.parent = this.transform;
                p1.transform.position = new Vector3(this.transform.position.x - length / 4, 0, this.transform.position.z + width / 4);
                p1.GetComponent<QuadTree>().width = width / 2;
                p1.GetComponent<QuadTree>().length = length / 2;



                //plane[1] = new QuadTreePlane(length / 2, width / 2, new Vector2(this.transform.position.x + length / 4, this.transform.position.z + width / 4));
                GameObject p2 = Instantiate(quad);
                p2.GetComponent<QuadTree>().divide = false;
                p2.gameObject.name = "NE";
                p2.transform.parent = this.transform;
                p2.transform.position = new Vector3(this.transform.position.x + length / 4, 0, this.transform.position.z + width / 4);
                p2.GetComponent<QuadTree>().width = width / 2;
                p2.GetComponent<QuadTree>().length = length / 2;


                //plane[2] = new QuadTreePlane(length / 2, width / 2, new Vector2(this.transform.position.x - length / 4, this.transform.position.z - width / 4));

                GameObject p3 = Instantiate(quad);
                p3.GetComponent<QuadTree>().divide = false;
                p3.gameObject.name = "SW";
                p3.transform.parent = this.transform;
                p3.transform.position = new Vector3(this.transform.position.x - length / 4, 0, this.transform.position.z - width / 4);
                p3.GetComponent<QuadTree>().width = width / 2;
                p3.GetComponent<QuadTree>().length = length / 2;


                //plane[3] = new QuadTreePlane(length / 2, width / 2, new Vector2(this.transform.position.x + length / 4, this.transform.position.z - width / 4));
                GameObject p4 = Instantiate(quad);
                p4.GetComponent<QuadTree>().divide = false;
                p4.gameObject.name = "SE";
                p4.transform.parent = this.transform;
                p4.transform.position = new Vector3(this.transform.position.x + length / 4, 0, this.transform.position.z- width / 4);
                p4.GetComponent<QuadTree>().width = width / 2;
                p4.GetComponent<QuadTree>().length = length / 2;
            }
        }
        else
        {
            if (transform.childCount > 0)
            {
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    Destroy(this.transform.GetChild(0).gameObject);
                }
            }

            //plane = new QuadTreePlane[1];
            //plane[0] = new QuadTreePlane(length, width, new Vector3(this.transform.position.x, this.transform.position.z));
        }
    }
}

[System.Serializable]
public class QuadTreePlane
{
    public float width;
    public float length;
    public Vector2 position;

    public QuadTreePlane(float length, float width, Vector2 position)
    {
        this.width = width;
        this.length = length;
        this.position = position;
    }
}

