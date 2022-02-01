using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class Rotation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject middle;
    float distance;
    double counter;
    double counterAmount;

    void Start()
    {
        distance = Vector3.Distance(transform.position, middle.transform.position);
        counterAmount = (2 * PI) / (60.0 * 4);
        Debug.Log(counterAmount);
        setCounter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        counter += counterAmount;
        transform.position = new Vector3((float)(distance * Cos(counter)), (float)(distance * Sin(counter)), 0);
    }

    void setCounter()
    {
        counter = Atan((double) transform.position.y / transform.position.x);
        if(transform.position.x < 0)
        {
            if (transform.position.y < 0)
            {
                Debug.Log("This happened");
                counter -= (2 * PI) / 2.0;
            }
            else
            {
                counter -= (2 * PI) / 2.0;
            }
        }
    }
    

}
