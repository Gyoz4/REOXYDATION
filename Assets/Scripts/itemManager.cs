using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using System.Linq;


public class itemManager : MonoBehaviour {

    static float commonRate = 0.30f;
    static float unCommonRate = 0.25f;
    static float rareRate = 0.20f;
    static float epicRate = 0.12f;
    static float legendaryRate = 0.08f;
    static float mythicRate = 0.05f;
    public TMP_Text itemtest;

    int i;

    double dropRate;

    System.Random rand = new System.Random();

    public static List<string> items = new List<string>() { };

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
        "28"};

    List<string> mythic = new List<string>() {
        "29",
        "30"};

    public void Quality() {
        Debug.Log("------------------------------------------------------");
        dropRate = rand.NextDouble();

        switch (true) {
            case bool expression when dropRate < 0.05f:
                Debug.Log("mythic");
                i =  Convert.ToInt32(rand.NextDouble());
                items.Add(mythic[i]);
                break;
            case bool expression when dropRate < 0.08f:
                Debug.Log("legendary");
                i = Convert.ToInt32(GetRandomNumber(0, 3));
                items.Add(legendary[i]);
                break;
            case bool expression when dropRate < 0.12f:
                Debug.Log("ebic");
                i = Convert.ToInt32(GetRandomNumber(0, 4));
                items.Add(ebic[i]);
                break;
            case bool expression when dropRate < 0.20f:
                Debug.Log("rare");
                items.Add(rare[0]);
                break;
            case bool expression when dropRate < 0.25f:
                Debug.Log("uncommon");
                i = Convert.ToInt32(GetRandomNumber(0, 5));
                items.Add(unCommon[i]);
                break;
            case bool expression when dropRate > 0.25f:
                Debug.Log("common");
                i = Convert.ToInt32(GetRandomNumber(0, 5));
                items.Add(common[i]);
                break;
        }
    }

    private void Update() {
        itemtest.text = "";
        foreach (string a in items) {
            itemtest.text = itemtest.text += a + " ";
        }
    }

    public double GetRandomNumber(double minimum, double maximum) { //  https://stackoverflow.com/questions/1064901/random-number-between-2-double-numbers
        return rand.NextDouble() * (maximum - minimum) + minimum;
    }
}
