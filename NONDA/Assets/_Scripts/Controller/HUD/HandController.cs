using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour {
	
	#region Public Ground Variables
	public Transform hand; // Drag hand here
	//private Vector2 startPosition;
	#endregion

	#region Instances 
	private static HandController instance;
	public static HandController Instance{
		get {
			if(instance == null){
				instance = GameObject.FindObjectOfType<HandController>();
			}
			return instance;
		}
	}
	public Animator MyAnimator {
		get;
		private set;
	}
	#endregion

	void Start () {
		//startPosition = gameObject.transform.position;
		MyAnimator = GetComponent<Animator>();
	}

	public void PlayHandAnimation(){
		MyAnimator.SetTrigger("ItemSpawn");
	}

}
