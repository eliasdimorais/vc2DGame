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
	[SerializeField] private float timeBetweenQuiz = 0f;
	[SerializeField]private Animator animator;
	#endregion

	void Start(){
		if(unansweredQuestions == null || unansweredQuestions.Count  == 0){
			unansweredQuestions = questions.ToList<Quiz>();
		}	
		SetCurrentQuestion();
	}

	void SetCurrentQuestion(){
		int randomQuestIndex = Random.Range(0, unansweredQuestions.Count);
		currentQuestion = unansweredQuestions[randomQuestIndex];

		questionText.text =  currentQuestion.question;

		if(currentQuestion.isTrue){
			trueAnswerText.text = "ESTÁ ERRADO =(";
			falseAnswerText.text = "MUITO BEM!";
		}
		else{
			trueAnswerText.text = "MUITO BEM!";
			falseAnswerText.text = "ESTÁ ERRADO =(";
		}
	}

	IEnumerator TransitionToNextQuestion(){
		unansweredQuestions.Remove(currentQuestion);
		yield return new WaitForSeconds (timeBetweenQuiz);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void UserSelectTrue(){
		animator.SetTrigger("True");
		if(currentQuestion.isTrue){
			Debug.Log("Correct");
		}
		else{
			Debug.Log("Wrong!");
		}

		StartCoroutine(TransitionToNextQuestion());
	}

	public void UserSelectFalse(){
		animator.SetTrigger("False");
		if(!currentQuestion.isTrue){
			Debug.Log("Correct");
		}
		else{
			Debug.Log("Wrong!");
		}
		StartCoroutine(TransitionToNextQuestion());
	}
}
