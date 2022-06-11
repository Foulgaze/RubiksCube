using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeFace 
{
    GameObject block;
    int faceSize;
    int scaleSize;
    float scaleValue;
    Faces face;
    public GameObject[,] blockGOs;
    public char[,] blockCols;

    public cubeFace(int faceSize, int scaleSize, Faces f, char c)
    {
        this.faceSize = faceSize;
        this.face = f;
        this.scaleSize = scaleSize;
        this.scaleValue = 0.5f * (faceSize - 1);

        blockGOs = new GameObject[faceSize, faceSize];
        blockCols = new char[faceSize, faceSize];

        setColorBlocks(c);
        
    }

    public void spawnBlocks(Dictionary<Vector3, GameObject> blocks, Texture2D text, Shader sha)
    {
        string g = "";

        for (int i = 0; i < faceSize; ++i)
        {
            for(int a = 0; a < faceSize; ++a)
            {
                int iTemp = i, aTemp = a;
                float vecValOne = scaleValue - (scaleSize * iTemp), vecValTwo = scaleValue - (scaleSize * aTemp);
                
                Vector3 posVector = new Vector3();

                switch (face)
                {
                    case Faces.Front:
                        posVector = new Vector3(vecValTwo, vecValOne, scaleValue);
                        break;
                    case Faces.Back:
                        aTemp = faceSize - 1 - a;
                        posVector = new Vector3(vecValTwo, vecValOne, -scaleValue);
                        break;
                    case Faces.Up:
                        iTemp = faceSize - 1 - i;
                        posVector = new Vector3(vecValTwo, scaleValue, vecValOne);
                        break;
                    case Faces.Down:
                        posVector = new Vector3(vecValTwo, -scaleValue, vecValOne);
                        break;
                    case Faces.Right:
                        posVector = new Vector3(-scaleValue,vecValOne, vecValTwo);
                        break;
                    case Faces.Left:
                        aTemp = faceSize - 1 - a;
                        posVector = new Vector3(scaleValue, vecValOne, vecValTwo);
                        break;
                }
                if(blocks.ContainsKey(posVector))
                {
                    blockGOs[iTemp, aTemp] = blocks[posVector];
                }
                else
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.localScale = new Vector3(scaleSize,scaleSize,scaleSize);
                    Material cubeMat = new Material(sha);
                    cubeMat.SetTexture("_MainTex", text);
                    cube.transform.GetComponent<Renderer>().material = cubeMat;
                    cube.transform.name = "" + blocks.Count;
                    cube.tag = "cube";
                    setRubikSide(cube);
                    cube.transform.position = posVector;
                    blockGOs[iTemp, aTemp] = cube;
                    blocks.Add(posVector, cube);
                }
            }
        }
    }

    void setColorBlocks(char c)
    {
        for(int i = 0; i < faceSize; ++i)
        {
            for(int a = 0; a < faceSize; ++a)
            {
                blockCols[i, a] = c;
            }
        }
    }

    void setRubikSide(GameObject cube) // BIG YOINKED https://answers.unity.com/questions/542787/change-texture-of-cube-sides.html
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

    public void printArr()
    {
        string g = "";
        for (int i = 0; i < faceSize; ++i)
        {
            for (int a = 0; a < faceSize; ++a)
            {
                g += blockGOs[i, a].name + " ";
            }
            g += "\n";
        }
        g += "\n\n";

        for (int i = 0; i < faceSize; ++i)
        {
            for (int a = 0; a < faceSize; ++a)
            {
                g += blockCols[i, a] + " ";
            }
            g += "\n";
        }
        Debug.Log(g);
    }

    public bool checkForCompletion()
    {
        char startChar = blockCols[0, 0];
        for(int i = 0; i < faceSize; ++i)
        {
            for(int a = 0; a < faceSize; ++a)
            {
                if(startChar != blockCols[i,a])
                {
                    return false;
                }
            }
        }
        return true;
    }
}
