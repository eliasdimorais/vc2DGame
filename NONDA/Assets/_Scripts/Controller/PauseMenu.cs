using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	#region Public Variables
	public string levelSelect;
	//public string mainMenu;
	public bool isPaused;
	public GameObject pauseMenuCanvas;
	#endregion


	void Update () {
		if(isPaused){
			pauseMenuCanvas.SetActive(true);
			Time.timeScale = 0f;
		}else{
			pauseMenuCanvas.SetActive(false);
			Time.timeScale = 1f;
		}

		if(Input.GetKeyDown(KeyCode.Escape)){
			isPaused = !isPaused;
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
