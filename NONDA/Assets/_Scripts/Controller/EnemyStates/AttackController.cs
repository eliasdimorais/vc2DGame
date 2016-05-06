using UnityEngine;
using System.Collections;

public class AttackController : IEnemyState {
	//Deveria CHamar attackBehaviour para adicionar junto a animação Attack
	public LevelManager levelManager;

	#region Private variables
	//AttackState
	[Range (-1.0f,2.0f)]
	//[SerializeField]private float currentSpeed = 1.0f;
	private Enemy enemy;
	private float attackTimer;
	private float attackCoolDown = 3;
	private bool canAttack = true;
	#endregion

	void Start () {
		//levelManager = FindObjectOfType<LevelManager>();
	}

	void Update (){
		//transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.name == "Player"){
			levelManager.RespawnPlayer();
		}
	}

	#region IEnemyState implementation
	//to attack the player taking damage 
	public void Execute ()
	{
		if(enemy.InAttackMode){
			Attack();
		}
	}

	public void Enter (Enemy enemy)
	{
		this.enemy = enemy;
	}

	public void Exit ()
	{
		
	}

	public void OnTriggerEnter (Collider2D other)
	{
		
	}
	#endregion]

	public void Attack(){
		attackTimer += Time.deltaTime;

		if(attackTimer >= attackCoolDown){
			canAttack = true;
			attackTimer = 0;
		}
		if(canAttack){
			canAttack = false;
			enemy.MyAnimator.SetTrigger("Attack");
			Debug.Log ("I started attacking you");
		}
	}

	public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int hi){
		animator.GetComponent<Character>().Attack = true;
		animator.SetFloat("Speed", 1);
//		if(animator.tag == "Player"){
//			if(Player.Instance.OnGround){
//				Player.Instance.MyRigidBody.velocity = Vector2.zero;
//			}
//		}
	}

	public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int hi){
			animator.GetComponent<Character>().Attack = false;
			animator.ResetTrigger("Attack");
	}


}
