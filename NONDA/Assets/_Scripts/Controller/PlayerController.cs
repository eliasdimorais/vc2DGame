using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
//	#region Public Ground Variables
//	public Transform groundCheck;
//	public float groundRadius;
//	public LayerMask whatIsGround;
//	public Transform playerC; // Drag your player here
//	#endregion
//
//	#region Private Variables
//	private Vector2 startPosition;
//	private bool grounded;
//	private bool doubleJumped = false;
//	private Vector2 fp; // first finger position
//	private Vector2 lp; // last finger position
//	private float offset = 80; //value where accept touch to calculate swipe
//	#endregion
//
//	#region Instances 
//	private static PlayerController instance;
//	public static PlayerController Instance{
//		get {
//			if(instance == null){
//				instance = GameObject.FindObjectOfType<PlayerController>();
//			}
//			return instance;
//		}
//	}
//	#endregion
//
//	public override void Start () {
//		Debug.Log("PlayerStart");
//		base.Start();
//		startPosition = transform.position;
//		Player = GetComponent<Rigidbody2D>();
//	    rigidB2D.freezeRotation = true;
//	}
//
//	void Update(){
//	#if UNITY_ANDROID
//		foreach(Touch touch in Input.touches){
//			if (touch.phase == TouchPhase.Began){
//				fp = touch.position;
//				lp = touch.position;
//			}
//			if (touch.phase == TouchPhase.Moved ){
//				lp = touch.position;
//			}
//			if(touch.phase == TouchPhase.Ended){
//				if((fp.x - lp.x) > offset) // left swipe
//				{
//					Flip(lp);
//				}
//				else if((fp.x - lp.x) < offset * -1) // right swipe
//				{
//					MoveSpeed *= -1;
//					Flip(lp);
//				}
//				else if((fp.y - lp.y) < offset * -1  ) // up swipe
//				{
//					Jump();
//				}
//			}
//		}
//		if(Input.touchCount >0){
//			Touch touch = Input.GetTouch(0);
//			if(touch.phase == TouchPhase.Moved){
//				
//			}
//		}
//	#elif UNITY_EDITOR
//		if(Input.GetButtonDown("Jump") && grounded){
//	        Jump();
//	    }
//	    if(Input.GetButtonDown("Jump") && !doubleJumped && !grounded){
//	        DoubleJump();
//	    }
//	#endif
//	}
//
//	//Metodo para controlar jogador usando teclas (botao no UI) 
//	void FixedUpdate(){
//	#if UNITY_STANDALONE
//		float move = Input.GetAxis ("Horizontal");
//		animator.SetFloat("Speed", Mathf.Abs(move));
//		rigidB2D.velocity = new Vector2(move * player.MoveSpeed, rigidB2D.velocity.y);
//
//		if(move > 0 && !player.FacingRight){
//			Flip();
//		}else if (move < 0 && player.FacingRight){
//			Flip();
//		}
//	#endif
//
//	//Metodo que player move automaticamente
//	#if UNITY_ANDROID
//		float move = rigidB2D.velocity.x;
//		MyAnimator.SetFloat("Speed", move);
//
//	    grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
//
//	    MyAnimator.SetBool("Grounded", grounded);
//
//		if(grounded)
//	        doubleJumped = false;
//
//		rigidB2D.velocity = new Vector2(instance.MoveSpeed, rigidB2D.velocity.y);
//
//		if(!grounded) return;
//
//		MyAnimator.SetFloat("Speed", rigidB2D.velocity.x);
//	#endif
//	}
//
//	void Flip(Vector2 horizontal){
//		if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight){
//			ChangeDirection();
//		}
//	}
//
//	void Jump(){
//		MyAnimator.SetBool("Grounded", false);
//		//rigidB2D.AddForce(new Vector2(0, player.JumpHeight));
//	    rigidB2D.velocity = new Vector2 (rigidB2D.velocity.x, instance.JumpHeight);
//	}
//
//	void DoubleJump(){
//	    rigidB2D.velocity = new Vector2 (rigidB2D.velocity.x, instance.JumpHeight);
//	    doubleJumped = true;
//	}
//
//	void OnTriggerEnter2D (Collider2D collider){
//		var tag = collider.gameObject.tag; 
//		if (tag == "Wall"){
//			Flip(lp);
//		}
//	}
}
