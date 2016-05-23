using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyDamageController : MonoBehaviour {
	public GameObject myEnemyObject;
	public int touchCount;
	public enum EnemyType{BIRD, LEECH, ANT}
	public EnemyType enemyType;
	public uint points;

	void Awake () {
		myEnemyObject = gameObject.GetComponent<GameObject>().gameObject; 
		//Debug.Log(touchCount);
	}

	void OnMouseDown () {
		touchCount--;
		Debug.Log(touchCount);
		if(touchCount <= 0){
			GameManager.Instance.UpdateScore(points);
			DestroyObject(gameObject);
			switch (enemyType){
				case EnemyType.ANT:
					break;
				case EnemyType.BIRD:
					DestroyObject(myEnemyObject);
					break;
				case EnemyType.LEECH:
					break;
				
			}
		}
	}
 }		

