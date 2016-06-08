using UnityEngine;
using System.Collections;

public class Enemy : Character {

	#region Private variable
	private IEnemyState currentState;
	[SerializeField]private float fightRange;
	[SerializeField] private EdgeCollider2D beakCollider;
	[SerializeField] private float inicialSpeed;
	[SerializeField] protected int damageValueOnPlayer;
	private float lastDamage = 0;

	public int touchCount;
	public enum EnemyType{BIRD, LEECH, ANT}
	public EnemyType enemyType;
	public int score;
	#endregion
	#region Public variable
	public GameObject Target { get; set;}

	public bool InAttackMode{
		get
		{
			if(Target != null){
				return Vector2.Distance(transform.position, Target.transform.position) <= fightRange; 
			}
		return false;
		}
	}
	#endregion

	public override void Start () {
		base.Start();
		//Player.Instance.Dead += new DeadEventHandler(RemoveTarget);
		ChangeState(new IdleState());
		//takingDamage = MyAnimator.GetComponent<Character>().TakingDamage = false;
	}

	void Update () {
		if(!IsDead){
			currentState.Execute();
			FollowTarget();
		}
		MoveSpeed = inicialSpeed; //change because every frame called this moveSpeed in order to make the enmy stop Speed while idling
	}

	public void Move(){
		if(!Attack && !TakingDamage){
			MyAnimator.SetFloat("Speed", MoveSpeed);
			transform.Translate(GetDirection() * (inicialSpeed * Time.deltaTime)); //move functions
		}
	}

	public void ChangeState(IEnemyState newState){
		if(currentState != null){
			currentState.Exit();
		}
		currentState = newState;

		currentState.Enter(this);
	}  

	public Vector2 GetDirection(){
		return facingRight ? Vector2.right : Vector2.left;
	}

	private void FollowTarget(){
		if(Target != null){
			float xDir = Target.transform.position.x - transform.position.x;
			if(xDir < 0 && facingRight || xDir > 0 && !facingRight){
					ChangeDirection();
			}
		}
	}

	public void RemoveTarget(){
		Target = null;
		ChangeState(new SearchState());
	}

	void OnTriggerEnter2D(Collider2D other){
		var tag = other.gameObject.tag;
		if (tag == "Edge"){
			ChangeDirection();
		}
//		if(tag == "Player" ){
//			Player.Instance.DealDamage(damageValueOnPlayer);
//			CharacterCollider.enabled = false;
//		}
		//avoid add force to each other
		if(tag == "Bird"){
			CharacterCollider.enabled = false;
		}
	}

	void OnTriggerStay2D(Collider2D other){
		var tag = other.gameObject.tag; 
		lastDamage += Time.deltaTime; 
		if(lastDamage >= Player.Instance.immortalTime && tag == "Player"){
			Player.Instance.DealDamage(damageValueOnPlayer);
			lastDamage = 0;
		}	

	}

	void OnTriggerExit2D(Collider2D other){
		var tag = other.gameObject.tag;
		//let the bird go and enable
		if(tag == "Bird"){
			CharacterCollider.enabled = true;
		}
		if(tag == "Player"){
			CharacterCollider.enabled = true;
		}
	}


	#region implemented abstract members of Character
	public override bool IsDead {
		get {
			return touchCount <= 0;
		}
	}
	#endregion

	// Touch Class
	void OnMouseDown () {
		TakingDamage = true;
		if(TakingDamage){
			touchCount--;
			gameObject.GetComponent<Animator>().SetTrigger("Damage");
			if(touchCount <= 0){
				GameManager.Instance.UpdateScore(score);
				gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				switch (enemyType){
					case EnemyType.ANT:
						break;
					case EnemyType.BIRD:
						gameObject.GetComponent<Animator>().SetBool("isDead", true); //I call Destroy Enemy Inside animation trough Event
						gameObject.SetActive(false);
						break;
					case EnemyType.LEECH:
						break;
				}
			}
		}
		
	}

	void OnMouseUp(){
		gameObject.GetComponent<Animator>().ResetTrigger("Damage"); //I can call trough event as well
 		TakingDamage = false;
	}

	void DestroyEnemy(){
		DestroyObject(this);
	}
}
