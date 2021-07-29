using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class itemManager : MonoBehaviour {

    static float commonRate = 0.30f;
    static float unCommonRate = 0.25f;
    static float rareRate = 0.20f;
    static float epicRate = 0.12f;
    static float legendaryRate = 0.08f;
    static float mythicRate = 0.05f;
    public TMP_Text itemtest;

    int i = (int)NextFloat(1, 5);

    float dropRate;

    List<string> items = new List<string>() { };

    List<string> common = new List<string>() {
        "01",
        "02",
        "03",
        "04",
        "06",
        "07"};

    List<string> unCommon = new List<string>() {
        "09",
        "10",
        "11",
        "12",
        "13",
        "14"};

    List<string> rare = new List<string>() {
        "18"};

    List<string> ebic = new List<string>() {
        "19",
        "20",
        "21",
        "22",
        "23"};

    List<string> legendary = new List<string>() {
        "25",
        "26",
        "27",
        "28"};

    List<string> mythic = new List<string>() {
        "29",
        "30"};

    public void Quality() {
        Debug.Log("------------------------------------------------------");
        dropRate = NextFloat(0, 1);

        switch (true) {
            case bool expression when dropRate < 0.05f:
                Debug.Log("mythic");
                i = (int)NextFloat(0, 1);
                items.Add(mythic[i]);
                break;
            case bool expression when dropRate < 0.08f:
                Debug.Log("legendary");
                i = (int)NextFloat(0, 3);
                items.Add(legendary[i]);
                break;
            case bool expression when dropRate < 0.12f:
                Debug.Log("ebic");
                i = (int)NextFloat(0, 4);
                items.Add(ebic[i]);
                break;
            case bool expression when dropRate < 0.20f:
                Debug.Log("rare");
                items.Add(rare[0]);
                break;
            case bool expression when dropRate < 0.25f:
                Debug.Log("uncommon");
                i = (int)NextFloat(0, 5);
                items.Add(unCommon[i]);
                break;
            case bool expression when dropRate > 0.25f:
                Debug.Log("common");
                i = (int)NextFloat(0, 5);
                items.Add(common[i]);
                break;
        }
    }

    static float NextFloat(float min, float max) { // random double (https://www.codegrepper.com/code-examples/csharp/c%23+random+float+between+two+numbers)
        System.Random random = new System.Random();
        double val = (random.NextDouble() * (max - min) + min);
        return (float)val;
    }

    private void Update() {
        itemtest.text = "";
        foreach (string a in items) {
            itemtest.text = itemtest.text += a + " ";
        }

    }

    public void CheckItem() {
        switch (true) {
            //common
            case bool expression when items.Contains("01") == true:
                break;
            case bool expression when items.Contains("02") == true:
                break;
            case bool expression when items.Contains("03") == true:
                break;
            case bool expression when items.Contains("04") == true:
                break;
            case bool expression when items.Contains("06") == true:
                break;
            case bool expression when items.Contains("07") == true:
                break;
            //uncommon
            case bool expression when items.Contains("09") == true:
                break;
            case bool expression when items.Contains("10") == true:
                break;
            case bool expression when items.Contains("11") == true:
                break;
            case bool expression when items.Contains("12") == true:
                break;
            case bool expression when items.Contains("13") == true:
                break;
            case bool expression when items.Contains("14") == true:
                break;
            //rare
            case bool expression when items.Contains("18") == true:
                break;
            //ebic
            case bool expression when items.Contains("19") == true:
                break;
            case bool expression when items.Contains("20") == true:
                break;
            case bool expression when items.Contains("21") == true:
                break;
            case bool expression when items.Contains("22") == true:
                break;
            case bool expression when items.Contains("23") == true:
                break;
            //legendary
            case bool expression when items.Contains("25") == true:
                break;
            case bool expression when items.Contains("26") == true:
                break;
            case bool expression when items.Contains("27") == true:
                break;
            case bool expression when items.Contains("28") == true:
                break;
            //mythic
            case bool expression when items.Contains("29") == true:
                break;
            case bool expression when items.Contains("30") == true:
                break;

        }
    }
}
