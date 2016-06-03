using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

	#region Public Variables
	//timebar
	public Image timerBar;
	public float currentTimerBar = 0f;
	public float maxTimerBar;
	public GameObject alertReference;
	#endregion

	#region Private Variables
	private bool isLevelOver = false;
	#endregion

	void Start () {
		currentTimerBar = maxTimerBar;
		InvokeRepeating("increaseTime", 2, 2);
	}

	#region Timebar
	void increaseTime(){
		currentTimerBar -= 5f;
		float cal_Timer = currentTimerBar / maxTimerBar;
		SetTimeBar(cal_Timer);
		if(currentTimerBar == 0){
			isLevelOver = true;
		}
		if(isLevelOver == true){
			SceneManager.LoadScene("03Level_Clear");
		}
	}

	void SetTimeBar (float myTimer) {
		timerBar.fillAmount = myTimer;
	}
	#endregion
}
