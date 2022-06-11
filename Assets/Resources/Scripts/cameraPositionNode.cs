using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPositionNode
{
    Vector3 position;
    cameraPositionNode left { get; set; }
    cameraPositionNode right { get; set; }
    cameraPositionNode up { get; set; }
    cameraPositionNode down { get; set; }

    cameraPositionNode(Vector3 position)
    {
        this.position = position;
    }
    

}
