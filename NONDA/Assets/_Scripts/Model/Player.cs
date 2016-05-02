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
	private bool grounded;
	private bool doubleJumped = false;
	private Vector2 fp; // first finger position
	private Vector2 lp; // last finger position
	private float lpX;
	private float offset = 80; //value where accept touch to calculate swipe
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
		grounded = true;
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

	    grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

	    //MyAnimator.SetBool("Grounded", grounded);

		if(grounded)
	        doubleJumped = false;

		MyRigidBody.velocity = new Vector2(Instance.MoveSpeed, MyRigidBody.velocity.y);

		//if(!grounded) return;

		MyAnimator.SetFloat("Speed", MyRigidBody.velocity.x);
	#endif
	}

	void Jump(){
		MyAnimator.SetTrigger("SetJump");
		//MyAnimator.SetBool("Grounded", false);
		//rigidB2D.AddForce(new Vector2(0, player.JumpHeight));
	    MyRigidBody.velocity = new Vector2 (MyRigidBody.velocity.x, instance.JumpForce);
	}

	void DoubleJump(){
	    MyRigidBody.velocity = new Vector2 (MyRigidBody.velocity.x, instance.JumpForce);
	    doubleJumped = true;
	}

	void OnTriggerEnter2D (Collider2D other){
		var tag = other.gameObject.tag; 
		if (tag == "Wall"){
			ChangeDirection();
		}
	} 

	void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(groundCheck.position, groundRadius);
    }

	#region implemented abstract members of Character
	public override IEnumerator TakeDamage ()
	{
		yield return null;
	}

	public override bool IsDead {
		get {
			return health <= 0;
		}
	}

	#endregion
}
