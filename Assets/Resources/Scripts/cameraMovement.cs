using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class cameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius;
    int horiRotation;
    int vertRotation;
    public GameObject menu;
    int flip;
    Vector3 startPosition { get; set; }
    void Start()
    {
        initVars();
    }



    void setHorizontal()
    {
        radius = (float) Sqrt((double)(transform.position.x * transform.position.x + transform.position.z * transform.position.z));
    }


    void initVars()
    {
        horiRotation = 0;
        vertRotation = 0;
        flip = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(menu.activeSelf)
        {
            if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)))//&& transform.position.y == 0
            {
                setHorizontal();
                int direction = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) ? -1 : 1;
                horiRotation += 45 * direction;
                horiRotation %= 360;
                transform.position = new Vector3(roundFloat((float)(radius * Sin(horiRotation * (PI / 180)))), transform.position.y, roundFloat((float)(radius * Cos(horiRotation * (PI / 180)))));
                //transform.rotation *= Quaternion.Euler(0, 45.0f * direction, 0);
                transform.LookAt(new Vector3(0, 0, 0));

            }
            else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)))//&& (transform.position.x == 0 || transform.position.z == 0)
            {
                setHorizontal();
                int direction = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) ? 1 : -1;
                if ((direction == -1 && transform.position.y >= 0) || (direction == 1 && transform.position.y <= 0))
                {
                    if(transform.position.y == 0)
                    {
                        transform.position += new Vector3(0, (radius / 1.5f) * direction, 0);
                    }
                    else
                    {
                        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                    }

                    transform.LookAt(new Vector3(0, 0, 0));

                }


            }
        }
        


    }

    float roundFloat(float f)
    {
        return Mathf.Round((f * 10000)) / 10000;
    }
}
