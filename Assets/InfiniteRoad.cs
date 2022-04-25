using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRoad : MonoBehaviour
{
    const int SegmentCount = 10;
    const int SegmentRender = 3;
    int firstRendered;
    int lastRendered;
    //[RequireComponent(typeof(RoadSegment))]
    [SerializeField]
    GameObject SegmentPrefab;
    //GameObject[] roadSegment =new GameObject[10];
    public RoadSegment lastSeg;
    [SerializeField]
     public float t=0;
    // Start is called before the first frame update
    private void Awake()
    {
        t = 0;
    }
    void Start()
    {
       
        firstRendered = 0;
        lastRendered = SegmentRender-1;
        intialGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime*0.2f;
        if (t >= 1)
        {
            GenerateNextSegment();
            t = 0;
         }
    }
    void intialGeneration()
    {
        RoadSegment firstSeg;
        GameObject cursegmentf = Instantiate(SegmentPrefab, gameObject.transform);
        firstSeg = cursegmentf.GetComponent<RoadSegment>();
        firstSeg.startPoint.position = gameObject.transform.position;
        firstSeg.startPoint.rotation= gameObject.transform.rotation;
        firstSeg.startPoint.localScale = new Vector3(1,1,Random.Range(1,7));
        firstSeg.EndPoint.position = new Vector3(Random.Range(-10, 40), Random.Range(-10, 40), Random.Range(-10, 40));
        firstSeg.EndPoint.rotation = Random.rotation;
        firstSeg.EndPoint.localScale = new Vector3(1, 1, Random.Range(1, 7));
        RoadSegment SecondSeg;
        GameObject cursegments = Instantiate(SegmentPrefab, gameObject.transform);
        SecondSeg = cursegments.GetComponent<RoadSegment>();
        SecondSeg.startPoint = firstSeg.EndPoint;
        SecondSeg.EndPoint.position = new Vector3(Random.Range(-10, 40), Random.Range(-10, 40), Random.Range(-10, 40));
        SecondSeg.EndPoint.rotation = Random.rotation;
        SecondSeg.EndPoint.localScale = new Vector3(1, 1, Random.Range(1, 7));
        lastSeg = SecondSeg;
    }
          
    void GenerateNextSegment()
    {
      /*  GameObject cursegmentNext= Instantiate(SegmentPrefab, gameObject.transform);
        RoadSegment nextSeg;
        nextSeg = cursegmentNext.GetComponent<RoadSegment>();
        nextSeg.startPoint = lastSeg.EndPoint;
        nextSeg.EndPoint.position = new Vector3(Random.Range(-10, 40), Random.Range(-10, 40), Random.Range(-10, 40));
        nextSeg.EndPoint.rotation = Random.rotation;
        nextSeg.EndPoint.localScale = new Vector3(1, 1, Random.Range(1, 7));
        lastSeg = nextSeg;*/
    }
}
