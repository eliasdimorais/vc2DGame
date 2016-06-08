using UnityEngine;
using System.Collections;

public class GlobalManager : MonoBehaviour {

	public static GlobalManager Instance;
	public GameObject player;
	public Transform TransitionTarget;

	public PlayerStatistics savedPlayerData = new PlayerStatistics();


	void Awake(){
		if(Instance == null){
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}else if (Instance != this){
			Destroy(gameObject);
		}

		if(TransitionTarget == null){
			TransitionTarget = gameObject.transform;
		}
	}
}
