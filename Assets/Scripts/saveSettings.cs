using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveSettings : MonoBehaviour
{
    public void colour(int index)
    {
        PlayerPrefs.SetInt("colour", index);
;   }

    public void delay(int index)
    {
        PlayerPrefs.SetInt("delay", index);
    }
}
