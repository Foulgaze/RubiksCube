using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube 
{
    // I should define this outside of any class
    private int Bottom = 0;
    private int Back = 1;
    private int Right = 2;
    private int Front = 3;
    private int Left = 4;
    private int Top = 5;

    private Face[] faces;
    private int faceSize;
    public Cube(Face[] faces,int faceSize)
    {
        this.faces = faces;
        this.faceSize = faceSize;
    }
    public List<T> adjustFacePostRotation<T>(int rotationType, int arrPosition, T[,] frontBlocks, T[,] topBlocks, T[,] backBlocks, T[,] bottomBlocks, List<T> returnFaces)
    {
        T[] tempArr = new T[faceSize];
        
        switch (rotationType)
        {
            default:
            case 0: // Front/Back Turn Equivalent
                
                for (int i = 0; i < faceSize; ++i)
                {
                    tempArr[i] = frontBlocks[i, arrPosition];
                    frontBlocks[i, arrPosition] = bottomBlocks[arrPosition,faceSize-1- i];
                    bottomBlocks[arrPosition,  faceSize-1-i] = backBlocks[faceSize - 1 - i, faceSize-1-arrPosition];
                    backBlocks[faceSize - 1 - i, faceSize-1-arrPosition] = topBlocks[arrPosition, faceSize - 1 - i];
                    topBlocks[arrPosition, faceSize - 1 - i] = tempArr[i];

                    // New face to be turned
                    returnFaces.Add(backBlocks[faceSize - 1 - i, faceSize - 1 - arrPosition]);
                    returnFaces.Add(bottomBlocks[arrPosition, faceSize - 1 - i]);
                    returnFaces.Add(frontBlocks[i, arrPosition]);
                    returnFaces.Add(topBlocks[arrPosition, faceSize - 1 - i]);
                }

                break;
            case 1: // Left/Right Turn Equivalent
                for(int i = 0; i < faceSize; ++i)
                {
                    tempArr[i] = frontBlocks[i, arrPosition];
                    frontBlocks[i, arrPosition] = bottomBlocks[faceSize-1-i, faceSize - 1 - arrPosition];
                    bottomBlocks[faceSize - 1 - i, faceSize - 1 - arrPosition] = backBlocks[faceSize - 1 - i, faceSize - 1 - arrPosition];
                    backBlocks[faceSize - 1 - i, faceSize - 1 - arrPosition] = topBlocks[i, arrPosition];
                    topBlocks[i, arrPosition] = tempArr[i];

                    // New face to be turned
                    returnFaces.Add(backBlocks[faceSize - 1 - i, faceSize - 1 - arrPosition]);
                    returnFaces.Add(bottomBlocks[faceSize - 1 - i, faceSize - 1 - arrPosition]);
                    returnFaces.Add(frontBlocks[i, arrPosition]);
                    returnFaces.Add(topBlocks[i, arrPosition]);
                }

                break;
            case 2: // Top/bottom turn Equivalent
                for(int i = 0; i < faceSize; ++i)
                {
                    tempArr[i] = frontBlocks[arrPosition, i];
                    frontBlocks[arrPosition, i] = bottomBlocks[arrPosition, i];
                    bottomBlocks[arrPosition,i] = backBlocks[arrPosition, i];
                    backBlocks[arrPosition, i] = topBlocks[arrPosition,i];
                    topBlocks[arrPosition, i] = tempArr[i];

                    // New face to be turned
                    returnFaces.Add(backBlocks[arrPosition, i]);
                    returnFaces.Add(bottomBlocks[arrPosition, i]);
                    returnFaces.Add(frontBlocks[arrPosition, i]);
                    returnFaces.Add(topBlocks[arrPosition, i]);
                }
                break;
        }
        return returnFaces;
        

    }

    public GameObject createMiddleBlock(int rotationType, int arrPosition, GameObject[,] middleBlocks, GameObject[,] positionBlocks )
    {
        GameObject fakeMiddle = new GameObject();
        Vector3 pos = middleBlocks[faceSize / 2, faceSize / 2].transform.position;
        switch (rotationType)
        {
            default:
            case 0: // Front/Back Turn Equivalent
                {
                    pos.x = positionBlocks[0, arrPosition].transform.position.x;
                }
                break;
            case 1: // Left/Right Turn Equivalent
                {
                    pos.z = positionBlocks[0, arrPosition].transform.position.z;
                }
                break;
            case 2: // Top/bottom turn Equivalent
                {
                    pos.y = positionBlocks[arrPosition, 0].transform.position.y;
                }
                break;

        }
        fakeMiddle.transform.position = pos;
        return fakeMiddle;
    }








}
