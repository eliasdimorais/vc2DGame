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
	private bool facingRight;
	private Animator animator;
	#endregion


	#region Instances 
	public Player player;
	#endregion

	void Start () {
	    facingRight = true;
	    player = FindObjectOfType<Player>();
	    animator = GetComponent<Animator>();
	    rigidB2D = gameObject.GetComponent<Rigidbody2D>();
	    rigidB2D.freezeRotation = true;
	}

	void FixedUpdate(){
	    grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
	    float horizontal = Input.GetAxis("Horizontal");

	    if(grounded)
	        doubleJumped = false;

	    animator.SetBool("Grounded", grounded);

	    if(Input.GetKeyDown (KeyCode.Space) && grounded){
	        Jump();
	    }
	    if(Input.GetKeyDown (KeyCode.Space) && !doubleJumped && !grounded){
	        DoubleJump();
	    }

	    HandleMovement(horizontal);
	    FlipHorizontal(horizontal);
	}

	private void HandleMovement(float horizontal){
	    rigidB2D.velocity = new Vector2(horizontal * player.MoveSpeed, rigidB2D.velocity.y);
	    animator.SetFloat("Speed", Mathf.Abs (horizontal));
	}

	private void FlipHorizontal(float horizontal){
	    if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight){
	        facingRight = !facingRight;
	        Vector3 flipScale = transform.localScale;
	        transform.localScale = flipScale;
	        Debug.Log("Entry here to flip");
	    }
	}

	void Jump(){
	    rigidB2D.velocity = new Vector2 (rigidB2D.velocity.x, player.JumpHeight);
	}

	void DoubleJump(){
	    rigidB2D.velocity = new Vector2 (rigidB2D.velocity.x, player.JumpHeight);
	    doubleJumped = true;
	}
}