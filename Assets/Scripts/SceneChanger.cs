using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public GameObject MainMenu;
	public GameObject Settings;

    public void Awake()
    {
		Screen.fullScreen = true;
    }

    public void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
	public void Exit()
	{
		Application.Quit();
	}

	public void settings(bool open)
	{
		if (open)
		{
			Settings.SetActive(false);
			MainMenu.SetActive(true);
		}
		else if (!open)
		{
			Settings.SetActive(true);
			MainMenu.SetActive(false);
		}
	}
}