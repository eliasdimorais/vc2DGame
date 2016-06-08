﻿using UnityEngine;
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
	[SerializeField] private string spawnSound;

	public Wave[] waves;
	public float timeBetweenWaves = 2.5f;
	public float waveCountdown;
	public Transform[] itemSpawnPoint;
	#endregion

	#region Private Variables
	private float searchCountdown = 1f; //Time between search is in seconds 
	private int nextWave = 0;
	private bool isFirstWave; 
	private SpawnState state = SpawnState.COUNTING;
	#endregion

	#region Animation Click Me
	//[SerializeField] private Sprite ClickMeCircle1;
	//[SerializeField] private Sprite ClickMeCircle2;
	//private float timer = 0.5f;
//	private float delay = 0.5f;
	#endregion

	void Start(){
		if(waves.Length == 0){
			Debug.LogError("No WAVES HAS BEEN FOUND. ");
		}

		if(itemSpawnPoint.Length == 0){
			Debug.LogError("No item Spawn referenced");
		}
		waveCountdown = timeBetweenWaves;
		//this.gameObject.GetComponent<SpriteRenderer>().sprite = ClickMeCircle1;
	}

	void Update(){
		if(state == SpawnState.WAITING){
			if(!ItemHasEnergy()){
				WaveCompleted();
				Debug.Log ("Wave completed");
				//Update Points on Game Manager
			}else{
				return;
			}
		}
		if(waveCountdown <= 0)
		{
			if (state != SpawnState.SPAWNING)
			{
				HandController.Instance.PlayHandAnimation();
				StartCoroutine( SpawnWave( waves[nextWave] ) );
			}
		}
		else
		{
			waveCountdown -= Time.deltaTime;
		}
		//timer -= Time.deltaTime;
//		if(timer <= 0){
//			if(this.gameObject.GetComponent<SpriteRenderer>().sprite = ClickMeCircle1){
//				this.gameObject.GetComponent<SpriteRenderer>().sprite = ClickMeCircle2;
//				timer = delay;
//			}
//		}
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
			//int itemIndex = Random.Range(0, _wave.count); randomize between Items
			//Debug.Log("O indice eh " + itemIndex);
			SpawnItem(_wave.item);
			yield return new WaitForSeconds(1f/_wave.rate); // ou 1f/_wave.delay
		}
		state = SpawnState.WAITING;
		yield break; // sempre usar quando tiver usando IEnumerator
	}

	public void SpawnItem(Transform _item){
		Transform _spawnPoint = itemSpawnPoint[ Random.Range (0,itemSpawnPoint.Length) ]; //choose random point declared on the Unity Editor
		Instantiate(_item, _spawnPoint.position, _spawnPoint.rotation);
		//AudioManager.Instance.PlaySound(spawnSound);
		//Debug.Log(_item + "Position: " + _spawnPoint.position);
	}

	public void SpawnAnimation (Transform _animation){
		Transform _spawnPoint = itemSpawnPoint[Random.Range(0, itemSpawnPoint.Length)];
		Instantiate(_animation, _spawnPoint.position, _spawnPoint.rotation);
	}

	void WaveCompleted(){
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
