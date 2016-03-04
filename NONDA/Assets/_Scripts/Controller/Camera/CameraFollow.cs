using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	#region Public Variables
	public float minX, maxX;
	#endregion

	#region Private Variables
	private Transform player;
	#endregion

	void Start () {
		//player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update () {
		/*Vector3 temp = transform.position;
		temp.x = player.position.x;
		temp.y = player.position.y + 3.1f; //TO CHANGE THIS VALUE

		if(temp.x < minX){
			temp.x = minX;
		}
		if(temp.x > maxX){
			temp.x = maxX;
		}
		transform.position = temp; */
	}

	//NEED A COLLIDER TO NOT GOING FAR FROM THE CAMERA
}
