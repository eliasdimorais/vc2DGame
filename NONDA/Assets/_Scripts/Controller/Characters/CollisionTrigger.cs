using UnityEngine;
using System.Collections;

public class CollisionTrigger : MonoBehaviour {

	private  BoxCollider2D playerCollider;
	[SerializeField]
	private BoxCollider2D platformCollider;
	//private string myPlayer;
	[SerializeField]
	private BoxCollider2D platformTrigger;

	void Start () {
		//myPlayer = globalName.myName;
		playerCollider = GameObject.Find("Player").GetComponent <BoxCollider2D>();
		Physics2D.IgnoreCollision(platformTrigger, platformTrigger, true);
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.name == "Player"){
			Physics2D.IgnoreCollision(platformTrigger, playerCollider, true);
			Debug.Log("Passou aqui no OnTriggerEnter2D");
		}
	}	

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.name == "Player"){
			Physics2D.IgnoreCollision(platformTrigger, playerCollider, false);
		}
	}
}
