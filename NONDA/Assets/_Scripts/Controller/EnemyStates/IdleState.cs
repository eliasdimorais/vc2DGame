 using UnityEngine;
using System.Collections;

public class IdleState : IEnemyState {
	#region Private
	private Enemy enemy;
	private float idleTimer;
	private float idleDuration = 3f; //in seconds
	#endregion

	void Start () {
	
	}

	void Update () {
	
	}

	private void Idle(){
		enemy.MyAnimator.SetFloat("Speed", 0);
		idleTimer += Time.deltaTime;
		if(idleTimer >= idleDuration){
			enemy.ChangeState(new SearchState());
		}
	}
	#region IEnemyState implementation

	public void Execute ()
	{
		//Debug.Log ("Im idling");
		Idle();

		if(enemy.Target != null){
			enemy.ChangeState(new SearchState()); //is already moving and its change to RangeState if necessary
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
