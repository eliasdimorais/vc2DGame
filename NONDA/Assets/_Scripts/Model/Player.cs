using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	#region Public Variables

	#endregion

	#region Private Variables
	private Rigidbody2D rigidB2D;
	[Range (-3f, 3f)][SerializeField] private float moveSpeed;
	[Range (0f, 18f)][SerializeField] private float jumpHeight;
	//[Range (-1f, 3f)][SerializeField] private float moveVelocity;
	private bool facingRight = true;
	#endregion

	public float MoveSpeed{
		get {return moveSpeed;}
		set {moveSpeed = value;}
	}

	public float JumpHeight{
		get {return jumpHeight;}
		set {jumpHeight = value;}
	}

	public bool FacingRight{
		get {return facingRight;}
		set {facingRight = value;}
	}

	public float Life{get; set;}

}
