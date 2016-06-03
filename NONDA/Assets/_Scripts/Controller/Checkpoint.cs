using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	#region Public Variables
	public LevelManager levelManager;
	#endregion

	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.name == "Player"){
			levelManager.currentCheckpoint = gameObject;
			//Debug.Log("Activated Checkpoint" + transform.position);
		}
	}
}
