using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class mapLoad : MonoBehaviour {
    public Sprite map0;
    public Sprite map1;
    public Sprite map2;
    public Sprite map3;
    public Sprite map4;
    public Sprite map5;
    public Sprite map6;
    public Sprite map7;
    public Sprite map8;
    public Sprite map9;
    public Sprite map10;

    void Update() {
        switch (consoleController.stage) {
            case 0:
                break;
            case 1:
                GetComponent<Image>().sprite = map1;
                break;
            case 2:
                GetComponent<Image>().sprite = map2;
                break;
            case 3:
                GetComponent<Image>().sprite = map3;
                break;
            case 4:
                GetComponent<Image>().sprite = map4;
                break;
            case 5:
                GetComponent<Image>().sprite = map5;
                break;
            case 6:
                GetComponent<Image>().sprite = map6;
                break;
            case 7:
                GetComponent<Image>().sprite = map7;
                break;
            case 8:
                GetComponent<Image>().sprite = map8;
                break;
            case 9:
                GetComponent<Image>().sprite = map9;
                break;
            case 10:
                GetComponent<Image>().sprite = map10;
                break;
        }
    }

}
