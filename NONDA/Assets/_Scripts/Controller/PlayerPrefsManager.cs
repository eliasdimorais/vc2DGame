using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour {

	#region CONSTANT KEYS
	const string MASTER_VOLUME_KEY = "master_volume";
	const string DIFFICULTY_KEY = "difficulty";
	const string LEVEL_KEY = "level_unlocked_";
	const string ITEM_KEY = "item_unlocked_";
	const string SCORE_KEY = "score";
	const string HEALTH_KEY = "health";
	const string STARS_KEY = "stars";

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

	public static float GetPointsToLevelClear(){
		return PlayerPrefs.GetInt(SCORE_KEY);
	}

	#endregion

}
