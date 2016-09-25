using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour
{
	[System.Serializable]
	public class Wave
	{
		public string name;//nome da Wave (para Items)
		public Transform item;// Referencia para instanciar o objeto (Enemies, player, item)
		public int count;//armazena a quantidade do contador
	}

	public Wave[] waves;
	public Transform[] itemSpawnPoint;
	private int nextWave = 0;
	public float maxTime = 9;
	public float minTime = 2;
	public float waveCountdown;
	public float timeBetweenWaves = 3f;


	//The time to spawn the object
	private float spawnTime;

	void Start ()
	{
		if (itemSpawnPoint.Length == 0) {
			Debug.LogError ("No item Spawn referenced");
		}
		waveCountdown = timeBetweenWaves;
	}

	void Update ()
	{
		if (waveCountdown <= 0) {
			isTimeToSpawn ();
		} else {
			waveCountdown -= Time.deltaTime;
		}
	}

	void isTimeToSpawn ()
	{
		waveCountdown = timeBetweenWaves;
		if (GameObject.FindGameObjectWithTag ("ItemN") == null) {
			if (nextWave > waves.Length - 1) {
				nextWave = 0;
				Debug.Log ("All bad waves completed! Looping...");
				return;
			} else {
				SpawnWave (waves [nextWave]);
				nextWave++;
			}
		}
	}

	void SpawnWave (Wave _wave)
	{
		for (int i = 0; i < _wave.count; i++) {
			SpawnItem (_wave.item);
		}
	}


	public void SpawnItem (Transform _item)
	{
		Transform _spawnPoint = itemSpawnPoint [Random.Range (0, itemSpawnPoint.Length)]; //choose random point declared on the Unity Editor
		Instantiate (_item, _spawnPoint.position, _spawnPoint.rotation);
	}
}