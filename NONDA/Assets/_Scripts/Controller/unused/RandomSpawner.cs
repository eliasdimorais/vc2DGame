using UnityEngine;
using System.Collections;

public class RandomSpawner : MonoBehaviour
{
	bool isSpawning = false;
    public float minTime = 5.0f;
    public float maxTime = 15.0f;
    public Transform[] enemies;  // Array of enemy prefabs.
	public Transform[] itemSpawnPoint;

    IEnumerator SpawnObject(int index, float seconds)
    {
        Debug.Log ("Waiting for " + seconds + " seconds");

        yield return new WaitForSeconds(seconds);
        Instantiate(enemies[index], transform.position, transform.rotation);     
        isSpawning = false;
    }

	void Start(){
		if(itemSpawnPoint.Length == 0){
			Debug.LogError("No item Spawn referenced");
		}
	}

    void Update () 
    {
        //We only want to spawn one at a time, so make sure we're not already making that call
        if(! isSpawning)
        {
            isSpawning = true; //Yep, we're going to spawn
            int enemyIndex = Random.Range(0, enemies.Length);
            StartCoroutine(SpawnObject(enemyIndex, Random.Range(minTime, maxTime)));
        }
    }
}
