using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;


public class edgeRotater : MonoBehaviour
{
    public GameObject middle;
    public double tOffset { get; set; }
    public double distanceOffset { get; set; }

    public void initVars(Vector3 rotationDirection)
    {
        setDistanceOffset();
        setTOffset(rotationDirection);

    }

    public void setTOffset(Vector3 rotationDirection)
    {
        float distBetweenX = transform.position.x - middle.transform.position.x;
        float distBetweenY = transform.position.y - middle.transform.position.y;
        float distBetweenZ = transform.position.z - middle.transform.position.z;
        
        float opposite = rotationDirection.y == 0  && rotationDirection.x == 0 ?   distBetweenY  : distBetweenZ ;
        float adjacent = rotationDirection.x == 0 ?  distBetweenX : distBetweenY;

        bool oppositeCheck = rotationDirection.y == 0 && rotationDirection.x == 0 ? transform.position.y < middle.transform.position.y : transform.position.z < middle.transform.position.z;
        bool adjacentCheck = rotationDirection.x == 0 ? transform.position.x < middle.transform.position.x : transform.position.y < middle.transform.position.y;
       
        tOffset = Atan((double)opposite / adjacent);
        if (adjacentCheck)
        {
            if (oppositeCheck)
            {
                tOffset -= (2 * PI) / 2.0;
            }
            else
            {
                tOffset -= (2 * PI) / 2.0;
            }
        }
        
    }

    public void setDistanceOffset()
    {
        distanceOffset = Vector3.Distance(transform.position, middle.transform.position);
        
    }

}
