using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using static System.Math;

public enum Faces { Front, Back, Right, Left, Up, Down };

public class faceManager : MonoBehaviour
{
    int faceSize = 3;
    public Dropdown facedd;
    public Dropdown sidedd;
    public GameObject camera;
    public Text timerText;

    cubeFace[] faceArr;
    Texture2D cubeText;
    Shader cubeShader;
    private double counter;
    private int FPS = 14;
    private float rotValue;
    private Vector3 globalRotation;
    private int negative;
    private int rotateCounter;

    private GameObject parentRotation;
    private Vector3 objPosition;
    private bool timer;
    private bool timer2;
    private float clock;

    private int seconds;
    private int minutes;
    private int hours;



    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    void FixedUpdate()
    {
        if (isRotating())
        {
            if (counter >= 90)
            {
                counter = 0;
                parentRotation.transform.DetachChildren();
                if(rotateCounter != 0)
                {
                    --rotateCounter;
                    if(rotateCounter == 0)
                    {
                        if(timer)
                        {
                            timer2 = true;
                        }
                    }
                }
                GameObject.Destroy(parentRotation);
                parentRotation = null;
                negative = 1;
                if(timer2)
                {
                    if(checkForCompeltion())
                    {
                        timer2 = false;
                    }
                }

               
            }
            else
            {
                parentRotation.transform.RotateAround(objPosition, globalRotation.normalized, rotValue);
                counter += rotValue;
            }
        }

        if (rotateCounter != 0 && parentRotation == null)
        {
            sidedd.value = (int)UnityEngine.Random.Range(0f, sidedd.options.Count );
            
            facedd.value = (int)UnityEngine.Random.Range(0f, 3);
            rotateFace();
        }

        if(timer2)
        {
            clock += Time.deltaTime;
            if(clock > 1)
            {
                clock = 0;
                ++seconds;
                hours += (minutes / 60);
                minutes += (seconds / 60);
                minutes %= 60;
                seconds %= 60;
                String printString = "";
                if(hours <10)
                {
                    printString += "0";
                }
                printString += hours + ":";
                if (minutes < 10)
                {
                    printString += "0";
                }
                printString += minutes + ":";
                if(seconds < 10)
                {
                    printString += "0";
                }
                printString += seconds;
                timerText.text = printString;


            }
        }

    }

    public void speedSolve()
    {
        if(sidedd.IsActive())
        {
            rotateCounter = 20 * (faceSize / 2);
            timer = true;
        }
        
    }



    bool isRotating()
    {
        return parentRotation != null;
    }

    void initVars()
    {
        faceArr = new cubeFace[6];
        cubeText = (Texture2D) Resources.Load("Textures\\RubiksTextV");
        cubeShader = (Shader)Resources.Load("Textures\\basic");
        List<string> m_DropOptions = new List<string>();
        for (int i = 1; i <= faceSize; ++i)
        {
            if(faceSize % 2 == 0 || i != ((faceSize / 2) + 1))
            {
                m_DropOptions.Add("Side " + i);
            }
            
        }
        sidedd.AddOptions(m_DropOptions);
        timerText.text = "00:00:00";
        rotValue = (360) / (FPS*4);
        counter = 0;
        parentRotation = null;
        seconds = 0;
        hours = 0;
        minutes = 0;
        camera.transform.position = Vector3.zero;
        timer = false;
        timer2 = false;
    }
    void initFaces()
    {
        Dictionary<Vector3, GameObject> dict = new Dictionary<Vector3, GameObject>();
        char[] colors = { 'g', 'b', 'r', 'o', 'w', 'y' };
        for (int i = 0; i < 6; ++i)
        {
            faceArr[i] = new cubeFace(faceSize,1,(Faces) i,colors[i]);
            faceArr[i].spawnBlocks(dict,cubeText,cubeShader);
        }
        float camOffset = faceSize * 2;
        camera.transform.position += Vector3.forward * camOffset;
        camera.transform.rotation = Quaternion.Euler(0, 180, 0);
        camera.GetComponent<cameraMovement>().radius = camOffset;
    }
    // Update is called once per frame
    void Update()
    {

        
        if(Input.GetKeyDown(KeyCode.Z))
        {
            for (int i = 0; i < faceArr.Length; ++i)
            {
                faceArr[i].printArr();
            }
        }
        else if(Input.GetKeyDown(KeyCode.T) && parentRotation == null)
        {
            //rotateFace();
        }
        else if (Input.GetKeyDown(KeyCode.R) && parentRotation == null)
        {
            sidedd.value = (int)UnityEngine.Random.Range(0f, faceSize);
            facedd.value = (int)UnityEngine.Random.Range(0f, 3);
            Debug.Log($"[{facedd.value} | {sidedd.value}");
            //rotateFace();
        }
        else if(Input.mouseScrollDelta.y != 0 && sidedd.IsActive())
        {
            Vector3 moveVector = camera.transform.position.normalized * Input.mouseScrollDelta.y * (faceSize / 2) * -1;
            if(Vector3.Distance(camera.transform.position + moveVector,Vector3.zero) >= (faceSize/2 + 1))
            {
                //re
                camera.transform.position += moveVector;
            }
        }               
    }



    public void debug(FileStream fs)
    {
        Byte[] title = new UTF8Encoding(true).GetBytes($"{facedd},{sidedd},{camera},{globalRotation},{parentRotation}\n");
        fs.Write(title, 0, title.Length);
        createCube(faceSize);
        fs.Write(title, 0, title.Length);

    }

    public void scrambleCube()
    {
        rotateCounter = 20 * (faceSize/2);
    }

    public void destroyCube()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("cube"))
        {
            GameObject.Destroy(go);
        }
    }

    public void createCube(int fs)
    {
        this.faceSize = fs;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("cube"))
        {
            GameObject.Destroy(go);
        }
        sidedd.options.Clear();
        initVars();
        initFaces();
    }

    bool checkForCompeltion()
    {
        for(int i = 0; i < 6; ++i)
        {
            if(!faceArr[i].checkForCompletion())
            {
                return false;
            }
        }
        return true;
    }

    public void buttonRotate()
    {
        if(rotateCounter == 0 && parentRotation == null)
        {
            rotateFace();
        }
    }

    public void rotateFace()
    {
        HashSet<GameObject> set = null;
        cubeFace f = null;
        Vector3 rotationVec = new Vector3();
        int ddValue = sidedd.value;
        negative = 1;
        int rotateCount = 1;
        if(faceSize % 2 != 0 &&  sidedd.value >= faceSize / 2)
        {
            ++ddValue;
        }
        int rotateAmount = ddValue >= faceSize / 2 ? 3 : 1;
        switch (facedd.value)
        {
            default:
            case 0: // Frontward Turn
                rotationVec = ddValue >= faceSize / 2 ? new Vector3(0, 0, -rotValue) : new Vector3(0, 0, rotValue);
                f = ddValue == 0 ? faceArr[(int)Faces.Front] : ddValue == faceSize - 1 ? faceArr[(int)Faces.Back] : null;
                //rotateCount = sidedd.value == faceSize - 1 ? 3 : 1; 
                
                for(int i = 0; i < rotateAmount; ++i)
                {
                    set = arrayManipulation.frontwardsTurn<GameObject>(faceArr[(int)Faces.Front].blockGOs, faceArr[(int)Faces.Back].blockGOs, faceArr[(int)Faces.Right].blockGOs, faceArr[(int)Faces.Left].blockGOs, faceArr[(int)Faces.Up].blockGOs, faceArr[(int)Faces.Down].blockGOs, ddValue, faceSize);
                    arrayManipulation.frontwardsTurn<char>(faceArr[(int)Faces.Front].blockCols, faceArr[(int)Faces.Back].blockCols, faceArr[(int)Faces.Right].blockCols, faceArr[(int)Faces.Left].blockCols, faceArr[(int)Faces.Up].blockCols, faceArr[(int)Faces.Down].blockCols, ddValue, faceSize);
                }
               

                break;
            case 1: // Sideward Turn
                rotationVec = ddValue >= faceSize / 2 ? new Vector3(-rotValue, 0, 0) : new Vector3(rotValue, 0, 0);
                f = ddValue == 0 ? faceArr[(int)Faces.Left] : ddValue == faceSize - 1 ? faceArr[(int)Faces.Right] : null;
                rotateCount = ddValue == faceSize - 1 ? 1 : 1;
                int rotSpecial = ddValue >= faceSize/2 ? 1 : 3;
                for (int i = 0; i < rotSpecial; ++i)
                {
                    set = arrayManipulation.sidewardTurn<GameObject>(faceArr[(int)Faces.Front].blockGOs, faceArr[(int)Faces.Back].blockGOs, faceArr[(int)Faces.Right].blockGOs, faceArr[(int)Faces.Left].blockGOs, faceArr[(int)Faces.Up].blockGOs, faceArr[(int)Faces.Down].blockGOs, ddValue, faceSize);
                    arrayManipulation.sidewardTurn<char>(faceArr[(int)Faces.Front].blockCols, faceArr[(int)Faces.Back].blockCols, faceArr[(int)Faces.Right].blockCols, faceArr[(int)Faces.Left].blockCols, faceArr[(int)Faces.Up].blockCols, faceArr[(int)Faces.Down].blockCols, ddValue, faceSize);
                }
                negative = -1;
                break;
            case 2: // Upwards Turn
                rotationVec = ddValue >= faceSize / 2 ? new Vector3(0, -rotValue, 0) : new Vector3(0, rotValue, 0);
                //rotateCount = ddValue == faceSize - 1 ? 3 : 1;
                negative = -1;
                f = ddValue == 0 ? faceArr[(int)Faces.Up] : ddValue == faceSize - 1 ? faceArr[(int)Faces.Down] : null;
                for (int i = 0; i < rotateAmount; ++i)
                {
                    set = arrayManipulation.upwardTurn<GameObject>(faceArr[(int)Faces.Front].blockGOs, faceArr[(int)Faces.Back].blockGOs, faceArr[(int)Faces.Right].blockGOs, faceArr[(int)Faces.Left].blockGOs, faceArr[(int)Faces.Up].blockGOs, faceArr[(int)Faces.Down].blockGOs, ddValue, faceSize);
                    arrayManipulation.upwardTurn<char>(faceArr[(int)Faces.Front].blockCols, faceArr[(int)Faces.Back].blockCols, faceArr[(int)Faces.Right].blockCols, faceArr[(int)Faces.Left].blockCols, faceArr[(int)Faces.Up].blockCols, faceArr[(int)Faces.Down].blockCols, ddValue, faceSize);
                }
                break;
        }
        GameObject rotateObj = new GameObject();

        if (f != null)
        {
            foreach (GameObject o in f.blockGOs)
            {
                set.Add(o);
            }
            arrayManipulation.rotateFace(f, faceSize,rotateCount);
        }
        
        Vector3 middlePos = new Vector3();
        foreach (GameObject o in set)
        {
            middlePos += o.transform.position;
            o.transform.parent = rotateObj.transform;
        }

        middlePos /= set.Count;
        objPosition = middlePos;
        

        parentRotation = rotateObj;

        globalRotation = rotationVec;

    }



}
