using UnityEngine;
using System.Collections;

public delegate void DeadEventHandler();

public class Player : Character {

	#region Public Ground Variables
	public Transform groundCheck;
	public float groundRadius;
	public LayerMask whatIsGround;
	public event DeadEventHandler Dead;
	#endregion

	#region Private Variables
//	private Vector2 startPosition;
	private bool isGrounded;
	private bool doubleJumped = false;
	private Vector2 fp; // first finger position
	private Vector2 lp; // last finger position
	//private float lpX; //Last position in X
	private int offset = 70; //value where accept touch to calculate swipe
	private float moveSpeedTemp;
	private bool immortal = false;
	[Range (0f, 3f)][SerializeField]private float immortalTime;
	[SerializeField]private SpriteRenderer spriteRenderer;

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
		//startPosition = transform.position;
		spriteRenderer = GetComponent<SpriteRenderer>();
		isGrounded = true;
	    MyRigidBody.freezeRotation = true;

	}

	void Update(){
		#if UNITY_ANDROID
			if(!IsDead){
			moveSpeedTemp = 0f;
			foreach(Touch touch in Input.touches){
				if (touch.phase == TouchPhase.Began){
					fp = touch.position;
					lp = touch.position;
					//lpX = lp.x;
				}
				if (touch.phase == TouchPhase.Moved ){
					lp = touch.position;
				}
				if(touch.phase == TouchPhase.Ended){
					if(touch.position.x < Screen.width/2){ //is it swiping the left side? so, it's directional controller
						if((fp.x - lp.x) > offset && !facingRight || (fp.x - lp.x) < offset * -1 && facingRight ) //I am going to the same direction that the user swipe?
						{ 
							moveSpeedTemp = 0.2f;
							MoveSpeed  = MoveSpeed + moveSpeedTemp;
						}
						else if ((fp.x - lp.x) > offset && facingRight || (fp.x - lp.x) < offset * -1 && !facingRight ) // right swipe
						{
							ChangeDirection();
						}
					}

					if (touch.position.x > Screen.width/2){ //is it swiping the right side? so it's jumping
						if((fp.y - lp.y) < offset * -1  ) // up swipe
						{
							Jump();
						}
					}
				}
			}
			if(Input.GetButtonDown("Horizontal")){
					ChangeDirection();
			}
			if(Input.GetButtonDown("Jump") && isGrounded){
			        Jump();
			}
			if(Input.GetButtonDown("Jump") && !doubleJumped && !isGrounded){
			        DoubleJump();
			} 
		}  
		#endif
	}
	//Metodo para controlar jogador usando teclas (botao no UI) 
	void FixedUpdate(){
		//Metodo que player move automaticamente
		#if UNITY_ANDROID
		if(!IsDead){
			float move = MyRigidBody.velocity.x;
			MyAnimator.SetFloat("Speed", move);

		    isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

			if(isGrounded)
		        doubleJumped = false;

			MyRigidBody.velocity = new Vector2(Instance.MoveSpeed, MyRigidBody.velocity.y);

			MyAnimator.SetFloat("Speed", MyRigidBody.velocity.x);
		}
		#endif
	}

	public void Jump(){
		MyAnimator.SetTrigger("SetJump");
	    MyRigidBody.velocity = new Vector2 (MyRigidBody.velocity.x, instance.JumpForce);

		//MyAnimator.SetBool("Grounded", false);
		//rigidB2D.AddForce(new Vector2(0, player.JumpHeight));
	}

	public void DoubleJump(){
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
	public override IEnumerator DealDamage (uint damage)
	{
		Debug.Log("Suzaninha");

		if(!immortal){
			totalLife -= damage;
			if(!IsDead){
				immortal = true;
				yield return new WaitForSeconds(immortalTime);
				immortal = false;
				MyAnimator.SetTrigger("Hit");
			}
		}else{
			MyAnimator.SetLayerWeight(1,0);
			MyAnimator.SetTrigger("Dead");
		} 
	}
	#endregion
	private IEnumerator IndicateImmortal(){
		while (!immortal){
			spriteRenderer.enabled = false;
			yield return new WaitForSeconds(.1f);
			spriteRenderer.enabled = true;
			yield return new WaitForSeconds (.1f);
		}
	}

	public override bool IsDead {
		get {
			if(totalLife <= 0){
				OnDead();
			}

			return totalLife <= 0;
		}
	}

	public void OnDead(){
		if(Dead != null){
			Dead();
		}
	}
}
