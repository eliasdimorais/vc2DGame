using UnityEngine;
using System.Collections;

public class Enemy : Character {

	#region Private variable
	private IEnemyState currentState;
	[SerializeField]private float fightRange;
	[SerializeField] private EdgeCollider2D beakCollider;
	[SerializeField] private float inicialSpeed;
	[SerializeField] private uint damageValueOnPlayer;

	public int touchCount;
	public enum EnemyType{BIRD, LEECH, ANT}
	public EnemyType enemyType;
	public uint points;
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
		Player.Instance.Dead += new DeadEventHandler(RemoveTarget);
		ChangeState(new IdleState());
	}

	void Update () {
		if(!IsDead /*&& !GameTimer.IsTimerOut()*/){
			//if(!TakingDamage){
				currentState.Execute();
			//}
			FollowTarget();
		}
	}

	public void Move(){
		if(!Attack){
			MyAnimator.SetFloat("Speed", 1);
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
	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.tag == "Player"){
			Debug.Log("Entrou no Dano, Colidiu com a Suzana.. aiaiai");
			Player.Instance.DealDamage(damageValueOnPlayer);
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		//base.OnTriggerEnter2D(other);
		var tag = other.gameObject.tag; 
		if (tag == "Edge"){
			ChangeDirection();
		}

	}

	#region implemented abstract members of Character

	public override IEnumerator DealDamage (uint damage)
	{
		if(!IsDead){
			MyAnimator.SetTrigger("Damage");
			Debug.Log("Damage");
		}
		else{
			MyAnimator.SetTrigger("Dead");
			Debug.Log("Dead");
			yield return null;
		}
     }	

	public override bool IsDead {
		get {
			return touchCount <= 0;
		}
	}
	#endregion

	// Touch Class
	void OnMouseDown () {
		touchCount--;
		Debug.Log(touchCount);
		gameObject.GetComponent<Animator>().SetTrigger("Damage");
		if(touchCount <= 0){
			GameManager.Instance.UpdateScore(points);

			switch (enemyType){
				case EnemyType.ANT:
					break;
				case EnemyType.BIRD:
					gameObject.GetComponent<Animator>().SetBool("isDead", true);
					gameObject.SetActive(false);
					//DestroyEnemy();
					break;
				case EnemyType.LEECH:
					break;
				
			}
		}
	}

	void OnMouseUp(){
		gameObject.GetComponent<Animator>().SetTrigger("Attack");
	}

	void DestroyEnemy(){
		DestroyObject(this);
	}

}
