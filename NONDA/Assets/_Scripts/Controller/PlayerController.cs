using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	#region Public Variables
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	#endregion

	#region Private Variables
	private Rigidbody2D rigidB2D;
	private bool grounded;
	private bool doubleJumped;
	private Animator animator;
	#endregion


	#region Instances 
	public Player player;
	#endregion

	void Start () {
		player = FindObjectOfType<Player>();
		animator = GetComponent<Animator>();
		rigidB2D = gameObject.GetComponent<Rigidbody2D>();
		rigidB2D.freezeRotation = true;
	}

	void Update () {
		if(grounded)
			doubleJumped = false;

		animator.SetBool("Grounded", grounded);
		
		if(Input.GetKeyDown (KeyCode.Space) && grounded){
			Jump();
		}
		if(Input.GetKeyDown (KeyCode.Space) && !doubleJumped && !grounded){
			DoubleJump();
		}
		if(Input.GetKeyDown (KeyCode.RightArrow)){
			rigidB2D.velocity = new Vector2 (player.MoveSpeed, rigidB2D.velocity.y);
		}
		if(Input.GetKeyDown (KeyCode.LeftArrow)){
			rigidB2D.velocity = new Vector2 (-player.MoveSpeed, rigidB2D.velocity.y);
		}

		animator.SetFloat("Speed", Mathf.Abs (rigidB2D.velocity.x));
		Flip();
	}

	void Flip(){
		if(rigidB2D.velocity.x > 0){
			transform.localScale = new Vector3(1f, 1f, 1f);
		}else if(rigidB2D.velocity.x < 0){
			transform.localScale = new Vector3(-1f, 1f, 1f);
		}
	}

	void Jump(){
		rigidB2D.velocity = new Vector2 (rigidB2D.velocity.x, player.JumpHeight);
	}

	void DoubleJump(){
		rigidB2D.velocity = new Vector2 (rigidB2D.velocity.x, player.JumpHeight);
		doubleJumped = true;
	}

	void FixedUpdate(){
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

//		float h = Input.GetAxis("Horizontal");
//		rigidB2D.AddForce ((Vector2.right * moveSpeed) * h);
	}
}
