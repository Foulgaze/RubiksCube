using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Button submitBtn;
    public InputField faceSizeInput;
    void Start()
    {
        faceSizeInput.contentType = InputField.ContentType.IntegerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
