﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
	public GameObject MainMenu;
	public GameObject Settings;
	public GameObject Tutorial;

	public TMP_Text txt;


	public void Awake()
    {
		Screen.fullScreen = true;
		Settings.SetActive(false);
		Tutorial.SetActive(false);
		MainMenu.SetActive(true);
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
			Tutorial.SetActive(false);
		}
		else if (!open)
		{
			Settings.SetActive(true);
			MainMenu.SetActive(false);
			Tutorial.SetActive(false);
		}
	}
	public void tutorial(bool open) {
		if (open) {
			Settings.SetActive(true);
			MainMenu.SetActive(false);
			Tutorial.SetActive(false);
		}
		else if (!open) {
			Settings.SetActive(false);
			MainMenu.SetActive(false);
			Tutorial.SetActive(true);
		}
	}

	private void Update() {
		if (consoleController.finished)
			txt.text = "Congrats you beat this very hard game !!!!!";
		else if (consoleController.dead)
			txt.text = "You died and failed";
		else
			txt.text = "";
    }
}