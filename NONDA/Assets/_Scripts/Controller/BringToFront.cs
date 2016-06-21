using UnityEngine;
using System.Collections;

public class BringToFront : MonoBehaviour {

	void onEnable(){
		transform.SetAsLastSibling(); 
	}
}
