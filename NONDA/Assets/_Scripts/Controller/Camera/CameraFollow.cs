using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	#region Public Variables
	//public float minX, maxX;
	public float smoothTimeX;
	public float smoothTimeY;

	#endregion

	#region Private Variables
	//private Transform player;
	private Vector2 velocity;
	#endregion

	public GameObject player;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void FixedUpdate(){
		float posX = Mathf.SmoothDamp(transform.position.x, transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp(transform.position.y, transform.position.y, ref velocity.y, smoothTimeY);

		transform.position = new Vector3(posX, posY,transform.position.z);
	}
	void Update () {
//		Vector3 temp = transform.position;
//		temp.x = player.position.x;
//		temp.y = player.position.y + 3.1f; //TO CHANGE THIS VALUE
//
//		if(temp.x < minX){
//			temp.x = minX;
//		}
//		if(temp.x > maxX){
//			temp.x = maxX;
//		}
//		transform.position = temp; 
	} 

	//NEED A COLLIDER TO NOT GOING FAR FROM THE CAMERA
}
