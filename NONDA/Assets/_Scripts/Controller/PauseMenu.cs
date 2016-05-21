﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	#region Public Variables
	//public string levelSelect;
	//public string mainMenu;
	public bool isPaused;
	public GameObject pauseMenuCanvas;
	#endregion

	public void Pause(){
		if(Time.timeScale == 1){
			isPaused = true;
			Time.timeScale = 0;
			pauseMenuCanvas.SetActive(true);
		}else if(Time.timeScale == 0){
			isPaused = !isPaused;
			Time.timeScale = 1;
			pauseMenuCanvas.SetActive(false);
		}
	}

	public void Resume(){
		isPaused = false;
	}

	public void LevelSelect(){
		SceneManager.LoadScene(sceneName:"00Splash");
	}

	public void Quit(){
		SceneManager.LoadScene(sceneName:"01a_Start");
	}
}
