using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
[RequireComponent(typeof(MeshFilter))]
public class RoadSegment : MonoBehaviour
{
    public Transform[] controlPoints =new Transform[4];

    [SerializeField] Mesh2D shape2D;
    [Range(2,32)]
    [SerializeField] int edgeRingCount=8;
    [Range(0, 1)]
    [SerializeField] float tTest = 0;

    Vector3 GetPosition(int i) => controlPoints[i].position;
    Mesh mesh;
     void Awake()
    {

        mesh = new Mesh();
        mesh.name = "segment";
        GetComponent<MeshFilter>().sharedMesh = mesh;
        GenerateMesh();
    }
    void Update()
    {
        GenerateMesh();

       
    }
    void GenerateMesh()
    {
        mesh.Clear();
        //vertixes
        List<Vector3> verts = new List<Vector3>();

        List<Vector3> normals = new List<Vector3>();
        for (int ring = 0; ring < edgeRingCount; ring++)
        {
            float t = ring / (edgeRingCount - 1f);
            OrientedPoint op = GetBezierPoint(t);
            for (int i = 0; i < shape2D.vertices.Length; ++i)
            {
                verts.Add(op.LocalToWorld(shape2D.vertices[i].point));
                normals.Add(op.LocalToWorld(shape2D.vertices[i].normal));
            }
        }

        //triangles
        List<int> triindices = new List<int>();
        for(int ring=0;ring<edgeRingCount-1;++ring)
        {
            int rootIndex = ring * shape2D.vertices.Length;
            int rootindexNext = (ring + 1) * shape2D.vertices.Length;
            for(int line=0;line<shape2D.lineIndices.Length;line+=2)
            {
                int lineA = shape2D.lineIndices[line];
                int lineB = shape2D.lineIndices[line+1];

                int currentA = lineA + rootIndex;
                int currentB = rootIndex + lineB;
                int nextA = rootindexNext + lineA;
                int nextB = rootindexNext + lineB;
                triindices.Add(currentA);
                triindices.Add(nextA);
                triindices.Add(nextB);
                triindices.Add(currentA);
                triindices.Add(nextB);
                triindices.Add(currentB);

            }
        }
        mesh.SetVertices(verts);
        mesh.SetTriangles(triindices, 0);
        mesh.SetNormals(normals);
    }
    public void OnDrawGizmos()
    {
        for(int i=0;i<4;++i)
        Gizmos.DrawSphere(GetPosition(i), 0.05f);

        Handles.DrawBezier(GetPosition(0), GetPosition(3), GetPosition(1), GetPosition(2), Color.white,EditorGUIUtility.whiteTexture,1f);
        OrientedPoint Point = GetBezierPoint(tTest);
        Handles.PositionHandle(Point.pos,Point.rot);

        float radius = 0.15f;
        void DrawPoint(Vector2 localPos)=> Gizmos.DrawSphere(Point.LocalToWorld(localPos), radius);


        //
       Vector3[] localVerts= shape2D.vertices.Select(v => Point.LocalToWorld(v.point)).ToArray();
        for (int i=0;i< shape2D.lineIndices.Length;i+=2)
        {
            Vector3 a = localVerts[shape2D.lineIndices[i]];
            Vector3 b = localVerts[shape2D.lineIndices[i + 1]];
            Gizmos.DrawLine(a, b);
        }
    }
    public OrientedPoint GetBezierPoint(float t)
    {
        Vector3 p0 = GetPosition(0);
        Vector3 p3 =GetPosition(3);
        Vector3 p1= GetPosition(1);
        Vector3 p2= GetPosition(2);

        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        Vector3 pos = Vector3.Lerp(d, e, t);
        Vector3 tangent = (e - d).normalized;
        return new OrientedPoint(pos,tangent);

    }



}
