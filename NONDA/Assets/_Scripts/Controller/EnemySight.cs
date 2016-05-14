using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {
	#region Private Variables
	[SerializeField] private Enemy enemy;
	#endregion
	void Start () {
		
	}

	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			enemy.Target = other.gameObject;
			Debug.Log("I found you worm little thing");
		}

	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			enemy.Target = null;
			Debug.Log("I'm letting you go");
		}
	}
}
