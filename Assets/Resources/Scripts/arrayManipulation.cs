using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class arrayManipulation
{
   
    // Start is called before the first frame update
    public static HashSet<T> upwardTurn<T>(T[,] front, T[,] back, T[,] right, T[,] left, T[,] up, T[,] down, int arrPos, int faceSize)
    {
        HashSet<T> set = new HashSet<T>();

        for (int i = 0; i < faceSize; ++i)
        {
            T temp = left[arrPos, i];
            left[arrPos, i] = front[arrPos, i];
            front[arrPos, i] = right[arrPos, i];
            right[arrPos, i] = back[arrPos, i];
            back[arrPos, i] = temp;

            set.Add(left[arrPos, i]);
            set.Add(front[arrPos, i]);
            set.Add(right[arrPos, i]);
            set.Add(back[arrPos, i]);
        }

        return set;
    }

    public static HashSet<T> sidewardTurn<T>(T[,] front, T[,] back, T[,] right, T[,] left, T[,] up, T[,] down, int arrPos, int faceSize)
    {
        HashSet<T> set = new HashSet<T>(); 

        for (int i = 0; i < faceSize; ++i)
        {
            
            T temp = up[i, arrPos];
            up[i, arrPos] = front[i, arrPos];
            front[i, arrPos] = down[i, arrPos];
            down[i, arrPos] = back[faceSize - 1 - i, faceSize - 1 - arrPos];
            back[faceSize - 1 - i, faceSize - 1 - arrPos] = temp;

            set.Add(up[i, arrPos]);
            set.Add(front[i, arrPos]);
            set.Add(down[i, arrPos]);
            set.Add(back[faceSize - 1 - i, faceSize - 1 - arrPos]);
        }

        return set;
        
    }

    public static HashSet<T> frontwardsTurn<T>(T[,] front, T[,] back, T[,] right, T[,] left, T[,] up, T[,] down, int arrPos, int faceSize)
    {
        HashSet<T> set = new HashSet<T>();

        for (int i = 0; i < faceSize; ++i)
        {
            T temp = up[faceSize - 1 - arrPos, faceSize - 1 - i];
            up[faceSize -1 - arrPos, faceSize - 1 - i] = left[ i, faceSize - 1 - arrPos];
            left[ i, faceSize - 1 - arrPos] = down[arrPos,  i];
            down[arrPos, i] = right[faceSize - 1 - i, arrPos];
            right[faceSize - 1 - i, arrPos] = temp;

            set.Add(up[faceSize - 1 - arrPos, faceSize - 1 - i]);
            set.Add(left[ i, faceSize - 1 - arrPos]);
            set.Add(down[arrPos,  i]);
            set.Add(right[faceSize - 1 - i, arrPos]);
        }

        return set;
    }

    public static void rotateFace(cubeFace f, int faceSize, int rotateCount)
    {
        for(int i = 0; i < rotateCount; ++i)
        {
            rotateArray<GameObject>(f.blockGOs, faceSize);
            rotateArray<char>(f.blockCols, faceSize);
        }
        
    }

    public static void rotateArray<T>(T[,] arr, int faceSize)  // Generic array rotation. Rotates any passed array by 90 degrees
    {
        /*Debug.Log(faceSize);

        int layerAmount = faceSize / 2; // How many layers need to be adjusted
        for (int i = 0; i < layerAmount; ++i) // Change back to 0 if alpha
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
        }*/
        /*T[,] ret = new T[n, n];

        for (int i = 0; i < n; ++i)
        {
            for (int j = 0; j < n; ++j)
            {
                ret[i, j] = arr[n - j - 1, i];
            }
        }*/
        for (int i = 0; i < faceSize / 2; i++)
        {
            for (int j = i; j < faceSize - i - 1; j++)
            {

                // Swap elements of each cycle
                // in clockwise direction
                T temp = arr[i, j];
                arr[i, j] = arr[faceSize - 1 - j, i];
                arr[faceSize - 1 - j, i] = arr[faceSize - 1 - i, faceSize - 1 - j];
                arr[faceSize - 1 - i, faceSize - 1 - j] = arr[j, faceSize - 1 - i];
                arr[j, faceSize - 1 - i] = temp;
            }
        }


    }



}
