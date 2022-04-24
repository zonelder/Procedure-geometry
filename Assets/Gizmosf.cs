using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Gizmosfs
{


    public static void DrawWireCircle(Vector3 pos, Quaternion rot, float radius, int detail = 32)
    {
        Vector3[] points3D = new Vector3[detail];
        for (int i = 0; i < detail; i++)
        {
            float t = i / (float)detail;
            float angRad = t * Mathfs.TAU;
            Vector2 point2D = Mathfs.GetUnitVectorByAngle(angRad);
            point2D *= radius;
            points3D[i] = pos + rot * point2D;
        }
        //рисуем точки.
        for (int i = 0; i < detail - 1; i++)
        {
            //Gizmos.DrawSphere(points3D[i], 0.02f);
            Gizmos.DrawLine(points3D[i], points3D[i + 1]);
        }

        Gizmos.DrawLine(points3D[detail - 1], points3D[0]);

        //vector3[] points3D new Vector3[details];
    }
}
