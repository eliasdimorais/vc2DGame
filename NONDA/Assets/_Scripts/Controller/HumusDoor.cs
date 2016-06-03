using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HumusDoor : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			 SceneManager.LoadScene("03Level_Clear");
		}	
	}
 }		

