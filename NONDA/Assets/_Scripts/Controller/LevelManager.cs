using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
	#region Public Variables
	public float autoLoadNextLevelAfter;
	#endregion

	void Start(){
		if(autoLoadNextLevelAfter <= 0){
			Debug.Log("Level auto load DISABLED, use a positive number in SECONDS");
		}else{
			Invoke ("LoadNextLevel", autoLoadNextLevelAfter);
		}
		
	}
	public void LoadLevel(string name)
    {
		Debug.Log ("New Level load: " + name);
		Application.LoadLevel (name);
	}
	public void QuitRequest()
    {
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

	public void LoadNextLevel()
    {
		Application.LoadLevel(Application.loadedLevel + 1); //load next level
	}
	
}


