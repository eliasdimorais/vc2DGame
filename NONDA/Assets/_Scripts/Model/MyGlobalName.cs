using UnityEngine;
using System.Collections;

public class MyGlobalName : MonoBehaviour {

	public string myName;


	void Awake(){
		Debug.Log(myName);
	}

}
