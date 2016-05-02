using UnityEngine;
using System.Collections;

public class SpawnerController : MonoBehaviour {
	#region Public Variables
	public enum SpawnState{SPAWNING, WAITING, COUNTING};
	[System.Serializable]
	public class Wave{
		public string name; //nome da Wave (para Enemies)
		public Transform enemy; // Referencia para instanciar o objeto (Enemies, player, item)
		public int count; //armazena a quantidade do contador
		public float rate; // 
	}
	public Wave[] waves;

	public float timeBetweenWaves = 5f;
	public float waveCountdown;
	public Transform[] enemySpawnPoint;
	//public LevelSpawn[] level;

//	[System.Serializable]
//	public class LevelSpawn{
//		public int[] enemyPosition;
//		public int[] enemy;
//	}
	//public Transform[] respawn;
	//public GameObject[] enemiesPrefab;
	#endregion

	#region Private Variables
	private float searchCountdown = 1f; //Time between search is in seconds 
	private int nextWave = 0;
	private SpawnState state = SpawnState.COUNTING;
	#endregion


	void Start(){
		if(waves.Length == 0){
			Debug.LogError("No WAVES HAS BEEN FOUND. ");
		}

		if(enemySpawnPoint.Length == 0){
			Debug.LogError("No enemy Spawn referenced");
		}

		waveCountdown = timeBetweenWaves;
	}

	void Update(){
		if(state == SpawnState.WAITING)
		{
			if(!EnemyIsAlive())
			{
				WaveCompleted();
				Debug.Log ("Wave completed");

				//Update Points 
			}else
			{
				return;
			}
		}
		if(waveCountdown <= 0)
		{
			if (state != SpawnState.SPAWNING)
			{
				StartCoroutine( SpawnWave( waves[nextWave] ) );
			}
		}
		else
		{
			waveCountdown -= Time.deltaTime;
		}
	}

	#region Enemy Spawner
	bool EnemyIsAlive(){
		searchCountdown -= Time.deltaTime;
		if(searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if(GameObject.FindGameObjectWithTag ("Enemy") == null)
			{
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
		Debug.Log("Spawing Wave "+ _wave.name);
		state = SpawnState.SPAWNING;
		for (int i = 0; i < _wave.count; i++)
		{
			SpawnEnemy(_wave.enemy);
			yield return new WaitForSeconds(1f/_wave.rate); // ou 1f/_wave.delay
	
		}
		state = SpawnState.WAITING;
		yield break; // sempre usar quando tiver usando IEnumerator
	}


	//public void SpawnEnemy(int levelFase){	
	public void SpawnEnemy(Transform _enemy){		
		Debug.Log("Spawning Enemy: " + _enemy.name);
		Transform _esp = enemySpawnPoint[ Random.Range (0, enemySpawnPoint.Length) ]; //choose random point declared on the Unity Editor
		Instantiate(_enemy, _esp.position, _esp.rotation);
//		for (int i = 0; i < level[levelFase].enemy.Length; i++) {
//			Instantiate (enemiesPrefab[level[levelFase].enemy[i]], respawn[level[levelFase].enemyPosition[i]], transform.rotation);
//		}
	}

	#endregion

}

