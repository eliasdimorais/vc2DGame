using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		print(PlayerPrefsManager.GetMasterVolume());
		PlayerPrefsManager.SetMasterVolume(0.1f);
		print(PlayerPrefsManager.GetMasterVolume());

		print(PlayerPrefsManager.IsLevelUnlocked(2));
		PlayerPrefsManager.UnlockLevel(-3);
		print(PlayerPrefsManager.IsLevelUnlocked(2));

		print(PlayerPrefsManager.GetGameDifficulty());
		PlayerPrefsManager.SetGameDifficulty(0.04f);
		print(PlayerPrefsManager.GetGameDifficulty());

		print(PlayerPrefsManager.GetPointsToLevelClear());
		PlayerPrefsManager.SetScoreToLevelClear(500);
		print(PlayerPrefsManager.GetPointsToLevelClear());

		print(PlayerPrefsManager.GetHighScore());
		PlayerPrefsManager.SetHighscore(0);
		print(PlayerPrefsManager.GetHighScore());

		print(PlayerPrefsManager.GetScoreQuiz());
		PlayerPrefsManager.SetScoreQuiz(0);
		print(PlayerPrefsManager.GetScoreQuiz());

		print(PlayerPrefsManager.GetHighScoreQuiz());
		PlayerPrefsManager.SetHighscoreQuiz(0);
		print(PlayerPrefsManager.GetHighScoreQuiz());

		print(PlayerPrefsManager.GetNumberCorrectAnswer());
		PlayerPrefsManager.SetNumberCorrectAnswers(0);
		print(PlayerPrefsManager.GetNumberCorrectAnswer());

		print(PlayerPrefsManager.GetNumberWrongAnswer());
		PlayerPrefsManager.SetNumberWrongAnswers(0);
		print(PlayerPrefsManager.GetNumberWrongAnswer());


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
