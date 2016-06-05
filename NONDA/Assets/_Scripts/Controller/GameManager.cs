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
	private AudioManager audioManager;
	private Player player; 
	#endregion

	#region Score variables
	[SerializeField] private Text scoreText;
	[SerializeField] private uint scoreToNextLevel;
	private int score = 0;
	private bool isTimeUp = false;
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
	public string healthSoundName;
	#endregion

	#region Stars Game

	#endregion

	#region Level Clear Canvas
	public GameObject levelClearCanvas;
	[SerializeField]private Text scoreTextCanvas;
	[SerializeField]private Text healthTextCanvas;
	[SerializeField]private Text conditionLabel;
	[SerializeField]private GameObject wormSad;
	[SerializeField]private GameObject wormHappy;
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
		isPaused = !isPaused;
		player = GameObject.Find("Player").GetComponent<Player>();
		currentTimerBar = maxTimerBar;
		InvokeRepeating("increaseTime", 2, 2);
		levelClearCanvas.SetActive(false);
		audioManager = AudioManager.Instance;
		if(audioManager == null){
			Debug.LogError("IM KREAZI NO AUDIOMANAGER ON THE MF SCENE");
		} 
		//wormHappy = gameObject.GetComponent<SpriteRenderer>().sprite;
		wormSad.SetActive(false);
		wormHappy.SetActive(false);
	}

	void Update(){
		ChangeHeartSpriteUI(player.Health);
		ShowLevelClear();
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

	public void PauseLevelClear(){
		if(Time.timeScale == 1){
			Time.timeScale = 0;
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
	//Handle With Level Clear if TIme is up
	void increaseTime(){
		currentTimerBar -= 5f;
		float cal_Timer = currentTimerBar / maxTimerBar;
		SetTimeBar(cal_Timer);
		if(currentTimerBar == 0){
			isTimeUp = true;
		}
		if(isTimeUp == true){
			levelClearCanvas.SetActive(true);
			LoadLevelClear();
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
		audioManager.PlaySound(healthSoundName);
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
	public void ShowLevelClear(){
		scoreTextCanvas.text = score.ToString("00000");
		healthTextCanvas.text = player.Health.ToString("0");

		if(healthRemaining > 0 && score >= scoreToNextLevel){
			conditionLabel.text = "MUITO BEM!";
			wormSad.SetActive(true);
		}else{
			conditionLabel.text = "AHHH NÃO!";
			wormHappy.SetActive(true);
		}
	}

	public void LoadLevelClear(){
		if(IsScoreEnough() && IsHealthEnough() ){
			ShowLevelClear();
			PauseLevelClear();
			Debug.Log("Passei Antes e tenho vida");
		}else if(IsTimeUp() && !IsHealthEnough()){
			//perdeu por vida E/OU tempo
			Debug.Log("Tempo Esgotado ou PLayer Sem Vida");
			PauseLevelClear();
			ShowLevelClear();
		}else{
			PauseLevelClear();
			ShowLevelClear();

			return;
		}

	}

	public bool IsLevelOver(){
		return isTimeUp ? true : false;
	}

	public bool IsScoreEnough(){
		return score >= scoreToNextLevel;
	}

	public bool IsHealthEnough(){
		return healthRemaining > 0;
	}

	public bool IsTimeUp(){
		return currentTimerBar <= 0;
	}
	#endregion

}
