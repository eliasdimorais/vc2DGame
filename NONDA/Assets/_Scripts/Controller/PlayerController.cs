using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	#region Public Ground Variables
	public Transform groundCheck;
	public float groundRadius;
	public LayerMask whatIsGround;
	public float facingRight;
	#endregion

	#region Private Variables
	private Rigidbody2D rigidB2D;
	private bool grounded = false;
	private bool doubleJumped = false;
	private Animator animator;

	//private Vector2 touchOrigin = -Vector2.one;
	#endregion

	#region Instances 
	public Player player;
	#endregion

	void Start () {
	    player = FindObjectOfType<Player>();
	    animator = GetComponent<Animator>();
	    rigidB2D = GetComponent<Rigidbody2D>();
	    rigidB2D.freezeRotation = true;

	}

	void Update(){
	    if(Input.GetButtonDown("Jump") && grounded){
	        Jump();
	    }

	    if(Input.GetButtonDown("Jump") && !doubleJumped && !grounded){
	        DoubleJump();
	    }
	}

	void RotateLeft() {
    	transform.Rotate (Vector3.back * -5f);
	}

	void RotateRight() {
    transform.Rotate (Vector3.forward * 5f);
}

	void Flip(){
		player.FacingRight = !player.FacingRight;
		player.MoveSpeed *= -1;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		rigidB2D.MoveRotation(rigidB2D.rotation + player.MoveSpeed * Time.fixedDeltaTime);


	}

	void Jump(){
		animator.SetBool("Grounded", false);
		rigidB2D.AddForce(new Vector2(0, player.JumpHeight));
	    //rigidB2D.velocity = new Vector2 (rigidB2D.velocity.x, player.JumpHeight);
		Debug.Log("Jumping at - " + rigidB2D.velocity.y);



	}

	void DoubleJump(){
	    rigidB2D.velocity = new Vector2 (rigidB2D.velocity.x, player.JumpHeight);
	    doubleJumped = true;
	}

	//Metodo para controlar jogador usando teclas (botao no UI) 
//	void FixedUpdate(){
//		float move = Input.GetAxis ("Horizontal");
//		animator.SetFloat("Speed", Mathf.Abs(move));
//		rigidB2D.velocity = new Vector2(move * player.MoveSpeed, rigidB2D.velocity.y);
//
//		if(move > 0 && !player.FacingRight){
//			Flip();
//		}else if (move < 0 && player.FacingRight){
//			Flip();
//		}
//	}

	//Metodo que executa automaticamente
	void FixedUpdate(){
		float move = rigidB2D.velocity.x;
		animator.SetFloat("Speed", move);

	    grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

	    animator.SetBool("Grounded", grounded);

		if(grounded)
	        doubleJumped = false;

	   // animator.SetFloat("vSpeed", rigidB2D.velocity.y);

		rigidB2D.velocity = new Vector2(player.MoveSpeed, rigidB2D.velocity.y);


		 

		//if(!grounded) return;

		animator.SetFloat("Speed", rigidB2D.velocity.x);
	}

	void OnTriggerEnter2D (Collider2D collider){
	   var tag = collider.gameObject.tag; 
	   // Debug.Log("Checked into the Shredder");
		if (tag == "Wall"){
			Flip();

		}
	}
}
