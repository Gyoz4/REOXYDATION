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

    int playerHP = 100;
    int enemyHP = 100;


    string userLine = "/";
    string responceLine = ">> ";


    List<string> commands = new List<string>() {
        "blue",
        "red",
        "penis",
        "echo",
        "attack",
        "health"};

    public void showText()
    {
        lineParser();

        scrollBar.verticalNormalizedPosition = 0f; //scroll to the botom of the text box
        Canvas.ForceUpdateCanvases();

        inputField.ActivateInputField();    // activates the inputfield

        inputField.text = "";
    }

    public void lineParser()
    {
        // add ff to the end of all hex colours  to account for opacity
        List<string> tokens = new List<string>(inputField.text.Split(' '));

        if (commands.Contains(tokens[0]))
        {
            switch (tokens[0])
            {
                case "blue":
                    GameObject.Find("Enemy").GetComponent<enemyController>().EnemyUpdate(tokens[0]);
                    consoleLog.text = consoleLog.text + "\n" + userLine + string.Format("<color=blue>{0}</color>", tokens[0]);
                    break;
                case "red":
                    GameObject.Find("Enemy").GetComponent<enemyController>().EnemyUpdate(tokens[0]);
                    consoleLog.text = consoleLog.text + "\n" + userLine + string.Format("<color=red>{0}</color>", tokens[0]);
                    break;
                case "penis":
                    consoleLog.text = consoleLog.text + "\n" + userLine + string.Format("<color=#ff00ffff>{0}</color>", tokens[0]);
                    consoleLog.text = consoleLog.text + "\n" + responceLine + string.Format("<color=purple>haha very funny</color>");
                    break;
                case "echo":
                    consoleLog.text = consoleLog.text + "\n" + userLine + string.Format("<color=#00ff00ff>{0}</color>", tokens[0]);
                    consoleLog.text = consoleLog.text + "\n" + responceLine + string.Format("<color=#008000ff>{0}</color>", tokens[1]);
                    break;
                case "attack":
                    consoleLog.text = consoleLog.text + "\n" + userLine + string.Format("<color=red>{0}</color>", tokens[0]);
                    consoleLog.text = consoleLog.text + "\n" + responceLine + string.Format("<color=yellow>You did {0} damage</color>", 10);
                    damageCalc();
                    break;
                case "health":
                    consoleLog.text = consoleLog.text + "\n" + userLine + string.Format("<color=blue>{0}</color>", tokens[0]);
                    consoleLog.text = consoleLog.text + "\n" + responceLine + string.Format("<color=green>Your health is: {0}</color>", playerHP);
                    consoleLog.text = consoleLog.text + "\n" + responceLine + string.Format("<color=red>Enemy health is: {0}</color>", enemyHP);

                    break;

            }
            Debug.Log(inputField.text);
        }
    }

    void damageCalc()
    {
        int baseDMG = 5;
        double baseCritChance = 0.1f;
        int Fdamage;
        int burn;
    }
}
