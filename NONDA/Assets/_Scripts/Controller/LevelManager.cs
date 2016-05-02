using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	#region Public Variables
	public float autoLoadNextLevelAfter;
	public GameObject currentCheckpoint;
	#endregion

	#region Private Variabless
	private Player player;
	private SpawnerController spawner;
	#endregion

	void Start(){
		player = FindObjectOfType<Player>();
		if(autoLoadNextLevelAfter <= 0){
			Debug.Log("Level auto load DISABLED, use a positive number in SECONDS");
		}else{
			Invoke ("LoadNextLevel", autoLoadNextLevelAfter);
		}	
	}

	public void LoadLevel(string name){
		Debug.Log ("New Level loaded: " + name);
		SceneManager.LoadScene (name);
	}

	public void QuitRequest(){
		//Debug.Log ("Quit requested");
		Application.Quit ();
	}

	public void LoadNextLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //load next level
	}

	public void RespawnPlayer(){
		//Debug.Log("Player Respawn Here");
		player.transform.position = currentCheckpoint.transform.position;
	}

//	public void CoroutineSpawn(){
//		spawner.SpawnEnemy (SceneManager.GetActiveScene().buildIndex);
//	}
	
}


