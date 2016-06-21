using UnityEngine;
using System.Collections;

public class ShowPoints : MonoBehaviour {
	public GUIText pointsText;
	public Color color = new Color(0.8f, 0.5f, 0,1.0f); 
	public float scroll = 0.02f; //scrolling velocity
	public float duration = 4.5f; 
	public float alpha;

	void Start(){
		GetComponent<GUIText>().material.color = color;
		alpha = 1;
	}

	void Update(){
		if(alpha > 0){
			Vector3 temp = transform.position;
			temp.y += scroll*Time.deltaTime;
			alpha -= Time.deltaTime/duration;
			Color tempC=GetComponent<GUIText>().material.color;
            tempC.a=alpha;
            GetComponent<GUIText>().material.color=tempC;       
         } else {
             Destroy(gameObject); // text vanished - destroy itself
         }
	}
}
