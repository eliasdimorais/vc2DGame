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
	[SerializeField] private Text scoreCurrentLevel;
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

	#region Tutorial Canvas
	[SerializeField]private GameObject tutorialCanvas;
	//private Animator hands;
	#endregion

	#region Level Clear Canvas
	public GameObject levelClearCanvas;
	[SerializeField]private Text scoreTextCanvas;
	[SerializeField]private Text healthTextCanvas;
	[SerializeField]private Text conditionLabel;
	[SerializeField]private GameObject wormSad;
	[SerializeField]private GameObject wormHappy;
	[SerializeField]private GameObject buttonPlay;
	[SerializeField]private GameObject buttonPlayAgain;
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
		scoreCurrentLevel.text = scoreToNextLevel.ToString("0000");
		currentTimerBar = maxTimerBar;
		InvokeRepeating("increaseTime", 2, 2);
		levelClearCanvas.SetActive(false);
		//audioManager = AudioManager.Instance;
		//if(audioManager == null){
			//Debug.LogError("IM KREAZI NO AUDIOMANAGER ON THE MF SCENE");
		//} 
		wormSad.SetActive(false);
		wormHappy.SetActive(false);
		//hands = GameObject.FindObjectOfType<Animator>();
		//hands = GameObject.Find("doubleTap").GetComponent<Animator>();
		//Debug.Log(hands);

		buttonPlay.SetActive(false);
		buttonPlayAgain.SetActive(false);
	}

	void Update(){
		ChangeHeartSpriteUI(player.Health);
		ShowLevelClear();

	
	}

	void Awake(){
		OpenTutorial();
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
//	public void Pause(){
//		if(Time.timeScale == 1){
//			isPaused = !isPaused;
//			Time.timeScale = 0;
//			pauseMenu.SetActive(true);
//		}else if(Time.timeScale == 0){
//			isPaused = !isPaused;
//			Time.timeScale = 1;
//			pauseMenu.SetActive(false);
//		}
//	}

	public void Pause( bool amIPaused){
		if(amIPaused == true){ //se tiver pausado, despausar
			isPaused = !isPaused;
			Time.timeScale = 0; //velocity normal
		}else if(amIPaused == false){ // pausar
			isPaused = !isPaused;
			Time.timeScale = 1;
		}
	}

	public void PauseMenu(){
		if(!isPaused){
			Pause(true);//se nao tiver pausado, eu envio a informacao que nao esta pausado para o Pause(condition)
			pauseMenu.SetActive(true);
		}else{
			Pause(false);
			pauseMenu.SetActive(false);
		}
	}
	public void Tutorial(){
		if(!isPaused){
			Pause(true);
			tutorialCanvas.SetActive(true);
		}else{
			Pause(false);
			tutorialCanvas.SetActive(false);
		}
	}

	public void OpenTutorial(){
		pauseMenu.SetActive(false); //disable pause and overlay
		Pause(true);
		tutorialCanvas.SetActive(true);
	}

	public void PauseLevelClear(){
		if(Time.timeScale == 1){
			Time.timeScale = 0;
		}
	}

	public void Resume(){
		isPaused = false;
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
			Pause(isTimeUp);
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
		//audioManager.PlaySound(healthSoundName);
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

		if(IsScoreEnough() && IsScoreEnough()){
			wormHappy.SetActive(true);

			conditionLabel.text = "MUITO BEM!";
			scoreTextCanvas.color = Color.black;
		}else{
			wormSad.SetActive(true);

			conditionLabel.text = "AHHH NÃO!";
			scoreTextCanvas.color = Color.red;
		}
	}

	public void LoadLevelClear(){
		if(IsScoreEnough() && IsHealthEnough() ){
			levelClearCanvas.SetActive(true);
			buttonPlay.SetActive(true);
			//Debug.Log(buttonPlay.activeInHierarchy);
			PauseLevelClear();
			ShowLevelClear();
		}else if(IsTimeUp()){
			levelClearCanvas.SetActive(true);

			Debug.Log("Button Play Again " + buttonPlayAgain);

			buttonPlayAgain.SetActive(true); 
			Debug.Log(buttonPlayAgain.activeInHierarchy);

			ShowLevelClear();
		}else{
			return;
			//Not over yet
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
