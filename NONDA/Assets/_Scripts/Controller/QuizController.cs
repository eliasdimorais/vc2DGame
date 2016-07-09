using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class QuizController : MonoBehaviour {
	#region Public Variables
	public Quiz[] questions; 
	#endregion

	#region Private Variables
	private static List<Quiz> unansweredQuestions;
	private Quiz currentQuestion;
	[SerializeField] private Text questionText;
	[SerializeField] private Text trueAnswerText;
	[SerializeField] private Text falseAnswerText;
	[SerializeField] private Text scoreQuizText;
	[SerializeField] private Text wrongScoreTxt;
	[SerializeField] private Text correctScoreTxt;

	[SerializeField] private float timeBetweenQuiz = 0f;

	[SerializeField] private Text highscoreQuizText;
	[SerializeField] private GameObject resultPanel;
	private int scoreQuiz = 0;
	private int wrongScore = 0;
	private int correctScore = 0;
	private bool canIClick = false;

	[SerializeField]private Animator animator;
	#endregion

	void Awake(){
		int y = SceneManager.GetActiveScene().buildIndex;

		if(y == 5){ //change for whatever build index number is when final project is done
			LoadDataOnPlayerPrefs();
		}
	}

	void Start(){
		
		if(unansweredQuestions == null || unansweredQuestions.Count  ==  0){
			unansweredQuestions = questions.ToList<Quiz>();
		}	
		SetCurrentQuestion();
		canIClick = true; //enable 1 click only
	}

	void SetCurrentQuestion(){
		int randomQuestIndex = Random.Range(0, unansweredQuestions.Count);
		currentQuestion = unansweredQuestions[randomQuestIndex];

		questionText.text =  currentQuestion.question;

		if(currentQuestion.isTrue){
			trueAnswerText.color = new Color(0.835f, 0.149f, 0.102f);
			//trueAnswerText.color = new Color(0.867f, 0.333f, 0.251f);

			trueAnswerText.text = "TENTE \nDE NOVO";

			falseAnswerText.color = new Color(0.867f, 0.984f,0.416f);
			falseAnswerText.text = "MUITO BEM!";

		}
		else{
			trueAnswerText.color = new Color(0.867f, 0.984f,0.416f);
			trueAnswerText.text = "MUITO BEM!";

			falseAnswerText.color = new Color(0.867f, 0.333f, 0.251f);
			falseAnswerText.text = "TENTE \nDE NOVO";
		}
	}

	IEnumerator TransitionToNextQuestion(){
		unansweredQuestions.Remove(currentQuestion);
		yield return new WaitForSeconds (timeBetweenQuiz);

		//resetar dados da animação + perguntas
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void UserSelectTrue(){
		animator.SetTrigger("True");
		if(currentQuestion.isTrue){
			Debug.Log("Correct");
			UpdateScore(5);
			correctScore += 1;
		}
		else{
			Debug.Log("Wrong!");
			UpdateScore(-5);
			wrongScore += 1;
		}
		StartCoroutine(TransitionToNextQuestion());
		SaveDataOnPlayerPrefs();
		canIClick = !canIClick;
	}

	public void UserSelectFalse(){
		animator.SetTrigger("False");
		if(!currentQuestion.isTrue){
			Debug.Log("Correct");
			UpdateScore(5);
			UpdateCorrectAnswer();
		}
		else if(currentQuestion.isTrue){
			Debug.Log("Wrong!");
			UpdateScore(-5);
			UpdateWrongAnswer();
		}
		StartCoroutine(TransitionToNextQuestion());
		SaveDataOnPlayerPrefs();
		canIClick = !canIClick;
	}

	void UpdateScore(int currentScore){
		scoreQuiz  += currentScore;
		scoreQuizText.text = scoreQuiz.ToString();
		if(PlayerPrefsManager.GetHighScoreQuiz() < scoreQuiz){
			PlayerPrefsManager.SetHighscoreQuiz(scoreQuiz);
		}
		PlayerPrefsManager.SetScoreQuiz(scoreQuiz);
	}

	void UpdateWrongAnswer(){
		wrongScore++;
		wrongScoreTxt.text = wrongScore.ToString();
	}

	void UpdateCorrectAnswer(){
		correctScore++;
		correctScoreTxt.text = correctScore.ToString();
	}

	void SaveDataOnPlayerPrefs(){
		//highscore save as soon as player answer correct + 1
		PlayerPrefsManager.SetScoreQuiz(scoreQuiz);
		PlayerPrefsManager.SetNumberWrongAnswers(wrongScore);
		PlayerPrefsManager.SetNumberCorrectAnswers(correctScore);

	}

	void LoadDataOnPlayerPrefs(){
		scoreQuizText.text = PlayerPrefsManager.GetScoreQuiz().ToString();
		highscoreQuizText.text = PlayerPrefsManager.GetHighScoreQuiz().ToString();
		correctScoreTxt.text = PlayerPrefsManager.GetNumberCorrectAnswer().ToString();
		wrongScoreTxt.text = PlayerPrefsManager.GetNumberWrongAnswer().ToString();

		scoreQuiz = PlayerPrefsManager.GetScoreQuiz();
		wrongScore = PlayerPrefsManager.GetNumberWrongAnswer();
		correctScore = PlayerPrefsManager.GetNumberCorrectAnswer();
	}

	public void ResetAllDataOnPlayerPrefs(){
		PlayerPrefsManager.ResetAllQuizValues(0, 0, 0, 0);
	}

	public void ResetAllDataButHighScoreOnPlayerPrefs(){
		PlayerPrefsManager.ResetAllQuizValues(0, 0, 0);
	}
}
