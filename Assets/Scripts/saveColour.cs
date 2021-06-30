using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveColour : MonoBehaviour
{
    public void colour(int index)
    {
        PlayerPrefs.SetInt("colour", index);
        Debug.Log(index);
;    }
}
