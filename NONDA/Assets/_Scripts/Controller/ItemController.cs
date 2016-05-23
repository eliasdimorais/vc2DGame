using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {

	#region Public Variables
	public enum SpawnState{SPAWNING, WAITING, COUNTING};
	[System.Serializable]
	public class Wave{
		public string name; //nome da Wave (para Items)
		public Transform item; // Referencia para instanciar o objeto (Enemies, player, item)
		public int count; //armazena a quantidade do contador
		public float rate; // 
	}
	public Wave[] waves;

	public float timeBetweenWaves = 5f;
	public float waveCountdown;
	public Transform[] itemSpawnPoint;

	#endregion

	#region Private Variables
	//private float searchCountdown = 1f; //Time between search is in seconds 
	private GameObject parent;
	private Item itemFolder;
	private int nextWave = 0;
	private SpawnState state = SpawnState.COUNTING;
	private float searchCountdown = 1f; 
	#endregion


	void Start(){
//		parent = GameObject.Find ("Items");
//		Debug.Log(parent);
//		itemFolder = GameObject.FindObjectsOfType<Item>();
//	
//		if (!parent) {
//			parent = new GameObject("Items");
//		}
		if(waves.Length == 0){
			Debug.LogError("No WAVES HAS BEEN FOUND. ");
		}

		if(itemSpawnPoint.Length == 0){
			Debug.LogError("No item Spawn referenced");
		}

		waveCountdown = timeBetweenWaves;
	}

	void Update(){
		if(state == SpawnState.WAITING){
			if(!ItemHasEnergy()){
				WaveCompleted();
				Debug.Log ("Wave completed");

				//Update Points 
			}else{
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

	#region Item Spawner
	bool ItemHasEnergy(){
		searchCountdown -= Time.deltaTime;
		if(searchCountdown <= 0f){
			searchCountdown = 1f;
			if(GameObject.FindGameObjectWithTag ("Item") == null){
				return false;
			}
		}
		return true;
	}

	IEnumerator SpawnWave(Wave _wave){
		state = SpawnState.SPAWNING;
		for (int i = 0; i < _wave.count; i++)
		{
			SpawnItem(_wave.item);
			yield return new WaitForSeconds(1f/_wave.rate); // ou 1f/_wave.delay

		}
		state = SpawnState.WAITING;
		yield break; // sempre usar quando tiver usando IEnumerator
	}

	public void SpawnItem(Transform _item){		
		Transform _esp = itemSpawnPoint[ Random.Range (0, itemSpawnPoint.Length) ]; //choose random point declared on the Unity Editor
		Instantiate(_item, _esp.position, _esp.rotation);
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

	#endregion

}
