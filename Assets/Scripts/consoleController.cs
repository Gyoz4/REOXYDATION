using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;

public class consoleController : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text consoleLog;
    public ScrollRect scrollBar;
    public TMP_Text statsTxt;
    public TMP_Text enemyStatsTxt;
    public Sprite arrow;

    int dmg; //buffer for damage function so its called less

    string userLine = "/"; // the line start determining the player input
    string responceLine = ">> "; // the line end determining the game output

    public static int stage = 0;

    int milk = 0; //  how many stask of milk have been applied
    int steak = 0; //  how many stask of steak have been applied


    public GameObject myItemmanager;
    public GameObject canvas;

    string thisispainfixthis2;

    private bool admin; 

    public static bool finished; //  when win the game 
    public static bool dead; //  when die 

    System.Random rand = new System.Random();

    List<string> commands = new List<string>() { // list of all valid commands
        "echo", //this is a test command, but seems fitting so its still here
        "attack",
        "health",
        "quality",
        "wait", 
        "item", 
        "give",
        "pass"};//  the command used to get admin
    public class Characters { // the class all enemies and the playes are based off
        public int bleed; //bleed damge
        public int bleedD; // bleed duration
        public int bleedS; // bleed stacks 
        public float bleedC; //bleed chance

        public int baseD; // base damage 
        public float baseC; // crit chance
        public float crit; // bonus crit chance

        public int hp; // the hp
        public int maxHp; // max hp

        public bool stun; // if enemy is stunned
        public float ms; // moove speed/dodge chance

        public Characters(int blee, int bleeD, int bleeS, float bleeC,  int basD, float basC, float cri, int HP, int MHP, bool stu, float move) {
            bleed = blee;
            bleedD = bleeD;
            bleedS = bleeS;
            bleedC = bleeC;
            baseD = basD;
            baseC = basC;
            crit = cri;
            hp = HP;
            maxHp = MHP;
            stun = stu;
            ms = move;
        }
    }

    public static Characters player = new Characters(3, 0, 0, 0, 10, 0.05f, 0f, 100, 100, false, 0); //  creates the player with all stats
    public static Characters enemy = new Characters(2, 0, 0, 0, 7, 0f, 0f, 50, 75, false, 0); //  cretaes a basic enemy
    public static Characters bossE = new Characters(3, 0, 0, 0, 10, 0.05f, 0f, 125, 125, false, 0); //  creates a boss enemy


    private void Start() { // selects the inout pox automaticaly and gets rid of "unknown command" from pressing the play button
        showText();
        consoleLog.text = ">> echo [string], attack, health, wait, item [number]";
    }

    int PlayerDamageCalc() { //calculates the damage done by any enemy
        double FD; //  final damage
        double healing = 0; //  the ammount of healing this turn

        bool critSu = false; //  crit success
        bool bleedSu = false; //  bleed seccess 
        bool stunSu = false; // stun success

        if (enemy.bleedS > 0) { //  bleed damage
            FD = player.baseD + player.bleed * enemy.bleedS;
            enemy.bleedD--;
        }
        else
            FD = player.baseD;

        if (player.baseC + player.crit >= rand.NextDouble()) { //  crit
            FD = FD * 2;
            healing += Convert.ToInt32(FD * 0.5 * itemManager.items.Where(s => s != null && s.StartsWith("10")).Count());
            critSu = true;

            if (itemManager.items.Contains("23")) { //  crit bleed
                enemy.bleedS += 1 * itemManager.items.Where(s => s != null && s.StartsWith("23")).Count();
                enemy.bleedD = 3;
            }
        }

        if (itemManager.items.Contains("03") && enemy.hp == 50 | enemy.hp == 125 | enemy.hp == 75)// bonus to full hp
            FD = FD + FD * 0.5;

        if (player.bleedC <= rand.NextDouble()) {// bleed stacks
            enemy.bleedS += 1;
            enemy.bleedD = 3;
            bleedSu = true;
        }

        if (stage % 3 == 0 && itemManager.items.Contains("03")) // more damage to bosses
            FD += 5;

        if (0.1 * itemManager.items.Where(s => s != null && s.StartsWith("18")).Count() >= rand.NextDouble()) // basicaly another crit chance
            FD = FD * 1.25;

        if (FD > 20 && itemManager.items.Contains("21")) { // band a, more damage and heal
            FD = FD * 1.25;
            healing += Convert.ToInt32(FD * 0.5);
        } 
        if (FD > 20 && itemManager.items.Contains("22")) { // band b, more damage and bleed
            FD = FD * 1.25;
            enemy.bleedS += 1;
            enemy.bleedD += 3;
        }

        if (itemManager.items.Contains("09")) { //stun chance
            float stun = 0.1f * itemManager.items.Where(s => s != null && s.StartsWith("09")).Count(); //from https://stackoverflow.com/questions/12911931/finding-count-of-particular-items-from-a-list
            if (stun >= rand.NextDouble()) {
                enemy.stun = true;
                bossE.stun = true;
                responce(false, "You stunned the enemy", "yellow");
            }
            else {
                enemy.stun = false;
                bossE.stun = false;
            }
            stunSu = true;
        }

        if (itemManager.items.Contains("26")) { // +1 rolls
            if (player.bleedC <= rand.NextDouble() && !bleedSu) {// bleed reroll
                enemy.bleedS += 1;
                enemy.bleedD = 3;
                bleedSu = true;
            }

            if (player.baseC + player.crit >= rand.NextDouble() && !critSu) { //  crit reroll
                FD = FD * 2;
                healing += Convert.ToInt32(FD * 0.5 * itemManager.items.Where(s => s != null && s.StartsWith("10")).Count());
                critSu = true;

                if (itemManager.items.Contains("23")) { //  crit bleed
                    enemy.bleedS += 1 * itemManager.items.Where(s => s != null && s.StartsWith("23")).Count();
                    enemy.bleedD = 3;
                }
            }

            if (itemManager.items.Contains("09") && !stunSu) { //stun reroll
                float stun = 0.1f * itemManager.items.Where(s => s != null && s.StartsWith("09")).Count();
                if (stun >= rand.NextDouble()) {
                    enemy.stun = true;
                    bossE.stun = true;
                    responce(false, "You stunned the enemy", "yellow");
                }
                stunSu = true;
            }
        }

        return Convert.ToInt32(FD); // return final damage dealt by the player in this turn, int because noone wants to see decimals in health

        if (itemManager.items.Contains("28")) //  double helaing
            player.hp += Convert.ToInt32(2 * healing);
        else
            player.hp += Convert.ToInt32(healing);

        if (player.hp > player.maxHp) // gets rid off overheal
            player.hp = player.maxHp;
    }

    int enemyDamageCalc(bool boss) { //  calculates the damage dealt by the enemy this turn
        double eFD = 0; //  enemy final damage
        if (boss) { //  if enemy is boss can crit
            if (rand.NextDouble() <= bossE.baseD)
                eFD = 2 * bossE.baseD;
            eFD = bossE.baseD;
        }
        else //  normal enemy
            eFD = enemy.baseD;

        if (itemManager.items.Contains("04")) //  reduce damage by one for evry 04 owned
            eFD -= itemManager.items.Where(s => s != null && s.StartsWith("04")).Count();
        if (itemManager.items.Contains("13")) { //  thorns
            if (boss)
                bossE.hp -= itemManager.items.Where(s => s != null && s.StartsWith("13")).Count();
            else
                enemy.hp -= itemManager.items.Where(s => s != null && s.StartsWith("04")).Count();
        }

        if (itemManager.items.Contains("14")) {
            if (player.ms >= rand.NextDouble())
                eFD = 0;
        }
        if (bossE.stun | enemy.stun) {
            enemy.stun = false;
            bossE.stun = false;
            eFD = 0;
        }

        return Convert.ToInt32(eFD);
    }

    void deathCheck() { // called when damage is taken by anyone to check their health and act accordingly
        if (player.hp <= 0 && !itemManager.items.Contains("30") && !itemManager.items.Contains("25")) {
            responce(false, "you died", "red");
            die();
        }
        else if (player.hp <= 0 && itemManager.items.Contains("25")) {
            itemManager.items.Remove("25");
            responce(false, "beacon activated", "green");
        }
        else if (player.hp <= 0 && itemManager.items.Contains("30")) {
            itemManager.items.Remove("30");
            responce(false, "stone mask activated", "green");
        }
        else if (enemy.hp <= 0) {
            responce(false, "you killed an enemy", "green");
            player.hp += 10;
            enemy.hp = 75;
            if (stage == 0) { //  stage 0 enemy drops and extra 3 items for starter purposes
                for (int i = 0; i < 3; i++)
                    myItemmanager.GetComponent<itemManager>().Quality();
            }
            myItemmanager.GetComponent<itemManager>().Quality();//  normal enemy gives 1 item
            stage++;
            if (stage > 9)
                win();
        }
        else if (bossE.hp <= 0) { //  bosses get more health the further you go
            myItemmanager.GetComponent<itemManager>().Quality();

            responce(false, "you killed a boss", "green");
            player.hp += 20;

            if (stage == 9)
                bossE.hp = 250;
            else if (stage == 6)
                bossE.hp = 200;
            else if (stage == 3)
                bossE.hp = 150;
            else
                Debug.Log("this is bad, boss health broke");
            //  bosses scale crit and base damage
            bossE.baseC = 0.1f * stage;
            bossE.baseD += Convert.ToInt32(0.25 * bossE.baseD);

            myItemmanager.GetComponent<itemManager>().Quality();

            if (stage > 9)
                win();
            stage++;

        }
    }

    public void showText() {
        lineParser();
        scrollBar.verticalNormalizedPosition = 0f; //scroll to the botom of the text box
        Canvas.ForceUpdateCanvases();
        inputField.ActivateInputField();    // activates the inputfield
        inputField.text = "";
    }

    private void responce(bool user, string text, string color) { // used for printing to the log
        if (user)
            consoleLog.text = consoleLog.text + "\n" + userLine + string.Format("<color={0}>" + text + "</color>", color);
        else if (!user)
            consoleLog.text = consoleLog.text + "\n" + responceLine + string.Format("<color={0}>" + text + "</color>", color);

    }

    public void lineParser() {
        // add ff to the end of all hex colours to account for opacity
        List<string> tokens = new List<string>(inputField.text.Split(' '));

        if (commands.Contains(tokens[0])) { //  the switch through the commands
            switch (tokens[0]) {
                case "give":
                    if (admin)
                        itemManager.items.Add(tokens[1]);
                    else
                        responce(false, "access denied", "red");
                    break;
                case "pass":
                    if (tokens[1] == "tempPass01") //  in production this could be a way more random and longer pass so its hard to guess
                        admin = true;
                    break;
                case "echo":
                    responce(true, tokens[0], "#00ff00ff");
                    responce(false, tokens[1], "#008000ff");
                    break;
                case "attack":
                    responce(true, tokens[0], "red");
                    dmg = PlayerDamageCalc();
                    responce(false, "You did " + dmg + " damage!", "yellow");
                    if (stage % 3 == 0 && stage != 0)
                        bossE.hp -= dmg;
                    else
                        enemy.hp -= dmg;

                    enemyAttack();
                    deathCheck();
                    break;
                case "health":
                    responce(true, tokens[0], "blue");
                    responce(false, "Your health: " + player.hp, "green");
                    if (stage % 3 == 0)
                        responce(false, "Enemy health: " + bossE.hp, "red");
                    else 
                        responce(false, "Enemy health: " + enemy.hp, "red");
                    break;
                case "wait":
                    if (itemManager.items.Contains("12") == true) {
                        player.hp += Convert.ToInt32(player.maxHp * 0.05);
                        responce(false, "You wait, and seem to feel 5% better", "green");
                    }
                    else
                        responce(false, "you wait, nothing seems to happen", "green");
                    enemyAttack();
                    deathCheck();
                    break;
                case "item":
                    if (admin) {
                        responce(true, tokens[0] + " " + tokens[1], "green");
                        switch (tokens[1]) {
                            case "01":
                                responce(false, "Knif, 10% bleed chance per item", "blue");
                                break;
                            case "02":
                                responce(false, "Crit Lenses, 5% crit chance per item", "blue");
                                break;
                            case "03":
                                responce(false, "Bigger Bullet,+5 damage to bosses per item", "blue");
                                break;
                            case "04":
                                responce(false, "Armor, reduce damage by one per item", "blue");
                                break;
                            case "06":
                                responce(false, "Steak, +10 max hp per item", "blue");
                                break;
                            case "07":
                                responce(false, "Guilotine, 50 % more damage to full health enemies", "blue");
                                break;
                            case "09":
                                responce(false, "Stun Grenade, 10% stun chance per item", "blue");
                                break;
                            case "10":
                                responce(false, "Vampic Scepter, crit helas for 10% damage dealt", "blue");
                                break;
                            case "11":
                                responce(false, "Choccy Milk, 10%  max health per item", "blue");
                                break;
                            case "12":
                                responce(false, "Risky Fungus, no action heals for 5% max hp", "blue");
                                break;
                            case "13":
                                responce(false, "Thorns, Return 10% damage taken", "blue");
                                break;
                            case "14":
                                responce(false, "Pink Bull, 10% moove speed/dodge chance per item", "blue");
                                break;
                            case "18":
                                responce(false, "Penetrator, 10% chance per item to deal 25% damage again ", "blue");
                                break;
                            case "20":
                                responce(false, "Triumph, on kill heal 5% per item", "blue");
                                break;
                            case "21":
                                responce(false, "Band A, if done >20 dmg +50% damage and heal", "blue");
                                break;
                            case "22":
                                responce(false, "Band B, if done >20 dmg +50% damage and apply bleed)", "blue");
                                break;
                            case "23":
                                responce(false, "Red Filter, crits stack bleed", "blue");
                                break;
                            case "25":
                                responce(false, "Beacon, on death resurect with 50% hp", "blue");
                                break;
                            case "26":
                                responce(false, "More Leaf Clover, +1 rolls for favourable outcomes", "blue");
                                break;
                            case "28":
                                responce(false, "Holy Steak, Double helaing", "blue");
                                break;
                            case "30":
                                responce(false, "Stone Mask, on death resurect with 100% hp", "blue");
                                break;
                            default:
                                responce(false, "this item was either removed or doenst exist at all", "red");
                                break;
                        }
                        
                    }
                    break;
            }
        }
        else
            responce(false, "Unknow command", "red");
        saveGame();
    }

    private void Update() { // used for updating stats
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "gaem") {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (sceneName == "main menu") {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        string thisispainfixthis = "Health: " + player.hp.ToString() + "\n" + //  makes the text for the stats to be displayed for the player stats
            "Base damage: " + player.baseD.ToString() + "\n" +
            "Crit chance: " + ((player.crit + player.baseC)*100).ToString() + "%\n" +
            "Bleed chance: " + ((player.bleedC)*100).ToString() + "%\n" +
            "Stage: " + stage.ToString();

        statsTxt.text = thisispainfixthis; //  no not fixing, pain

        if (stage % 3 == 0 | stage == 10 && stage != 0) { //  makes the text for the stats to be displayed for the enemy stats
            thisispainfixthis2 = "Health: " + bossE.hp.ToString() + "\n" +
            "Base damage: " + bossE.baseD.ToString() + "\n" +
            "Crit chance: " + ((bossE.crit + bossE.baseC) * 100).ToString() + "%\n" +
            "Bleed chance: " + ((bossE.bleedC) * 100).ToString() + "%\n";
        }
        else
            thisispainfixthis2 = "Health: " + enemy.hp.ToString() + "\n" +
            "Base damage: " + enemy.baseD.ToString() + "\n";

        enemyStatsTxt.text = thisispainfixthis2;


        player.crit = itemManager.items.Where(s => s != null && s.StartsWith("02")).Count() * 0.05f; //  crit
        player.bleedC = itemManager.items.Where(s => s != null && s.StartsWith("01")).Count() * 0.1f; //  bleed
        player.ms = itemManager.items.Where(s => s != null && s.StartsWith("14")).Count() * 0.1f; //  moove speed
        
        while (milk < itemManager.items.Where(s => s != null && s.StartsWith("11")).Count()) { //  chalice
            milk++;
            player.maxHp += Convert.ToInt32(player.maxHp * 0.1 * itemManager.items.Where(s => s != null && s.StartsWith("11")).Count()); //  10% max hp
        }

        while (steak < itemManager.items.Where(s => s != null && s.StartsWith("06")).Count()) { //  chalice
            steak++;
            player.maxHp += itemManager.items.Where(s => s != null && s.StartsWith("06")).Count(); //  +10 max hp
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            saveGame();
            canvas.GetComponent<SceneChanger>().ChangeScene("main menu");//  on esc press changes back to main menu
        }
    }

    void OnApplicationQuit() { //  saves on exit
        Debug.Log("Application ending after " + Time.time + " seconds");
        saveGame();
    }

    void saveGame() { //  does the saving
        string combindedItems = string.Join(" ", itemManager.items); //  stores all items to a string so playerprefs can save it, separated by a comma so its easy to split when retrieving
        PlayerPrefs.SetString("saved items", combindedItems); //  does the saving bit

        PlayerPrefs.SetInt("saved stage", stage); //  does the saving bit
        PlayerPrefs.SetInt("saved player hp", player.hp); //  does the saving bit
        PlayerPrefs.SetInt("saved enemy hp", enemy.hp); //  does the saving bit
        PlayerPrefs.SetInt("saved boss hp", bossE.hp); //  does the saving bit
    }

    public void loadGame() { //  for loading the game
        itemManager.items.Clear();

        List<string> itemTmp = new List<string>() { }; //  temp list for looping over
        itemTmp = PlayerPrefs.GetString("saved items").Split(' ').ToList();

        for (int i =0; i < itemTmp.Count(); i++) //  the loop to save everything to the actual item list
            itemManager.items.Add(itemTmp[i]);
         //  gets the saved values
        stage = PlayerPrefs.GetInt("saved stage", stage); // the variable at the end is the default value
        player.hp =  PlayerPrefs.GetInt("saved player hp", player.hp);
        enemy.hp =  PlayerPrefs.GetInt("saved enemy hp", enemy.hp);
        bossE.hp =  PlayerPrefs.GetInt("saved boss hp", bossE.hp);
    }

    public void newGame() { //  resets everything to default values
        itemManager.items.RemoveRange(0, itemManager.items.Count());
        itemManager.items.Clear();

        stage = 0;
        player.hp = 100;
        steak = 0;
        milk = 0;
        player.maxHp = 100;
        enemy.hp = 50;
        bossE.hp = 125;

        finished = false;
    }

    void win() {
        newGame();

        canvas.GetComponent<SceneChanger>().ChangeScene("main menu");//  on esc press changes back to main menu
        
        finished = true;
    }
    void die() {
        newGame();

        canvas.GetComponent<SceneChanger>().ChangeScene("main menu");//  on esc press changes back to main menu

        dead = true;
    }

    void enemyAttack() {
        if (stage == 0) { //  the starting enemy has less health and is suposed to get players their starting items
            int enemyDMG = enemyDamageCalc(false);
            player.hp -= enemyDMG;
            responce(false, "You took " + enemyDMG + " damage from the enemy", "red");
        }
        else if (stage % 3 == 0) { //  boss enemy has a bit more health, damage and can crit
            int bossDMG = enemyDamageCalc(true);
            player.hp -= bossDMG;
            responce(false, "You took " + bossDMG + " damage from the boss", "red");
        }
        else if (stage % 3 != 0) { //  normal enemy, filler between bosses
            int enemyDMG = enemyDamageCalc(false);
            player.hp -= enemyDMG;
            responce(false, "You took " + enemyDMG + " damage from the enemy", "red");
        }
    }
}
