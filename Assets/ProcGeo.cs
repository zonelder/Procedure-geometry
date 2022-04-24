using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcGeo : MonoBehaviour
{
    // Start is called before the first frame update

     void Awake()
    {
        Mesh mesh = new Mesh();
        mesh.name = "Procedural Quad";
        Debug.Log("work");
        List<Vector3> points = new List<Vector3>()
        {
            new Vector3(-1,1),
            new Vector3(1,1),
            new Vector3(-1,-1),
            new Vector3(1,-1)
        };
        int[] triIndices= new int[] {
            1,0,2,
            3,1,2
        };
        List<Vector2> uvs = new List<Vector2>()
        {
            new Vector2(1,1),
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(0,0),
        };
        List<Vector3> normals = new List<Vector3>()
        {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward
        };
        mesh.SetVertices(points);
        mesh.SetNormals(normals);
        mesh.SetUVs(0,uvs);
        mesh.triangles = triIndices;
        

        //mesh.RecalculateNormals();
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}
