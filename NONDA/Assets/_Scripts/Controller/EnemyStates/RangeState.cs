using UnityEngine;
using System.Collections;

public class RangeState : IEnemyState {
	private Enemy enemy;
//	private float attackTimer;
//	private float attackCoolDown = 3;
//	private bool canAttack = true;

	void Start () {
	
	}
	
	void Update () {
	
	}

	#region IEnemyState implementation

	public void Execute ()
	{
		if (enemy.Target != null){
			enemy.Move();
			Debug.Log("Inimigo "+ enemy.name);
		}
		else if(enemy.InAttackMode){
			enemy.ChangeState(new AttackController());
		}
		else{
			enemy.ChangeState(new IdleState());
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
	#endregion


}
