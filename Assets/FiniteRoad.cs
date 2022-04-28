using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteRoad : MonoBehaviour
{


    private const int _segmentCount=10;
    [SerializeField]
    private GameObject SegmentPrefab;
    private RoadSegment[] segments=new RoadSegment[_segmentCount];
    private const int _lenght= 40;



    static Vector3 offSet = new Vector3(0, 2, 0);
    [SerializeField]
    private GameObject _camera;
    private float t = 0;
    private int segmentIndex = 0;
    void Start()
    {
        RoadSegment curSegment;
        Vector3 position = Vector3.zero;
        Quaternion rotation = Quaternion.Euler(0, 90, 0);
        for(int i=0;i<_segmentCount;++i)
        {
            GameObject segment= Instantiate(SegmentPrefab, gameObject.transform);
            segments[i] = segment.GetComponent<RoadSegment>();
            segments[i].startPoint.position = position;
            segments[i].startPoint.rotation = rotation;
            position += new Vector3(_lenght, 0,0);
            rotation = Quaternion.Euler(0, 90, Random.Range(-90,90));
            segments[i].EndPoint.position = position;
            segments[i].EndPoint.rotation = rotation; 
        }
    }

    void Update()
    {
        if (t >= 1)
        {
            segmentIndex++;
            t = 0;
        }
       
 
        if (segmentIndex >= _segmentCount)
            segmentIndex = 0;

        OrientedPoint op = segments[segmentIndex].GetBezierPoint(t);
        _camera.transform.position = op.LocalToWorld(offSet);
        _camera.transform.rotation = op.rot;
         t += Time.deltaTime;
    }
}
