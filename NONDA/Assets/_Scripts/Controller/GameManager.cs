using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	#region Private variables
	private static GameManager instance; 
//	[SerializeField] 
	private GameObject itemPrefab; 
	[SerializeField] private Text itemText;
	[SerializeField] private Text scoreText;
	[SerializeField] private uint pointsToNextLevel;
	private int collectedItem;
	private float searchCountdown = 1f;
	private float point = 0;
	#endregion


	void Awake(){
		scoreText.text = point.ToString("000000");
	}


	public void UpdateScore(float points){
		point += points;
		scoreText.text = point.ToString("000000");
	}


	void LoadNextLevel(){
		if(point >= pointsToNextLevel){
			SceneManager.LoadScene(sceneBuildIndex:+1);
		}
	}













	#region Public variables
	public static GameManager Instance{
		get{
			if(instance == null){
				instance = FindObjectOfType<GameManager>();
			}
			return instance;
		}
	}

	public GameObject ItemPrefab {
		get {
			return itemPrefab;
		}
	}

	public int CollectedItem {
		get {
			return collectedItem;
		}
		set {
			itemText.text = value.ToString();
			this.collectedItem = value;
		}
	}

	//------ITEMS
	public class Item{
		public string name; //nome do Item (para Coletar)
		public Transform item; // Referencia para instanciar o objeto (item)
		public int count; //armazena a quantidade do contador
	}

	public Item[] items;

	#endregion

	bool ItemHasEnergy(){
		searchCountdown -= Time.deltaTime;
		if(searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if(GameObject.FindGameObjectWithTag ("Item") == null)
			{
				return false;
			}
		}
		return true;
	}

}
