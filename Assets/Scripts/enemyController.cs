using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyController : MonoBehaviour {
    public Sprite blue;
    public Sprite red;
    public Sprite green;

    private void Awake() {
        GetComponent<Image>().sprite = green;
    }

    public void EnemyUpdate(string name) {
        switch (name) {
            case "red":
                GetComponent<Image>().sprite = red;
                break;
            case "blue":
                GetComponent<Image>().sprite = blue;
                break;
        }
    }
}
