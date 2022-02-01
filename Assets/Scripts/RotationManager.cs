using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    public List<GameObject> faceList;
    public GameObject topLeft;
    public GameObject middle;
    public GameObject topMiddle;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        foreach(GameObject go in faceList)
        {
            go.transform.Rotate(new Vector3(0, 0, 1.5f));
        }
       /* middle.transform.Rotate(new Vector3(0, 0, 1.5f));
        topMiddle.transform.Rotate(new Vector3(0, 0, 1.5f));
        topLeft.transform.Rotate(new Vector3(0, 0, 1.5f));*/
        //Debug.Log((float)(middle.transform.rotation.z + (0.001 * (180 / 3.1415))));
    }
}
