﻿using UnityEngine;
using System.Collections;

public class SpawnerController : MonoBehaviour {

	#region Public Variables
	public enum SpawnState{SPAWNING, WAITING, COUNTING};
	[System.Serializable]
	public class Wave{
		public string name; //nome da Wave (para Enemies)
		public Transform enemy; // Referencia para instanciar o objeto (Enemies, player, item)
		public int count; //armazena a quantidade do contador
		public float rate = 2f; // taxa que eu quero que apareca
	}
	public Wave[] waves;
	private GameObject parent;
	//private Enemy enemies;

	public float timeBetweenWaves = 5f;
	public float waveCountdown;
	public Transform[] enemySpawnPoint;
	#endregion

	#region Private Variables
	private float searchCountdown = 1f; //Time between search is in seconds 
	private int nextWave = 0;
	private SpawnState state = SpawnState.COUNTING;
	#endregion


	void Start(){
//		parent = GameObject.Find ("Enemies");
//		Debug.Log(parent);
//		enemies = GameObject.FindObjectOfType<Enemy>();
//	
//		if (!parent) {
//			parent = new GameObject("Enemies");
//		}

		if(waves.Length == 0){
			Debug.LogError("No WAVES HAS BEEN FOUND. ");
		}

		if(enemySpawnPoint.Length == 0){
			Debug.LogError("No enemy Spawn referenced");
		}

		waveCountdown = timeBetweenWaves;
	}

	void Update(){
		if(state == SpawnState.WAITING){
			if(!EnemyIsAlive()){
				WaveCompleted();
				Debug.Log ("Wave completed");
			}else{
				return;
			}
		}
		if(waveCountdown <= 0){
			if (state != SpawnState.SPAWNING){
				StartCoroutine( SpawnWave( waves[nextWave] ) );
			}
		}else{
			waveCountdown -= Time.deltaTime;
		}
	}

	#region Enemy Spawner
	bool EnemyIsAlive(){
		searchCountdown -= Time.deltaTime;
		if(searchCountdown <= 0f){
			searchCountdown = 1f;
			if(GameObject.FindGameObjectWithTag ("Enemy") == null){
				return false;
			}
		}
		return true;
	}

	void WaveCompleted(){
		Debug.Log("Wave Completed");
		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;
		if(nextWave+1 > waves.Length -1){
			nextWave = 0;
			Debug.Log("All waves completed! Looping...");
			//Chamar Level Clear e depois chamar LevelManager (Level 2)
		}else{
			nextWave++;
		}
	}

	IEnumerator SpawnWave(Wave _wave){
		//Debug.Log("Spawing Wave "+ _wave.name);
		state = SpawnState.SPAWNING;
		for (int i = 0; i < _wave.count; i++){
			SpawnEnemy(_wave.enemy);
			yield return new WaitForSeconds(1f/_wave.rate); // ou 1f/_wave.delay
		}
		state = SpawnState.WAITING;
		yield break; // sempre usar quando tiver usando IEnumerator
	}

	public void SpawnEnemy(Transform _enemy){		
		//Debug.Log("Spawning Enemy: " + _enemy.name);
		Transform _esp = enemySpawnPoint[ Random.Range (0, enemySpawnPoint.Length) ]; //choose random point declared on the Unity Editor
		Instantiate(_enemy, _esp.position, _esp.rotation);

//		for (int i = 0; i < level[levelFase].enemy.Length; i++) {
//			Instantiate (enemiesPrefab[level[levelFase].enemy[i]], respawn[level[levelFase].enemyPosition[i]], transform.rotation);
//		}
	}

	static void RemoveAllEnemies(Wave _wave){
   		foreach(Transform enemies in _wave.enemy){
   			Transform.Destroy(enemies);
     	}
     }


	#endregion

}

