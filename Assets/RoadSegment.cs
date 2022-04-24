using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class RoadSegment : MonoBehaviour
{
    public Transform[] controlPoints =new Transform[4];
    Vector3 GetPosition(int i) => controlPoints[i].position;

    public void OnDrawGizmos()
    {
        for(int i=0;i<4;++i)
        Gizmos.DrawSphere(GetPosition(i), 0.05f);
    }
}
