using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

	#region Public Variables
	public Image timerBar;
	public float cur_TimerBar = 0f;
	public float maxTimerBar = 100f;
	#endregion

	#region Private Variables
	private bool isEndOfLevel = false;
	#endregion

	void Start () {
		cur_TimerBar = maxTimerBar;
		InvokeRepeating("increaseTime", 2f, 2f);
	}

	void increaseTime(){
		cur_TimerBar -= 5f;
		float cal_Timer = cur_TimerBar / maxTimerBar;
		SetTimeBar(cal_Timer);
	}

	void SetTimeBar (float myTimer) {
		timerBar.fillAmount = myTimer;
	}
}
