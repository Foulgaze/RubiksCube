using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class uiController : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject turnSelection;
    public GameObject settingsMenu;
    public GameObject manager;
    public GameObject displayCube;
    public GameObject camera;
    faceManager fm;
    void Start()
    {
        fm = manager.GetComponent<faceManager>();
        turnSelection.SetActive(false);
        settingsMenu.SetActive(false);
        transform.position = new Vector3(0, 1, 8);
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void changeToCube()
    {

        string value = startMenu.transform.Find("InputField").GetComponent<InputField>().text;
     
        if (value != "")
        {
            if(Int32.Parse(value) >= 1)
            {
                fm.createCube(Int32.Parse(value));
                displayCube.SetActive(false);
                startMenu.SetActive(false);
                turnSelection.SetActive(true);
            }
            
        }


    }


    public void changeToMenu()
    {
        camera.transform.position = new Vector3(0, 1, 8);
        camera.transform.rotation = Quaternion.Euler(0, 180, 0);
        displayCube.SetActive(true);
        startMenu.SetActive(true);
        fm.destroyCube();
        turnSelection.SetActive(false);
    }

    public void changeToSettings()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
