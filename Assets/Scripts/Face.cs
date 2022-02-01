using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class Face 
{
    public GameObject middleCube { get; set; }
    public Vector3 rotationDirection { get; set; }
    public GameObject[,] faceBlocks { get; set; }
    public char[,] colorBlocks { get; set; }
    public int clockWise { get; set; }
    /* Half of the cubes store the cubes of their face from bottom left to top right. If they store from bottom right to top left
       that means that swapped cube storage will be true */
    public bool swappedCubeStorage { get; set; }
    public bool isActive = true;
    public Face[] relevantFaces; // Faces that are needed to be referenced when swapping
    private int faceSize;
    private int arrXPos;
    private int arrYPos;
    public int dir { get; set; }

    

    public Face(int faceSize, Vector3 rotationDirection, int clockWise, bool swappedCubeStorage,int dir)
    {
        this.faceSize = faceSize;
        this.rotationDirection = rotationDirection;
        this.faceBlocks = new GameObject[faceSize,faceSize];
        this.colorBlocks = new char[faceSize, faceSize];
        this.arrXPos = swappedCubeStorage ? faceSize-1 : 0;
        this.arrYPos = faceSize-1;
        this.clockWise = clockWise;
        this.swappedCubeStorage = swappedCubeStorage;
        this.dir = dir;
        this.middleCube = null;
        
    }

    public void addToArray(GameObject cube)
    {
        if (arrXPos >= faceSize || arrYPos >= faceSize) // This shouldn't happen, but just in case index gets out of bounds
        {
            Debug.Log("Add Index out of Bounds");
            return;
        }

        faceBlocks[arrYPos,arrXPos] = cube;
        arrXPos += swappedCubeStorage ? -1 : 1;
        if(arrXPos == -1 || arrXPos == faceSize)
        {
            arrXPos = swappedCubeStorage ? faceSize-1 : 0;
            --arrYPos;
        }
    }

    public void setColor(bool nameChange)
    {
        isActive = !isActive;
        for(int i = 0, b = 0; i < faceSize; ++i)
        {
            for(int a = 0; a < faceSize; ++a)
            {
                faceBlocks[i, a].gameObject.SetActive(isActive);
                if(nameChange)
                {
                    faceBlocks[i, a].transform.name = b++ + "";

                }
            }
        }
    }

    /*public void adjustArrayPostRotation()
    {
        int layerAmount = faceSize / 2; // How many layers need to be adjusted
        for(int i = 0; i < layerAmount; ++i)
        {

            // Swap Corners
            GameObject storage = faceBlocks[i, i];
            faceBlocks[i, i] = faceBlocks[faceSize - 1-i,i]; // Set TL corner to BL corner
            faceBlocks[faceSize-1-i, i] = faceBlocks[faceSize-1-i,faceSize-1-i]; // Set BL corner to BR corner
            faceBlocks[faceSize - 1-i, faceSize - 1-i] = faceBlocks[i, faceSize - 1-i]; // Set BR corner to TR corner
            faceBlocks[i, faceSize - 1-i] = storage; // Set TR corner to TL corner

            char charStorage = colorBlocks[i, i];
            colorBlocks[i, i] = colorBlocks[faceSize - 1 - i, i]; // Set TL corner to BL corner
            colorBlocks[faceSize - 1 - i, i] = colorBlocks[faceSize - 1 - i, faceSize - 1 - i]; // Set BL corner to BR corner
            colorBlocks[faceSize - 1 - i, faceSize - 1 - i] = colorBlocks[i, faceSize - 1 - i]; // Set BR corner to TR corner
            colorBlocks[i, faceSize - 1 - i] = charStorage; // Set TR corner to TL corner

            // Swap Middle Blocks
            for (int a = 1+i; a < faceSize - 1; ++a)
            {
                storage = faceBlocks[i, a];
                faceBlocks[i, a] = faceBlocks[faceSize - 1 - a, i];
                faceBlocks[faceSize - 1 - a, i] = faceBlocks[faceSize - 1-i, faceSize - 1 - a];
                faceBlocks[faceSize - 1-i, faceSize - 1 - a] = faceBlocks[a, faceSize - 1-i];
                faceBlocks[a, faceSize - 1-i] = storage;

                charStorage = colorBlocks[i, a];
                colorBlocks[i, a] = colorBlocks[faceSize - 1 - a, i];
                colorBlocks[faceSize - 1 - a, i] = colorBlocks[faceSize - 1 - i, faceSize - 1 - a];
                colorBlocks[faceSize - 1 - i, faceSize - 1 - a] = colorBlocks[a, faceSize - 1 - i];
                colorBlocks[a, faceSize - 1 - i] = charStorage;
            }
        }
    }*/

    public void adjustArrayPostRotation()
    {
        // Rotate face blocks
        rotateArray<GameObject>(faceBlocks);

        // Rotate color blocks
        rotateArray<char>(colorBlocks);
    }
        
    public void rotateArray<T>(T[,] arr)  // Generic array rotation. Rotates any passed array by 90 degrees
    {

        int layerAmount = faceSize / 2; // How many layers need to be adjusted
        for (int i = 0; i < layerAmount; ++i)
        {

            // Swap Corners
            T storage = arr[i, i];
            arr[i, i] = arr[faceSize - 1 - i, i]; // Set TL corner to BL corner
            arr[faceSize - 1 - i, i] = arr[faceSize - 1 - i, faceSize - 1 - i]; // Set BL corner to BR corner
            arr[faceSize - 1 - i, faceSize - 1 - i] = arr[i, faceSize - 1 - i]; // Set BR corner to TR corner
            arr[i, faceSize - 1 - i] = storage; // Set TR corner to TL corner


            // Swap Middle Blocks
            for (int a = 1 + i; a < faceSize - 1; ++a)
            {
                storage = arr[i, a];
                arr[i, a] = arr[faceSize - 1 - a, i];
                arr[faceSize - 1 - a, i] = arr[faceSize - 1 - i, faceSize - 1 - a];
                arr[faceSize - 1 - i, faceSize - 1 - a] = arr[a, faceSize - 1 - i];
                arr[a, faceSize - 1 - i] = storage;

               
            }
        }
    }

    public void printArray()
    {
        // Function for debugging
        string printStr = dir + "\n";
        for(int i = 0; i < faceSize; ++i)
        {
            for(int a = 0; a < faceSize; ++a)
            {
                printStr += faceBlocks[i, a].transform.name + ",";
            }
            printStr += "\n";
        }
        Debug.Log(printStr);
    }

    /*public void adjustFacesPostRotation()
    {
        // Top & Bottom
        GameObject[,] topBlocks = relevantFaces[0].faceBlocks;
        GameObject[,] botBlocks = relevantFaces[2].faceBlocks;
        GameObject[,] leftBlocks = relevantFaces[3].faceBlocks;
        GameObject[,] rightBlocks = relevantFaces[1].faceBlocks;
       
        switch(dir)
        {
            case 0: // Bottom
                for (int i = 0; i < faceSize; ++i)
                {
                    topBlocks[faceSize-1, i] = faceBlocks[faceSize-1, faceSize-1-i];
                    botBlocks[faceSize-1, i] = faceBlocks[0, i];
                    leftBlocks[faceSize-1,i] = faceBlocks[faceSize-1-i,0];
                    rightBlocks[faceSize-1, i] = faceBlocks[i,faceSize-1]; // L
                }
                break;
                
            case 5: // Top
                for (int i = 0; i < faceSize; ++i)
                {
                    topBlocks[0, i] = faceBlocks[0, faceSize-1-i];
                    botBlocks[0, i] = faceBlocks[faceSize-1, i];
                    leftBlocks[0, i] = faceBlocks[faceSize-1-i,faceSize-1];
                    rightBlocks[0,i] = faceBlocks[i, 0];
                }
                break;
            case 3: // Front
                for (int i = 0; i < faceSize; ++i)
                {
                    topBlocks[faceSize-1, i] = faceBlocks[0, i];
                    botBlocks[faceSize-1, i] = faceBlocks[faceSize-1,faceSize-1-i];
                    leftBlocks[i, 0] = faceBlocks[i, faceSize-1];
                    rightBlocks[i, faceSize-1] = faceBlocks[i,0];
                }
                break;
            case 1: // Back
                for (int i = 0; i < faceSize; ++i)
                {
                    topBlocks[0, faceSize-1-i] = faceBlocks[faceSize-1, faceSize-1-i];
                    botBlocks[0, i] = faceBlocks[0,faceSize-1-i];
                    leftBlocks[i, faceSize-1] = faceBlocks[i,0];
                    rightBlocks[i,0] = faceBlocks[i, faceSize-1]; // L
                }
                break;
            case 2: // Right
                for (int i = 0; i < faceSize; ++i)
                {
                    leftBlocks[i,0] = faceBlocks[i, faceSize-1];
                    rightBlocks[i, faceSize-1] = faceBlocks[i,0];
                    topBlocks[i,0] = faceBlocks[0,i];
                    botBlocks[i,faceSize-1] = faceBlocks[faceSize-1, i]; // L
                }
                break;
            case 4: // Left
                for (int i = 0; i < faceSize; ++i)
                {
                    leftBlocks[i, 0] = faceBlocks[i, faceSize-1];
                    rightBlocks[i,faceSize-1] = faceBlocks[i, 0];
                    topBlocks[i, faceSize-1] = faceBlocks[0,faceSize-1-i];
                    botBlocks[i, 0] = faceBlocks[faceSize-1,faceSize-1-i];
                }
                break;
            default:
                break;

        }
    }*/
    public void adjustFacesPostRotation() // Helper function
    {
        // Adjust cube array
        adjustFacesPostRotation<GameObject>(relevantFaces[0].faceBlocks, relevantFaces[2].faceBlocks, relevantFaces[3].faceBlocks, relevantFaces[1].faceBlocks, faceBlocks);
        // Adjust color array
        adjustFacesPostRotation<char>(relevantFaces[0].colorBlocks, relevantFaces[2].colorBlocks, relevantFaces[3].colorBlocks, relevantFaces[1].colorBlocks,colorBlocks);
    }
    public void adjustFacesPostRotation<T>(T[,] topBlocks, T[,] botBlocks, T[,] leftBlocks, T[,] rightBlocks, T[,] currFace)
    {
        /* Hard coded method to keep track of which faces contains which cubes. 
         * Each face interacts with every other face except the face that is across from itself
         * Each face interacts slightly differently with every other face because of how I coded it, so each face needs a special case for how to adjust cubes
        */

        switch (dir)
        {
            case 0: // Bottom
                for (int i = 0; i < faceSize; ++i)
                {
                    topBlocks[faceSize - 1, i] = currFace[faceSize - 1, faceSize - 1 - i];
                    botBlocks[faceSize - 1, i] = currFace[0, i];
                    leftBlocks[faceSize - 1, i] = currFace[faceSize - 1 - i, 0];
                    rightBlocks[faceSize - 1, i] = currFace[i, faceSize - 1]; // L
                }
                break;

            case 5: // Top
                for (int i = 0; i < faceSize; ++i)
                {
                    topBlocks[0, i] = currFace[0, faceSize - 1 - i];
                    botBlocks[0, i] = currFace[faceSize - 1, i];
                    leftBlocks[0, i] = currFace[faceSize - 1 - i, faceSize - 1];
                    rightBlocks[0, i] = currFace[i, 0];
                }
                break;
            case 3: // Front
                for (int i = 0; i < faceSize; ++i)
                {
                    topBlocks[faceSize - 1, i] = currFace[0, i];
                    botBlocks[faceSize - 1, i] = currFace[faceSize - 1, faceSize - 1 - i];
                    leftBlocks[i, 0] = currFace[i, faceSize - 1];
                    rightBlocks[i, faceSize - 1] = currFace[i, 0];
                }
                break;
            case 1: // Back
                for (int i = 0; i < faceSize; ++i)
                {
                    topBlocks[0, faceSize - 1 - i] = currFace[faceSize - 1, faceSize - 1 - i];
                    botBlocks[0, i] = currFace[0, faceSize - 1 - i];
                    leftBlocks[i, faceSize - 1] = currFace[i, 0];
                    rightBlocks[i, 0] = currFace[i, faceSize - 1]; // L
                }
                break;
            case 2: // Right
                for (int i = 0; i < faceSize; ++i)
                {
                    leftBlocks[i, 0] = currFace[i, faceSize - 1];
                    rightBlocks[i, faceSize - 1] = currFace[i, 0];
                    topBlocks[i, 0] = currFace[0, i];
                    botBlocks[i, faceSize - 1] = currFace[faceSize - 1, i]; // L
                }
                break;
            case 4: // Left
                for (int i = 0; i < faceSize; ++i)
                {
                    leftBlocks[i, 0] = currFace[i, faceSize - 1];
                    rightBlocks[i, faceSize - 1] = currFace[i, 0];
                    topBlocks[i, faceSize - 1] = currFace[0, faceSize - 1 - i];
                    botBlocks[i, 0] = currFace[faceSize - 1, faceSize - 1 - i];
                }
                break;
            default:
                break;

        }
    }

    





}
