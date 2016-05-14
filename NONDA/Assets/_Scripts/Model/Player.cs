using UnityEngine;
using System.Collections;

public class Player : Character {

	#region Public Ground Variables
	public Transform groundCheck;
	public float groundRadius;
	public LayerMask whatIsGround;
	#endregion

	#region Private Variables
	private Vector2 startPosition;
	private bool isGrounded;
	private bool doubleJumped = false;
	private Vector2 fp; // first finger position
	private Vector2 lp; // last finger position
	private float lpX; //Last position in X
	private float offset = 70; //value where accept touch to calculate swipe
	#endregion

	#region Instances 
	private static Player instance;
	public static Player Instance{
		get {
			if(instance == null){
				instance = GameObject.FindObjectOfType<Player>();
				//Debug.Log("Pass here");
			}
			return instance;
		}
	}
	#endregion

	public override void Start () {
		base.Start();
		startPosition = transform.position;
		isGrounded = true;
	    MyRigidBody.freezeRotation = true;
	}

	void Update(){
	#if UNITY_ANDROID
		foreach(Touch touch in Input.touches){
			if (touch.phase == TouchPhase.Began){
				fp = touch.position;
				lp = touch.position;
				lpX = lp.x;
			}
			if (touch.phase == TouchPhase.Moved ){
				lp = touch.position;
			}
			if(touch.phase == TouchPhase.Ended){
				if((fp.x - lp.x) > offset) // left swipe
				{
					ChangeDirection();
					//MyRigidBody.MoveRotation(MyRigidBody.rotation + Instance.MoveSpeed * Time.fixedDeltaTime);
				}
				else if((fp.x - lp.x) < offset * -1) // right swipe
				{
					ChangeDirection();
					//MyRigidBody.MoveRotation(MyRigidBody.rotation + Instance.MoveSpeed * Time.fixedDeltaTime);
				}
				else if((fp.y - lp.y) < offset * -1  ) // up swipe
				{
					Jump();
				}
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
		float move = MyRigidBody.velocity.x;
		MyAnimator.SetFloat("Speed", move);

	    isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

	    //MyAnimator.SetBool("Grounded", isGrounded);
		//if(!isGrounded) return;

		if(isGrounded)
	        doubleJumped = false;

		MyRigidBody.velocity = new Vector2(Instance.MoveSpeed, MyRigidBody.velocity.y);

		MyAnimator.SetFloat("Speed", MyRigidBody.velocity.x);
	#endif
	}

	void Jump(){
		MyAnimator.SetTrigger("SetJump");
	    MyRigidBody.velocity = new Vector2 (MyRigidBody.velocity.x, instance.JumpForce);

		//MyAnimator.SetBool("Grounded", false);
		//rigidB2D.AddForce(new Vector2(0, player.JumpHeight));
	}

	void DoubleJump(){
	    MyRigidBody.velocity = new Vector2 (MyRigidBody.velocity.x, instance.JumpForce);
	    doubleJumped = true;
	}

	//collide wit the Wall
	void OnTriggerEnter2D (Collider2D other){
		var tag = other.gameObject.tag; 
		if (tag == "Wall"){
			ChangeDirection();
		}
	} 

	//check groundcheck
	void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(groundCheck.position, groundRadius);
    }

	#region implemented abstract members of Character
	public override IEnumerator TakeDamage ()
	{
		//yield return null;
		//new script
		MyAnimator.SetTrigger("Hit");
		totalLife -= enemyDamageValue; 
		yield return totalLife; 
	}	

	public override bool IsDead {
		get {
			return totalLife <= 0;
		}
	}
	#endregion

}
