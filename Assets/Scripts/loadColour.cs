using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadColour : MonoBehaviour
{
    public Sprite blue;
    public Sprite green;
    public Sprite purple;
    public Sprite red;
    public Sprite white;

    private void Awake() {
        int colour = PlayerPrefs.GetInt("colour");

        switch (colour) {
            case 0:
                GetComponent<Image>().sprite = blue;
                break;
            case 1:
                GetComponent<Image>().sprite = green;
                break;
            case 2:
                GetComponent<Image>().sprite = purple;
                break;
            case 3:
                GetComponent<Image>().sprite = red;
                break;
            case 4:
                GetComponent<Image>().sprite = white;
                break;
            default:
                GetComponent<Image>().sprite = blue;
                break;
        }
        //Debug.Log(colour);
    }
}
