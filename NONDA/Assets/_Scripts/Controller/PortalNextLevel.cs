using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PortalNextLevel : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
 		if(other.tag == "Player"){
				GameManager.Instance.LoadLevelClear();
			}
	}
 }		

