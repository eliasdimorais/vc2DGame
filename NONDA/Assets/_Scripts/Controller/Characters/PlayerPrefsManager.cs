using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour {

	#region CONSTANT KEYS
	const string MASTER_VOLUME_KEY = "master_volume";
	const string DIFFICULTY_KEY = "difficulty";
	const string LEVEL_KEY = "level_unlocked_";
	const string ITEM_KEY = "item_unlocked_";

	const string HEALTH_KEY = "health";
	const string STARS_KEY = "stars";

	const string SCORE_KEY = "score";
	const string HIGHSCORE_KEY = "highscore";

	const string SCORE_QUIZ_KEY = "scoreQuiz";
	const string HIGHSCORE_QUIZ_KEY = "highscoreQuiz";
	const string NUMBER_WRONG_ANSWERS = "numberWrongAnswers";
	const string NUMBER_CORRECT_ANSWERS = "numberCorrectAnswers";


	#endregion

	//Settings for Change Game Music Volume
	public static void SetMasterVolume(float volume){
		if(volume > 0f && volume < 1f){
			PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
		}else{
			Debug.LogError("It could not reach the master volume");
		}
	}

	public static float GetMasterVolume(){
		return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
	}

	//Settings for Check Unlock Level
	public static void UnlockLevel (int level){
		if(level <= SceneManager.sceneCountInBuildSettings -1){
			PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1); //Use 1 for true because there is no boolean on PlayerPrefs
		}else{
			Debug.LogError("No level founded. Are you trying to unlock level that are not under Build Settings?");
		}
	}

	public static bool IsLevelUnlocked(int level){
		int levelValue = PlayerPrefs.GetInt (LEVEL_KEY + level.ToString());
		bool isLevelUnlocked = (levelValue == 1);
		if(level <= SceneManager.sceneCountInBuildSettings -1){
			return isLevelUnlocked;
		}else{
			Debug.LogError("No level founded. Are you trying to unlock level that are not under Build Settings? I can't reach bro");
			return false;
		}
	}

	//Settings for Change Difficulty of the Game
	public static void SetGameDifficulty(float difficulty){
		if(difficulty >= 0 && difficulty <= 1f){
			PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
		}else{
			Debug.LogError("Difficulty could't be reached.");
		}
	}

	public static float GetGameDifficulty(){
		return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
	}


	#region Player Level 
	public static void SetScoreToLevelClear(uint score){
		if (score > 0){

			PlayerPrefs.SetInt(SCORE_KEY, (int)score);
		}else{
			Debug.LogError("POINTS OUT OF RANGE");
		}
	}

	public static int GetPointsToLevelClear(){
		return PlayerPrefs.GetInt(SCORE_KEY);
	}

	public static void SetHighscore(uint highscore){
		if (highscore > PlayerPrefs.GetInt(HIGHSCORE_KEY)){

			PlayerPrefs.SetInt(HIGHSCORE_KEY, (int)highscore);
		}else{
			Debug.LogError("HIGHSCORE OUT OF RANGE");
		}
	}

	public static int GetHighScore(){
		return PlayerPrefs.GetInt(HIGHSCORE_KEY);
	}

	#endregion

	#region QUIZ CONTROLLER 
	public static int GetScoreQuiz(){
		return PlayerPrefs.GetInt(SCORE_QUIZ_KEY);
	}

	public static void SetScoreQuiz(int scoreQuiz){
		PlayerPrefs.SetInt(SCORE_QUIZ_KEY, scoreQuiz);

	}

	public static int GetHighScoreQuiz(){
		return PlayerPrefs.GetInt(HIGHSCORE_QUIZ_KEY);
	}

	public static void SetHighscoreQuiz(int highscoreQuiz){
		if (highscoreQuiz > PlayerPrefs.GetInt(HIGHSCORE_QUIZ_KEY)){

			PlayerPrefs.SetInt(HIGHSCORE_QUIZ_KEY, highscoreQuiz);
		}else{
			Debug.LogError("HIGHSCORE QUIZ OUT OF RANGE");
		}
	}

	public static int GetNumberWrongAnswer(){
		return PlayerPrefs.GetInt(NUMBER_WRONG_ANSWERS);
	}

	public static void SetNumberWrongAnswers(int numberWrongAnswers){
		if (numberWrongAnswers > PlayerPrefs.GetInt(NUMBER_WRONG_ANSWERS)){
			PlayerPrefs.SetInt(NUMBER_WRONG_ANSWERS, numberWrongAnswers);
		}else{
			Debug.LogError("NUMBER LESS THAN 0 or QUIZ OUT OF RANGE");
		}
	}

	public static int GetNumberCorrectAnswer(){
		return PlayerPrefs.GetInt(NUMBER_CORRECT_ANSWERS);
	}

	public static void SetNumberCorrectAnswers(int numberCorrectAnswers){
		if (numberCorrectAnswers > PlayerPrefs.GetInt(NUMBER_CORRECT_ANSWERS)){
			PlayerPrefs.SetInt(NUMBER_CORRECT_ANSWERS, numberCorrectAnswers);
		}else{
			Debug.LogError("NUMBER LESS THAN 0 or QUIZ OUT OF RANGE");
		}
	}

	public static void ResetAllQuizValues(int highscore, int score, int numberCorrectAnswers, int numberWrongAnswers){
		PlayerPrefs.SetInt(HIGHSCORE_QUIZ_KEY, highscore);
		PlayerPrefs.SetInt(SCORE_QUIZ_KEY, score);
		PlayerPrefs.SetInt(NUMBER_CORRECT_ANSWERS, numberCorrectAnswers);
		PlayerPrefs.SetInt(NUMBER_WRONG_ANSWERS, numberWrongAnswers);
	}

	public static void ResetAllQuizValues(int score, int numberCorrectAnswers, int numberWrongAnswers){
		PlayerPrefs.SetInt(SCORE_QUIZ_KEY, score);
		PlayerPrefs.SetInt(NUMBER_CORRECT_ANSWERS, numberCorrectAnswers);
		PlayerPrefs.SetInt(NUMBER_WRONG_ANSWERS, numberWrongAnswers);
	}

	#endregion

}
