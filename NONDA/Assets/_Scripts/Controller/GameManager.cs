using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	#region Level Handling
	//[SerializeField] private bool isTimeLevel = true;
	#endregion

	#region Instances
	private static GameManager instance;
	private Player player; 
	#endregion

	#region Score variables
	[SerializeField] private Text scoreText;
	[SerializeField] private uint scoreToNextLevel;
	private int score = 0;
	private bool isLevelOver = false;
	#endregion

	#region Pause Menu
	public bool isPaused;
	public GameObject pauseMenu;
	#endregion

	#region TimeBarUI variables
	//timer
	public Image timerBar;
	public float currentTimerBar = 0f;
	public float maxTimerBar;
	#endregion

	#region Save Player Data
	public GameObject alertReference;
	public PlayerStatistics localPlayerData = new PlayerStatistics();
	#endregion

	#region Health 
	[SerializeField]private uint healthRemaining = 5;
	public Sprite[] HeartSprites;
	public Image HeartImageUI;
	#endregion

	#region Stars Game

	#endregion

	#region Level Clear Canvas
	public GameObject levelClearCanvas;
	[SerializeField]private Text scoreTextCanvas;
	[SerializeField]private Text healthTextCanvas;
	[SerializeField] private Text conditionLabel;
	#endregion

	public static GameManager Instance{
		get{
			if(instance == null){
				instance = FindObjectOfType<GameManager>();
			}
			return instance;
		}
	}

	void Start(){
		player = GameObject.Find("Player").GetComponent<Player>();
		currentTimerBar = maxTimerBar;
		InvokeRepeating("increaseTime", 2, 2);
		levelClearCanvas.SetActive(false);
	}

	void Update(){
		ChangeHeartSpriteUI(player.Health);
		LoadLevelClear();
	}

	void Awake(){
		scoreText.text = score.ToString("00000");
	}

	void LoadNextLevel(){
		if(score >= scoreToNextLevel){
			SceneManager.LoadScene(sceneBuildIndex:+1);
		}
	}

	public void UpdateScore(int scores){
		score  += scores;
		scoreText.text = score.ToString("00000");
	}

	#region Pause Menu Code
	public void Pause(){
		if(Time.timeScale == 1){
			isPaused = true;
			Time.timeScale = 0;
			pauseMenu.SetActive(true);
		}else if(Time.timeScale == 0){
			isPaused = !isPaused;
			Time.timeScale = 1;
			pauseMenu.SetActive(false);
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
	#endregion

	#region Timebar
	//handles with the Level Clear Screen
	void increaseTime(){
		currentTimerBar -= 5f;
		float cal_Timer = currentTimerBar / maxTimerBar;
		SetTimeBar(cal_Timer);
		if(currentTimerBar == 0){
			isLevelOver = true;
		}
		if(isLevelOver == true){
			levelClearCanvas.SetActive(true);
			//Pause();
		}
	}

	void SetTimeBar (float myTimer) {
		timerBar.fillAmount = myTimer;
	}
	#endregion

	public void SavePlayer(){
		GlobalManager.Instance.savedPlayerData = localPlayerData;
	}

	public void ChangeHeartSpriteUI(uint newHealth){
		int updatedHealth = (int) newHealth;
		for (int i = (int)healthRemaining; i <= updatedHealth; i--) {
			if(updatedHealth == i){ 
				HeartImageUI.sprite = HeartSprites[i];
				break;
			}
		}
		HealthRemaining(newHealth);
	}

	void HealthRemaining(uint totalHealth){
		healthRemaining = totalHealth;
	}

	#region Handling with Victory / Lose
	public void LoadLevelClear(){
		scoreTextCanvas.text = score.ToString("00000");
		healthTextCanvas.text = player.Health.ToString("0");
		MessageToPlayer();

	}

	void MessageToPlayer(){
		if(healthRemaining > 0 && score >= scoreToNextLevel){
			conditionLabel.text = "MUITO BEM!";
		}else{
			conditionLabel.text = "AHHHH NÃO!";
		}
	}
//
//	void EnableWinLabel(){
//		winLabel.enabled = !winLabel.enabled;
//	}
//
//	void EnableLoseLabel(){
//		winLabel.enabled = !winLabel.enabled;
//	}

	#endregion

}
