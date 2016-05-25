using UnityEngine;
using System.Collections;

public class ShowPoints : MonoBehaviour {
	public GUIText pointsText;
	public Color color = Color.yellow; 
	public float scroll = 0.05f;
	public float duration = 1.5f; 
	public float alpha;
	public float temp;

	void Start(){
		pointsText.color = color;
		alpha = 1;
		temp = transform.position.y;
	}

	void Update(){
		if(alpha > 0){
			temp += scroll*Time.deltaTime;
			alpha -= Time.deltaTime/duration;
			alpha = pointsText.material.color.a;
			Debug.Log("Quase saindo daqui");
		}else{
			DestroyObject(pointsText);
		}
	}
}
