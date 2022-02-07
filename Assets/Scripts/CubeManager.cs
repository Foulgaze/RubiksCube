using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class CubeManager : MonoBehaviour
{
    private int Bottom = 0;
    private int Back = 1;
    private int Right = 2;
    private int Front = 3;
    private int Left = 4;
    private int Top = 5;

    public const int faceSize = 3;
    //public edgeRotater er;
    private Face[] faces;
    private Material cubeMat;
    private Face currFace;
    private Cube rubiksCube;
    private double counter;
    private double counterAmount;
    private int FPS = 10;
    private int randomizing;
    private int randomizeCount = 25; // Amount of turns the cube does during randomization

    void Start()
    {
        initVars();
        createMat();
        createCubes();
        //faces[Front].colorBlocks[2, faceSize - 1] = 'Q';

    }

    // Update is called once per frame
    void Update()
    {
        if (!isRotating())
        {
            checkForInput();
            checkForDebugInput();
            checkForShuffling();
        }



    }

    void FixedUpdate()
    {
        if (isRotating())
        {
            GameObject[,] cubes = currFace.faceBlocks;
            for (int i = 0; i < faceSize; ++i)
            {
                for (int a = 0; a < faceSize; ++a)
                {
                    GameObject currCube = cubes[i, a];
                    if (currCube != currFace.middleCube)
                    {
                        edgeRotater er = currCube.transform.GetComponent<edgeRotater>();

                        if (counter == 0) // Init vars first loop
                        {
                            er.middle = currFace.middleCube;
                            er.initVars(currFace.rotationDirection);
                        }

                        Vector3 moveVector = createRotationVector(er.middle, currFace.rotationDirection, er.distanceOffset, er.tOffset + counter);
                        currCube.transform.position = moveVector;
                    }
                    if (counter != 0)
                    {
                        currCube.transform.Rotate(currFace.rotationDirection, Space.World);
                    }


                }
            }


            if (Abs(counter) >= (2 * PI) / 4)
            {
                currFace.adjustArrayPostRotation();
                currFace.adjustFacesPostRotation();
                //rubiksCube.adjustFacePostRotation<char>(0, 1, faces[Right].colorBlocks, faces[Top].colorBlocks, faces[Left].colorBlocks, faces[Bottom].colorBlocks);
                //currFace.setColor();
                //currFace.printArray();
                counter = 0;
                currFace = null;

            }
            else
            {
                counter += counterAmount * currFace.clockWise;
            }
        }
    }

    void checkForInput()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Bottom Left to Top Right
        {
            currFace = faces[3];
        }
        else if (Input.GetKeyDown(KeyCode.U)) // Bot Left to Top Right
        {
            currFace = faces[5];
        }
        else if (Input.GetKeyDown(KeyCode.D)) // Bot Right to Top Left
        {
            currFace = faces[0];
        }
        else if (Input.GetKeyDown(KeyCode.B)) // Bot Right to Top Left
        {
            currFace = faces[1];
        }
        else if (Input.GetKeyDown(KeyCode.R)) // L to R
        {
            currFace = faces[4];
        }
        else if (Input.GetKeyDown(KeyCode.L)) // R to L 
        {
            currFace = faces[2];
        }

        else if (Input.GetKeyDown(KeyCode.G)) // Randomize Cube
        {
            randomizing = randomizeCount;
        }


    }

    void checkForDebugInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) // Bottom Left to Top Right  // Debugging
        {

            faces[3].setColor(false);
            faces[3].printArray();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) // Bot Left to Top Right
        {
            faces[5].setColor(false);
            faces[5].printArray();

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) // Bot Right to Top Left
        {
            faces[0].setColor(false);
            faces[0].printArray();

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) // Bot Right to Top Left
        {
            faces[1].setColor(false);
            faces[1].printArray();

        }
        else if (Input.GetKeyDown(KeyCode.Period)) // L to R
        {
            faces[4].setColor(false);
            faces[4].printArray();

        }
        else if (Input.GetKeyDown(KeyCode.Comma)) // R to L 
        {
            faces[2].setColor(false);
            faces[2].printArray();

        }
        else if(Input.GetKeyDown(KeyCode.Y))
        {
            //rubiksCube.adjustFacePostRotation<char>(0, 2, faces[Right].colorBlocks, faces[Top].colorBlocks, faces[Left].colorBlocks, faces[Bottom].colorBlocks);
            //rubiksCube.adjustFacePostRotation<char>(1, 2, faces[Front].colorBlocks, faces[Top].colorBlocks, faces[Back].colorBlocks, faces[Bottom].colorBlocks);
            rubiksCube.adjustFacePostRotation<char>(2, 2, faces[Front].colorBlocks, faces[Left].colorBlocks, faces[Back].colorBlocks, faces[Right].colorBlocks);

        }
        else if(Input.GetKeyDown(KeyCode.Z))
        {
            for(int c = 0; c < 6; ++c)
            {
                string g = "";
                for (int i = 0; i < faceSize; ++i)
                {
                    for (int a = 0; a < faceSize; ++a)
                    {
                        g += faces[c].colorBlocks[i, a] + ",";
                    }
                    g += "\n";
                }
                Debug.Log(g);
            }
            
        }
        if (currFace != null)
        {
            string g = "";
            for (int i = 0; i < faceSize; ++i)
            {
                for (int a = 0; a < faceSize; ++a)
                {
                    g += currFace.colorBlocks[i, a] + ",";
                }
                g += "\n";
            }
            Debug.Log(g);
        }
    }

    void checkForShuffling()
    {
        if (currFace != null || !isRandomizing()) { return; }

        
        --randomizing;
        currFace = faces[UnityEngine.Random.Range(0, 5)];
        
    }
    Vector3 createRotationVector(GameObject middleCube, Vector3 rotationDirection, double distanceOffset, double counter)
    {
        int rotationDir = 0; // Value to know when to skip Index
        Vector3 moveVector = new Vector3(0, 0, 0);

        if (rotationDirection.x != 0) // Skip this value
        {
            ++rotationDir;
            moveVector.x = middleCube.transform.position.x;
        }
        else
        {
            moveVector.x = middleCube.transform.position.x + (float)(distanceOffset * Cos(counter));
        }

        if (rotationDirection.y != 0)
        {
            moveVector.y = middleCube.transform.position.y;
        }
        else
        {
            if (rotationDir != 0) // Skip the first value, second value is Cos
            {
                moveVector.y = middleCube.transform.position.y + (float)(distanceOffset * Cos(counter));
            }
            else // First value is Cos, Second value is Sin
            {
                moveVector.y = middleCube.transform.position.y + (float)(distanceOffset * Sin(counter));
            }
        }

        if (rotationDirection.z == 0)
        {
            moveVector.z = middleCube.transform.position.z + (float)(distanceOffset * Sin(counter));
        }
        else
        {
            moveVector.z = middleCube.transform.position.z;
        }
        //Debug.Log(distanceOffset);
        return moveVector;


    }

    void initalizeFaceColors()
    {
        
    }

    void fillFaceArray(char[,] arr,char c)
    {
        for(int i = 0; i < faceSize; ++i)
        {
            for(int a = 0; a < faceSize; ++a)
            {
                arr[i,a] = c;
            }
        }
    }

    bool isRotating()
    {
        return currFace != null;
    }

    void createMat() // Creates the material that the cubes use
    {
        Texture2D cubeText = Resources.Load<Texture2D>("Textures/RubiksTextV");
        cubeMat = new Material(Shader.Find("Specular"));
        cubeMat.SetTexture("_MainTex", cubeText);
    }

    void initVars()
    {

        float rotValue = ((float)360) / (FPS * 4);
        faces = new Face[6]; // Rubiks cube always has 6 faces
        faces[Bottom] = new Face(faceSize, new Vector3(0, -rotValue, 0), 1, true, Bottom); // Bottom
        faces[Back] = new Face(faceSize, new Vector3(0, 0, -rotValue), -1, true, Back); // Back
        faces[Right] = new Face(faceSize, new Vector3(rotValue, 0, 0), 1, true, Right); // Right
        faces[Front] = new Face(faceSize, new Vector3(0, 0, rotValue), 1, false, Front); // Front
        faces[Left] = new Face(faceSize, new Vector3(-rotValue, 0, 0), -1, false, Left); // Left
        faces[Top] = new Face(faceSize, new Vector3(0, rotValue, 0), -1, false, Top); // Top

        // These values are necessary for turning the cube
        faces[Bottom].relevantFaces = new Face[] { faces[Front], faces[Right], faces[Back], faces[Left], faces[Top] };
        faces[Top].relevantFaces = new Face[] { faces[Back], faces[Right], faces[Front], faces[Left],faces[Back] };
        faces[Back].relevantFaces = new Face[] { faces[Bottom], faces[Right], faces[Top], faces[Left], faces[Front] };
        faces[Front].relevantFaces = new Face[] { faces[Top], faces[Right], faces[Bottom], faces[Left], faces[Back] };
        faces[Right].relevantFaces = new Face[] { faces[Top], faces[Back], faces[Bottom], faces[Front], faces[Left] };
        faces[Left].relevantFaces = new Face[] { faces[Top], faces[Front], faces[Bottom], faces[Back], faces[Right] };

        // This initalizes the values for the face colors
        fillFaceArray(faces[Front].colorBlocks, 'W');
        fillFaceArray(faces[Back].colorBlocks, 'Y');
        fillFaceArray(faces[Top].colorBlocks, 'B');
        fillFaceArray(faces[Bottom].colorBlocks, 'G');
        fillFaceArray(faces[Left].colorBlocks, 'O');
        fillFaceArray(faces[Right].colorBlocks, 'R');


        rubiksCube = new Cube(faces, faceSize);
        counterAmount = (2 * PI) / (FPS * 4.0);
        counter = 0;
        randomizing = 0;
        currFace = null;
    }

    void createCubes() // Loads in faceSize x faceSize cube 
    {
        int startValue = faceSize % 2 == 0 ? faceSize -1: faceSize / 2;
        int endValue = faceSize % 2 == 0 ? 0 : -faceSize / 2;
        for (int i = startValue; i >= endValue; --i) // Y
        {
            for (int a = startValue; a >= endValue; --a) // Z 
            {

                for (int b = startValue; b >= endValue; --b) // X
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(b, -i, a);
                    cube.transform.GetComponent<Renderer>().material = cubeMat;
                    setRubikSide(cube, i, a, b);
                    cube.transform.parent = transform;
                    cube.transform.name = $"{i}{a}{b}";
                    cube.AddComponent<edgeRotater>();
                    addToFace(cube, i, a, b);
                }
            }
        }
        if (faceSize % 2 == 0) // Set the middle cubes for even cubes
        {
            for (int i = 0; i < faces.Length; ++i)
            {
                GameObject newmiddle = new GameObject();
                newmiddle.transform.position = calculateMiddleEvenCube(faces[i]);
                newmiddle.name = $"{i}";
                faces[i].middleCube = newmiddle;    


            }

        }
    }

    void addToFace(GameObject cube, int yPos, int zPos, int xPos)
    {
        bool even = faceSize % 2 == 0;
        if ((!even && zPos == faceSize / 2) || (even && zPos == faceSize - 1)) // Back of Cube
        {
            if(yPos == xPos && yPos == 0) // The format of all middle checks are check for linearaity, then check to see if middle layer
            {
                faces[3].middleCube = cube;
            }
            faces[3].addToArray(cube);
        }
        if((!even && yPos == faceSize/2) || (even && yPos == faceSize -1)) // Top of Cube
        {
    
            if(zPos == xPos && zPos == 0)
            {
                faces[0].middleCube = cube;
            }
            faces[0].addToArray(cube);
        }
        if ((!even && yPos == -faceSize/2) || (even && yPos == 0)) // Bottom of Cube
        {
            if (zPos == xPos && zPos == 0)
            {
                faces[5].middleCube = cube;
            }
            faces[5].addToArray(cube);
        }
        if ((!even && zPos == -faceSize/2) || (even && zPos == 0)) // Front of Cube
        {
            if(xPos == yPos && yPos == 0)
            {
                faces[1].middleCube = cube;
            }
            faces[1].addToArray(cube);
        }
        if ((!even && xPos == faceSize/2) || (even && xPos == faceSize - 1)) // Left of Cube
        {
            if(zPos == yPos && yPos == 0)
            {
                faces[2].middleCube = cube;
            }
            faces[2].addToArray(cube);
        }
        if((!even && xPos == -faceSize/2) || (even && xPos == 0)) // Right of Cube
        {
            if(zPos == yPos && yPos == 0)
            {
                faces[4].middleCube = cube;
            }
            faces[4].addToArray(cube);
        }
    }

    void setRubikSide(GameObject cube, int yPos, int zPos, int xPos) // BIG YOINKED https://answers.unity.com/questions/542787/change-texture-of-cube-sides.html
    {
        Mesh mesh = cube.transform.GetComponent<MeshFilter>().mesh;
        Vector2[] UVs = new Vector2[mesh.vertices.Length];
        
        // Front
        UVs[0] = new Vector2(0.0f, 0.0f);
        UVs[1] = new Vector2(0.333f, 0.0f);
        UVs[2] = new Vector2(0.0f, 0.333f);
        UVs[3] = new Vector2(0.333f, 0.333f);
        // Top
        UVs[4] = new Vector2(0.334f, 0.333f);
        UVs[5] = new Vector2(0.666f, 0.333f);
        UVs[8] = new Vector2(0.334f, 0.0f);
        UVs[9] = new Vector2(0.666f, 0.0f);
        // Back
        UVs[6] = new Vector2(1.0f, 0.0f);
        UVs[7] = new Vector2(0.667f, 0.0f);
        UVs[10] = new Vector2(1.0f, 0.333f);
        UVs[11] = new Vector2(0.667f, 0.333f);
        // Bottom
        UVs[12] = new Vector2(0.0f, 0.334f);
        UVs[13] = new Vector2(0.0f, 0.666f);
        UVs[14] = new Vector2(0.333f, 0.666f);
        UVs[15] = new Vector2(0.333f, 0.334f);
        // Left
        UVs[16] = new Vector2(0.334f, 0.334f);
        UVs[17] = new Vector2(0.334f, 0.666f);
        UVs[18] = new Vector2(0.666f, 0.666f);
        UVs[19] = new Vector2(0.666f, 0.334f);
        // Right        
        UVs[20] = new Vector2(0.667f, 0.334f);
        UVs[21] = new Vector2(0.667f, 0.666f);
        UVs[22] = new Vector2(1.0f, 0.666f);
        UVs[23] = new Vector2(1.0f, 0.334f);
        mesh.uv = UVs;
        
    }

    public Vector3 calculateMiddleEvenCube(Face f)
    {
       
        Vector3 retVec = new Vector3(0,0,0);
        if (f.rotationDirection.x == 0)
        {
            retVec.x = (f.faceBlocks[faceSize / 2, faceSize / 2].transform.position.x + f.faceBlocks[faceSize / 2, (faceSize / 2) - 1].transform.position.x) / 2.0f;
        } 
        else
        {
            retVec.x = f.faceBlocks[0, 0].transform.position.x;
        }
        if(retVec.y == 0)
        {
            retVec.y = (f.faceBlocks[faceSize / 2, faceSize / 2].transform.position.y + f.faceBlocks[(faceSize / 2) - 1, (faceSize / 2) ].transform.position.y) / 2.0f;
        }
        else
        {
            retVec.y = f.faceBlocks[0, 0].transform.position.y;
        }
        if (retVec.z == 0)
        {
            if(f.dir == 2 || f.dir == 4)
            {
                retVec.z = (f.faceBlocks[faceSize / 2, faceSize / 2].transform.position.z + f.faceBlocks[(faceSize / 2), (faceSize / 2) - 1].transform.position.z) / 2.0f;
            }
            else
            {
                retVec.z = (f.faceBlocks[faceSize / 2, faceSize / 2].transform.position.z + f.faceBlocks[(faceSize / 2) - 1, (faceSize / 2)].transform.position.z) / 2.0f;
            }
        }
        else
        {
            retVec.z = f.faceBlocks[0, 0].transform.position.z;
        }
        
        return retVec;
        
    }

    bool isRandomizing()
    {
        return randomizing != 0;
    }

}
