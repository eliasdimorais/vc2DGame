using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	#region Public Variables
	public LevelManager levelManager;
	#endregion

	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
	}

	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.name == "Player"){
			levelManager.RespawnPlayer();
		}
	}
}
