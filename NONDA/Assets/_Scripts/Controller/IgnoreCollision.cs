using UnityEngine;
using System.Collections;

public class IgnoreCollision : MonoBehaviour {

	#region Public Ground Variables

	#endregion

	#region Private Variables
	[SerializeField] private Collider2D other;
	#endregion

	#region Instances 

	#endregion

	private void Awake(){
		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
	}
}