using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToAboveRoad : MonoBehaviour
{
    static Vector3 up = new Vector3(0, 2, 0);
    [Range(0.01f,1)]
    [SerializeField]
    float timeSlower;
    [SerializeField]
    RoadSegment road;

    float t;
    // Start is called before the first frame update
    void Awake()
    {
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime*timeSlower;
        if (t >= 1)
            t = 0;
        OrientedPoint op = road.GetBezierPoint(t);
        gameObject.transform.position =op.LocalToWorld(up);
        gameObject.transform.rotation = op.rot;
    }
}
