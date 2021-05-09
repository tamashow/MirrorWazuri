using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class String : MonoBehaviour
{
    GameObject[] StringPoints;
    DistanceJoint2D[] distanceJoints;
    LineRenderer lr;
    Vector3[] positions;
    int pointNum;
    // Start is called before the first frame update
    void Start()
    {
        pointNum = transform.childCount;
        StringPoints = new GameObject[pointNum];
        positions = new Vector3[pointNum];
        for(int i=0;i < pointNum;i++)
        {
            // Debug.Log(transform.GetChild(i+1).gameObject.name);
            StringPoints[i] = transform.GetChild(i).gameObject;
        }
        
        lr = GetComponent<LineRenderer>();
        lr.positionCount = pointNum;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i=0;i<pointNum;i++)
        {
            positions[i]=StringPoints[i].transform.position;
            // distanceJoints[i].distance -= Input.GetAxisRaw("Vertical")*speed;
        }
        lr.SetPositions(positions);
        // foreach(var dj in distanceJoints)
        // {

        // }
    }
}
