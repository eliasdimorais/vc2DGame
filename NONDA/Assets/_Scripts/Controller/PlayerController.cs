using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	#region Public Ground Variables
	public Transform groundCheck;
	public float groundRadius;
	public LayerMask whatIsGround;
	public float facingRight;
	public Transform playerC; // Drag your player here
	#endregion

	#region Private Variables
	private Rigidbody2D rigidB2D;
	private bool grounded;
	private bool doubleJumped = false;
	private Animator animator;
	private Vector2 fp; // first finger position
	private Vector2 lp; // last finger position
	private float offset = 80; //value where accept touch to calculate swipe
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
	#if UNITY_ANDROID
		foreach(Touch touch in Input.touches){
			if (touch.phase == TouchPhase.Began){
				fp = touch.position;
				lp = touch.position;
			}
			if (touch.phase == TouchPhase.Moved ){
				lp = touch.position;
			}
			if(touch.phase == TouchPhase.Ended){
				if((fp.x - lp.x) > offset) // left swipe
				{
					//playerC.Rotate(0,-90,0);
					Flip();
				}
				else if((fp.x - lp.x) < offset * -1) // right swipe
				{
					//playerC.Rotate(0,90,0);
					Flip();
				}
				else if((fp.y - lp.y) < offset * -1  ) // up swipe
				{
					Jump();
				}
			}
		}
		if(Input.touchCount >0){
			Touch touch = Input.GetTouch(0);
			if(touch.phase == TouchPhase.Moved){
				
			}
		}
	#elif UNITY_EDITOR
		if(Input.GetButtonDown("Jump") && grounded){
	        Jump();
	    }
	    if(Input.GetButtonDown("Jump") && !doubleJumped && !grounded){
	        DoubleJump();
	    }
	#endif
	}

	void Flip(){
	#if UNITY_ANDROID 
		player.MoveSpeed *= -1; //change Speed to eighter negative or positive

		player.FacingRight = !player.FacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		rigidB2D.MoveRotation(rigidB2D.rotation + player.MoveSpeed * Time.fixedDeltaTime);
	#endif
	}


	void Jump(){
		animator.SetBool("Grounded", false);
		//rigidB2D.AddForce(new Vector2(0, player.JumpHeight));
	    rigidB2D.velocity = new Vector2 (rigidB2D.velocity.x, player.JumpHeight);
	}

	void DoubleJump(){
	    rigidB2D.velocity = new Vector2 (rigidB2D.velocity.x, player.JumpHeight);
	    doubleJumped = true;
	}

	//Metodo para controlar jogador usando teclas (botao no UI) 
	void FixedUpdate(){
	#if UNITY_STANDALONE
		float move = Input.GetAxis ("Horizontal");
		animator.SetFloat("Speed", Mathf.Abs(move));
		rigidB2D.velocity = new Vector2(move * player.MoveSpeed, rigidB2D.velocity.y);

		if(move > 0 && !player.FacingRight){
			Flip();
		}else if (move < 0 && player.FacingRight){
			Flip();
		}
	#endif

	//Metodo que player move automaticamente
	#if UNITY_ANDROID
		float move = rigidB2D.velocity.x;
		animator.SetFloat("Speed", move);

	    grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

	    animator.SetBool("Grounded", grounded);

		if(grounded)
	        doubleJumped = false;

		rigidB2D.velocity = new Vector2(player.MoveSpeed, rigidB2D.velocity.y);

		if(!grounded) return;

		animator.SetFloat("Speed", rigidB2D.velocity.x);
	#endif
	}

	void OnTriggerEnter2D (Collider2D collider){
		var tag = collider.gameObject.tag; 
		if (tag == "Wall"){
			Flip();
		}
	}
}
