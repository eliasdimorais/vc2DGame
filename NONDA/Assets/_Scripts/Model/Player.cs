using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public delegate void DeadEventHandler();

public class Player : Character {

	#region Public Ground Variables
	public Transform groundCheck;
	public Transform[] groundPoints;
	public float groundRadius;
	public LayerMask whatIsGround;
	//public event DeadEventHandler Dead;
	[SerializeField] public uint totalHealth;
	public uint currentHealth;
	public string deadSoundName;
	public string hitSoundName;
	public string jumpSoundName;
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
	private bool canDoubleJump = false;
	private Vector2 fp; // first finger position
	private Vector2 lp; // last finger position
	//private float lpX; //Last position in X
	private int offset = 90; //value where accept touch to calculate swipe
	private bool immortal = true;
	[Range (0f, 30f)][SerializeField]public float immortalTime;
	[SerializeField] private BoxCollider2D boxCollider;

	//[SerializeField]private SpriteRenderer spriteRenderer;
	#endregion


	#region Test platform 
	private float groundBuffer;
//	private float height;
//	private float length;
	//private float topMost;
//	private Vector2 dimensions;
	#endregion

	#region Instances
	private static Player player;

	public static Player Instance{
		get {
			if(player == null){
				player = GameObject.FindObjectOfType<Player>();
			}
			return player;
		}
	}

	public uint Health{
		get{
			return totalHealth;
		}
		set 
		{
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
	    MyRigidBody.freezeRotation = true;
	    currentHealth = totalHealth;


//	    length = transform.localScale.x * ((BoxCollider2D)GetComponent<Collider2D>()).size.x;
//		height = transform.localScale.y * ((BoxCollider2D)GetComponent<Collider2D>()).size.y;

//		dimensions = new Vector2(length, height);

		//get the top part of our collider
		//topMost = transform.position.y + dimensions.y / 2;

	}

	void Update(){
		#if UNITY_ANDROID
			//if((topMost - groundBuffer > groundCheck.position.y){
			//	gameObject.layer = 16;
			//}
			
//			if (currentHealth > totalHealth){
//				currentHealth = totalHealth;
//			}
			if(!IsDead){

				if(Input.GetButtonDown("Horizontal")){
						ChangeDirection();
				}
				if(Input.GetButtonDown("Jump") && isGrounded){
				        Jump();
				}
				if(Input.GetButtonDown("Jump") && !canDoubleJump && !isGrounded){
				        DoubleJump();
				} 
			}  
		#endif
	}
	 
	void FixedUpdate(){
		//Metodo que player move automaticamente
		isGrounded = IsGrounded();
		#if UNITY_ANDROID
		if(!IsDead){
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
						//if(touch.position.x < Screen.width/2){ //is it swiping the left side? so, it's directional controller
							/*if((fp.x - lp.x) > offset && !facingRight || (fp.x - lp.x) < offset * -1 && facingRight ) //I am going to the same direction that the user swipe?
							{
								runNow = true;
								RunTemp(transform.localScale.x); 

							}
							else */
							if ((fp.x - lp.x) > offset && facingRight || (fp.x - lp.x) < offset * -1 && !facingRight ) // right swipe
							{
								ChangeDirection();
							}
						//}

						//if (touch.position.x > Screen.width/2){ //is it swiping the right side? so it's jumping
							if((fp.y - lp.y) < offset * -1 && (isGrounded || !canDoubleJump)) // up swipe
							{
								Jump();
								if(!canDoubleJump && !isGrounded)
									canDoubleJump = true;


							}
						else if(canDoubleJump && !isGrounded && (fp.y - lp.y) < offset * -1){
							DoubleJump();
							canDoubleJump = false;
						}
						//}
					}
				}
			float move = MyRigidBody.velocity.x;
			MyAnimator.SetFloat("Speed", move);

		   // isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

			if(isGrounded)
		        canDoubleJump = false;
		    

			MyRigidBody.velocity = new Vector2(Instance.MoveSpeed, MyRigidBody.velocity.y);
			//MyRigidBody.gravityScale(4);
		}

//		if (Instance.transform.position.x <= minX || Instance.transform.position.x >= maxX){
//			float xPos = Mathf.Clamp(Instance.transform.position.x, minX + 0.1f, maxX);
//			Instance.transform.position = new Vector3(xPos, Instance.transform.position.y,1);
//			ChangeDirection();
//		}
//
//		if (Instance.transform.position.y <=minY || Instance.transform.position.y >=maxY){
//			float yPos = Mathf.Clamp(Instance.transform.position.y, minY + 0.1f, maxY);
//			Instance.transform.position = new Vector3(yPos, Instance.transform.position.x,1);
//		}
		#endif
	}

	private bool IsGrounded(){
		if(MyRigidBody.velocity.y <= 0 ){
			foreach (Transform point in groundPoints) {
				Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

				for(int i = 0; i < colliders.Length; i++){
					if(colliders[i].gameObject != player){
						return true;
					}
				}
			}
		}
		return false;
	}

	public void Jump(){
		MyAnimator.SetTrigger("SetJump");
	    MyRigidBody.velocity = new Vector2 (MyRigidBody.velocity.x, player.JumpForce);
		//AudioManager.Instance.PlaySound(jumpSoundName);
	}

	public void DoubleJump(){
	    MyRigidBody.velocity = new Vector2 (MyRigidBody.velocity.x, player.JumpForce);
		//AudioManager.Instance.PlaySound(jumpSoundName);
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
		//IndicateImmortal();
		if(!IsDead && immortal){
			MyAnimator.SetTrigger("Hit");
			totalHealth -= (uint)damage;
			GameManager.Instance.ChangeHeartSpriteUI(totalHealth);
			//AudioManager.Instance.PlaySound(hitSoundName);
		}else
		if(IsDead){
				Dead();
				GameManager.Instance.LoadLevelClear();
				//AudioManager.Instance.PlaySound(deadSoundName);
		}
 	}
	#endregion
	private IEnumerator IndicateImmortal(){
		yield return new WaitForSeconds(immortalTime);
		//yield return new WaitForEndOfFrame();
		immortal = !immortal;
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
