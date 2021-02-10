using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peice : MonoBehaviour
{
    // Start is called before the first frame update
    public int row;
    public int column;
    public Material mat;
    public GameObject Cube;
    public GameObject Sphere;
    void Start()
    {
        Sphere.GetComponent<Renderer>().material = mat;
    }

    // Update is called once per frame
    void Update()
    {
        Sphere.GetComponent<Renderer>().material = mat;
    }
}
