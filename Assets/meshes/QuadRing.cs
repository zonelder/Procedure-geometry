using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
public class QuadRing : MonoBehaviour
{
    [Range(0.01f,2)]
    [SerializeField] float innerRadius;
    [Range(0.01f, 2)]
    [SerializeField] float thickness;

    float RadiusOuter
    {
        get
        {
            return innerRadius + thickness;
        }
    }

    int VertexCount => angularSegments * 2;
    [Range(3,32)]
    [SerializeField] int angularSegments = 3;
    Mesh mesh;
    void OnDrawGizmosSelected()
    {
        Gizmosfs.DrawWireCircle(transform.position, transform.rotation, innerRadius, angularSegments);
        Gizmosfs.DrawWireCircle(transform.position, transform.rotation, RadiusOuter, angularSegments);
    }

    void Awake()
    {
        mesh = new Mesh();
        mesh.name = "QuadRing";
        GetComponent<MeshFilter>().sharedMesh = mesh;
        GenerateMesh();
    }
    void Update() => GenerateMesh();

    void GenerateMesh()
    {
        mesh.Clear();
        int vCount = VertexCount;
        List<Vector3> vertices =new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        for (int i=0;i<angularSegments;i++)
        {
            float t = i / (float)angularSegments;
            float angRad = t * Mathfs.TAU;
            //Vector3 zOffset = Vector3.forward*Mathf.Cos(angRad * 4);
            Vector2 dir = Mathfs.GetUnitVectorByAngle(angRad);


            vertices.Add((Vector3)(dir * RadiusOuter));
            vertices.Add((Vector3)(dir * innerRadius));
            normals.Add(Vector3.forward);
            normals.Add(Vector3.forward);

            uvs.Add(new Vector2(t,1));
            uvs.Add(new Vector2(t,0));
        }
        List<int> triangleIndices = new List<int>();
        for (int i = 0; i < angularSegments; i++)
        {
            int rootIndex = i*2;
            int indexInnerRoot = rootIndex + 1;
            int indexOuterNext = (rootIndex + 2)%vCount;
            int indexInnerNext = (rootIndex + 3) % vCount;

            triangleIndices.Add(rootIndex);
            triangleIndices.Add(indexOuterNext);
            triangleIndices.Add(indexInnerNext);


            triangleIndices.Add(rootIndex);
            triangleIndices.Add(indexInnerNext);
            triangleIndices.Add(indexInnerRoot);
        }
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangleIndices, 0);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);


    }
}
