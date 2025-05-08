using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputDisplayController : MonoBehaviour
{
    // Start is called before the first frame update

    //This scripts is attached to a Canvas object in the scene with a Text component
    //This is to print the keyboard input values to the screen

    GameObject textObject = null;
    String newInputString = "";
    String inputString = "";




    void Start()
    {
        textObject = GameObject.Find("InputText");
        textObject.GetComponent<UnityEngine.UI.Text>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        //textObject.GetComponent<UnityEngine.UI.Text>().text = "Input: " + Input.inputString;
        textObject.GetComponent<UnityEngine.UI.Text>().text = "Input: " + inputString;
        //This is to print the keyboard input values to the screen
        if (Input.anyKeyDown)
        {
            //If the key is Space the print 'Space' to the screen
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UpdateInputString("Space");
            } else if (Input.GetKeyDown(KeyCode.Return))
            {
                UpdateInputString("Return");
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                UpdateInputString("Escape");
            }
            else if (Input.GetKeyDown(KeyCode.Backspace))
            {
                UpdateInputString("Backspace");
            }
            else if (Input.GetKeyDown(KeyCode.Delete))
            {
                UpdateInputString("Delete");
            }
            else if (Input.GetKeyDown(KeyCode.Tab))
            {
                UpdateInputString("Tab");
            }
            else if (Input.GetMouseButtonDown(0))
            {
                UpdateInputString("Mouse Button 0");
            }
            else if (Input.GetMouseButtonDown(1))
            {
                UpdateInputString("Mouse Button 1");
            }
            else if (Input.GetMouseButtonDown(2))
            {
                UpdateInputString("Mouse Button 2");
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                UpdateInputString("Left Shift");
            }
            else if (Input.GetKeyDown(KeyCode.RightShift))
            {
                UpdateInputString("Right Shift");
            }
            else
            {
                //This is to print the keyboard input values to the screen
                UpdateInputString(Input.inputString);
            }

            //UpdateInputString(Input.inputString);
        } 
        
        



    }

    void UpdateInputString(String input)
    {
        //This is to print the keyboard input values to the screen
        newInputString = input;
        if (newInputString != inputString)
        {
            inputString = newInputString;
        }
    }
}






