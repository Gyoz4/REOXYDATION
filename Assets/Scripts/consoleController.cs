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
    public ScrollRect scrollBar;

    void Update()
    {
        
    }

    public void showText()
    {
        command();

        scrollBar.verticalNormalizedPosition = 0f; //scroll to the botom of the text box
        Canvas.ForceUpdateCanvases();

        inputField.ActivateInputField();    // activates the inputfield

        inputField.text = "";
    }

    public void command()
    {
        if (inputField.text != "")
        {
            //consoleLog.text = consoleLog.text + "\n>> " + inputField.text;

            switch (inputField.text)
            {
                case "blue":
                    consoleLog.text = consoleLog.text + "\n>> " + string.Format("<color=blue>{0}</color>", inputField.text);
                    break;
            }
        }
    }
}
