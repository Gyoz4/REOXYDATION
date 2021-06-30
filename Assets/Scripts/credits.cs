using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits : MonoBehaviour
{
	public GameObject MainMenu;
	public GameObject Credits;

    public void Awake()
    {
        Credits.SetActive(false);
    }
    public void credit()
	{
		Credits.SetActive(true);
		MainMenu.SetActive(false);
	}

    private void OnApplicationQuit()
    {
		Application.CancelQuit();
    }
}
