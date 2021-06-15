using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class consoleController : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text consoleLog;
    int i = 0;

    void Update()
    {
        inputField.ActivateInputField();    // activates the inputfield
    }

    public void sendit()
    {
        Debug.Log("get text " + i);
        getit();
        i++;
    }

    public void getit()
    {
        string txt = inputField.text;
        string log = consoleLog.text;
        string x = log + "\n>> " + txt;
        consoleLog.text = x;
        Debug.Log(txt);
    }
}
