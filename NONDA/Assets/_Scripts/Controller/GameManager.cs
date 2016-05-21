using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	#region Private variables
	private static GameManager instance; 
	[SerializeField] private GameObject itemPrefab; 
	[SerializeField] private Text itemText;
	[SerializeField] private Text scoreText;
	private int collectedItem;
	private float searchCountdown = 1f;
	private float point = 0;
	#endregion


	void Awake(){
		scoreText.text = point.ToString("0000000");
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

	public void UpdateScore(float points){
		point += points;
		scoreText.text = point.ToString("0000000");
	}


}
