using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public delegate void DeadEventHandler();

public class Player : Character {

	#region Public Ground Variables
	public Transform groundCheck;
	public float groundRadius;
	public LayerMask whatIsGround;
	//public event DeadEventHandler Dead;
	[SerializeField] public uint totalHealth;
	public uint currentHealth;
	#endregion

	#region Private Variables
	private AudioSource audioSource;
	[SerializeField] private float minX;
	[SerializeField] private float maxX;
	[SerializeField] private float minY;
	[SerializeField] private float maxY;
	//private Vector2 startPosition;
	private bool isGrounded;
	[Range (0f, 18f)][SerializeField] private float jumpForce;
	private bool doubleJumped = false;
	private Vector2 fp; // first finger position
	private Vector2 lp; // last finger position
	//private float lpX; //Last position in X
	private int offset = 70; //value where accept touch to calculate swipe
	private float moveSpeedTemp;
	private bool immortal = true;
	[Range (0f, 3f)][SerializeField]private float immortalTime;
	//[SerializeField]private SpriteRenderer spriteRenderer;
	#endregion

	#region Instances 
	private static Player instance;

	public static Player Instance{
		get {
			if(instance == null){
				instance = GameObject.FindObjectOfType<Player>();
			}
			return instance;
		}
	}

	public uint Health{
		get{
			return totalHealth;
		}
		set 
		{
			//healthText.text = value.ToString("000");
			this.totalHealth = value;
		}
	}

	public float JumpForce{
		get {return jumpForce;}
		set {jumpForce = value;}
	}
	#endregion

	public override void Start () {
		base.Start();
		//startPosition = transform.position;
		//spriteRenderer = GetComponent<SpriteRenderer>();
		isGrounded = true;
	    MyRigidBody.freezeRotation = true;
	    currentHealth = totalHealth;
	}

	void Update(){
		#if UNITY_ANDROID
			if (currentHealth > totalHealth){
				currentHealth = totalHealth;
			}
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

			if (Instance.transform.position.x <= minX || Instance.transform.position.x >= maxX){
				float xPos = Mathf.Clamp(Instance.transform.position.x, minX + 0.1f, maxX);
				Instance.transform.position = new Vector3(xPos, Instance.transform.position.y,1);
				Debug.Log("Entrou max ou min em X");
				ChangeDirection();
			}

			if (Instance.transform.position.y <=minY || Instance.transform.position.y >=maxY){
				float yPos = Mathf.Clamp(Instance.transform.position.y, minY + 0.1f, maxY);
				Instance.transform.position = new Vector3(yPos, Instance.transform.position.x,1);

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
			//MyRigidBody.gravityScale(4);
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
	public void DealDamage (int damage)
	{	
		if(!IsDead){
			IndicateImmortal();
			MyAnimator.SetTrigger("Hit");
			GameManager.Instance.ChangeHeartSpriteUI(totalHealth);
			totalHealth -= (uint)damage;
			//AudioSource.PlayClipAtPoint(playerDeathSound, transform.position);
		}else{
			Dead();
		}
	}
	#endregion
	private IEnumerator IndicateImmortal(){
		immortal = !immortal;
		yield return new WaitForSeconds(immortalTime);

//		while (!immortal){
//			spriteRenderer.enabled = false;
//			yield return new WaitForSeconds(.1f);
//			spriteRenderer.enabled = true;
//			yield return new WaitForSeconds (.1f);
//		}
	}

	public override bool IsDead {
		get {
			if(totalHealth <= 0){
				Dead();
			}

			return totalHealth <= 0;
		}
	}

	public void Dead(){
		MyAnimator.SetTrigger("Dead");
	}

	/*public void OnDead(){
		if(Dead != null){
			instance.MyAnimator.SetTrigger("Dead");
			Dead();
		}
	}*/
}
