﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class consoleController : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text consoleLog;
    public ScrollRect scrollBar;
    public TMP_Text statsTxt;
    public Sprite arrow;

    int dmg; //buffer for damage function so its called less

    string userLine = "/";
    string responceLine = ">> ";

    int delay = PlayerPrefs.GetInt("delay");

    bool playerTurn;
    int stage = 0;


    List<string> commands = new List<string>() { // list of all valid commands
        "blue", //this is a test command
        "red", //this is a test command
        "pp", //this is a test command
        "echo", //this is a test command
        "attack",
        "health"};

    public class Character // the class all enemies and the playes are based off
    {
        public int burn; //burn damge
        public int burnD; // burn duration
        public int burnS; // burn stacks 

        public int baseD; // base damage 
        public float baseC; // crit chance
        public float crit; // bonus crit chance

        public int hp; // the hp

        public Character(int bur, int burD, int burS, int basD, float basC, float cri, int HP)
        {
            burn = bur;
            burnD = burD;
            burnS = burS;
            baseD = basD;
            baseC = basC;
            crit = cri;
            hp = HP;
        }
    }

    public Character player = new Character(3, 0, 0, 10, 0.05f, 0f, 100); // creates the player with all stats
    public Character enemy = new Character(2, 0, 0, 7, 0f, 0f, 75); // cretaes a basic enemy
    public Character boss = new Character(3, 0, 0, 10, 0.05f, 0f, 150); // creates a boss enemy

    private void Start()
    {
        Vector3 theScale = GameObject.Find("arrow").transform.localScale;
        theScale.x *= -1;
        GameObject.Find("arrow").transform.localScale = theScale;
    }

    int damageCalc(Character name)//calculates the damage done by any enemy
    {
        int FD; // final damage before crit and other effects
        if (name.burnD > 0)
            FD = name.baseD + name.burn * name.burnS;
        else
            FD = name.baseD;

        if (NextFloat(0, 1) < name.baseC + name.crit)
            return FD * 2;
        else
            return FD;
    }

    void deathCheck() // called when damage is taken by anyone to check their health and act accordingly
    {
        if (player.hp <= 0)
            responce(false, "you died", "red");
        else if (enemy.hp <= 0)
        {
            responce(false, "you killed an enemy", "green");
            stage++;
        }
        else if (boss.hp <= 0)
        {
            responce(false, "you killed a boss", "green");
            stage++;
        }
    }

    public void showText()
    {
        if (playerTurn) // turn swapper
        {
            lineParser();

            scrollBar.verticalNormalizedPosition = 0f; //scroll to the botom of the text box
            Canvas.ForceUpdateCanvases();
            inputField.ActivateInputField();    // activates the inputfield
            inputField.text = "";

            playerTurn = false;
        }
        else if (!playerTurn)
        {

            inputField.ActivateInputField();
            inputField.text = "";
            playerTurn = true;
        }
    }

    private void responce(bool user, string text, string color) // used for printing to the log
    {
        if (user)
            consoleLog.text = consoleLog.text + "\n" + userLine + string.Format("<color={0}>" + text + "</color>", color);
        else if (!user)
            consoleLog.text = consoleLog.text + "\n" + responceLine + string.Format("<color={0}>" + text + "</color>", color);

    }

    public void lineParser()
    {
        // add ff to the end of all hex colours to account for opacity
        List<string> tokens = new List<string>(inputField.text.Split(' '));

        if (commands.Contains(tokens[0]))
        {
            switch (tokens[0])
            {
                case "blue":
                    GameObject.Find("Enemy").GetComponent<enemyController>().EnemyUpdate(tokens[0]);
                    responce(true, tokens[0], "blue");
                    break;
                case "red":
                    GameObject.Find("Enemy").GetComponent<enemyController>().EnemyUpdate(tokens[0]);
                    responce(true, tokens[0], "red");
                    break;
                case "pp":
                    responce(true, tokens[0], "#ff00ffff");
                    responce(false, "haha verry funny", "purple");
                    break;
                case "echo":
                    responce(true, tokens[0], "#00ff00ff");
                    responce(false, tokens[1], "#008000ff");
                    break;
                case "attack":
                    responce(true, tokens[0], "red");
                    dmg = damageCalc(player);
                    responce(true, "You did " + dmg + " damage!", "yellow");
                    enemy.hp = enemy.hp - dmg;
                    deathCheck();
                    break;
                case "health":
                    responce(true, tokens[0], "blue");
                    responce(false, "Your health: " + player.hp, "green");
                    responce(false, "Enemy health: " + enemy.hp, "red");
                    break;

            }
            Debug.Log(inputField.text);
        }
    }

    static float NextFloat(float min, float max) // random double (https://www.codegrepper.com/code-examples/csharp/c%23+random+float+between+two+numbers)
    {
        System.Random random = new System.Random();
        double val = (random.NextDouble() * (max - min) + min);
        return (float)val;
    }

    private void Update() // used for updating stats
    {
        string thisispainfixthis = "Health: " + player.hp.ToString() + "\n" +
            "Base Damage: " + player.baseD.ToString() + "\n" +
            "Crit chance: " + ((player.crit + player.baseC)*100).ToString() + "%\n" +
            "Burn damage: " + player.burn.ToString();

        statsTxt.text = thisispainfixthis;
    }
}
