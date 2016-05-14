﻿using UnityEngine;
using System.Collections;

public class SearchState : IEnemyState {
	private Enemy enemy;
	private float searchTimer;
	private float searchDuration = 10f;
	 
	void Start () {
	
	}

	void Update () {
	
	}

	private void Search(){
		searchTimer += Time.deltaTime;
		if(searchTimer >= searchDuration){
			enemy.ChangeState(new IdleState());
		}
	}

	#region IEnemyState implementation
	public void Execute ()
	{
		//Debug.Log("Searching State");
		Search();
		enemy.Move();
		if(enemy.Target != null && !enemy.InAttackMode){ //&& get closes to attack
			enemy.ChangeState(new RangeState());
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
		if (other.tag == "Edge"){
			enemy.ChangeDirection();
		}	
	}
	#endregion
}
