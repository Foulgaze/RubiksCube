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

    public void adjustFacePostRotation<T>(int rotationType, int arrPosition, T[,] frontBlocks, T[,] topBlocks, T[,] backBlocks, T[,] bottomBlocks)
    {

        switch (rotationType)
        {
            default:
            case 0: // Front/Back Turn Equivalent
                T[] tempArr = new T[faceSize];
                for (int i = 0; i < faceSize; ++i)
                {
                    tempArr[i] = frontBlocks[arrPosition,i];
                    frontBlocks[arrPosition, i] = bottomBlocks[i,arrPosition];
                    bottomBlocks[i, arrPosition] = backBlocks[i, arrPosition];
                    backBlocks[i, arrPosition] = topBlocks[i, arrPosition];
                    topBlocks[i, arrPosition] = tempArr[i];
                }

                break;
            case 1: // Left/Right Turn Equivalent
                break;
            case 2: // Top/bottom turn Equivalent
                break;
            
        }

    }






}
