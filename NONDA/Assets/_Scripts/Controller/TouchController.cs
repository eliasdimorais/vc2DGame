using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TouchController : MonoBehaviour {
	public Image energyBar;
	public Sprite mySprite;
	public int touchCount;
	public enum ItemType{BANANA, BREAD, APPLE, LETTUCE, EGG, COFFEE, TEA_BAG, CHICKEN_BONE, CHEESE, TOUCH_COLOR};
	public ItemType itemType;
	public int score;
	public bool healthy;
	public Transform pointsPrefab;
	float posX;
	float posY;

	private Vector3 defaultScale = new Vector3(1,1,1);
	private Vector3 newScale = new Vector3(2,2,2); //Twice the size.
	private bool scaled;
	private int lastDamage = 0;

	private ItemController itemController;

	void Awake () {
		gameObject.GetComponent<SpriteRenderer>().sprite = mySprite;
		lastDamage = touchCount;
		//Debug.Log(touchCount);
	}

	void OnMouseDown () {
		touchCount--;
		UpdateEnergyBar();
		if(!scaled){
                transform.localScale = newScale;
                scaled = true;
        }
		if(touchCount <= 0){
			//AudioManager.Instance.PlaySound("ItemCollected");
			GameManager.Instance.UpdateScore2(score);
			switch(itemType){
				#region POWER_UPS
				case ItemType.TEA_BAG:
					SpawnPoints(score, gameObject.transform.position.x , gameObject.transform.position.y);
					break;
				#endregion

				#region GREEN Itens
				case ItemType.BANANA:
					SpawnPoints(score, gameObject.transform.position.x , gameObject.transform.position.y);
					//Animacao de Dano e adicionar + numero 
					break;
				case ItemType.APPLE:
					SpawnPoints(score, gameObject.transform.position.x , gameObject.transform.position.y);
					break;
				case ItemType.LETTUCE:
					SpawnPoints(score, transform.position.x, transform.position.y);
					break;
				#endregion

				#region YELLOW Itens
				case ItemType.BREAD:
					SpawnPoints(score, gameObject.transform.position.x , gameObject.transform.position.y);
					break;
				case ItemType.COFFEE:
					SpawnPoints(score, gameObject.transform.position.x , gameObject.transform.position.y);
					break;
				#endregion

				#region RED Itens
				case ItemType.CHICKEN_BONE:
					SpawnPoints(score, gameObject.transform.position.x , gameObject.transform.position.y);
					break;

				case ItemType.CHEESE:
					SpawnPoints(score, gameObject.transform.position.x , gameObject.transform.position.y);
					break;

				#endregion
			}
			//itemController.DestroyItem(gameObject);	 //NAO DELETAR
			DestroyObject(gameObject);
		}
//		Debug.Log("Entrou aqui "  + gameObject.name); //NAO DELETAR
		
	}

	void OnMouseUp(){
		transform.localScale = defaultScale;
	}

	void  SpawnPoints ( float score ,   float x ,   float y  ){
         x = Mathf.Clamp(x,0.5f,0.5f); // clamp position to screen to ensure
         y = Mathf.Clamp(y,0.5f,0.5f);  // the string will be visible
		Transform gui = Instantiate(pointsPrefab,new Vector3(x,y,0),Quaternion.identity) as Transform;

		if(healthy){
			gui.GetComponent<GUIText>().fontSize = 65;
			gui.GetComponent<GUIText>().color = new Color(0.647f, 0.863f, 0.251f);
			gui.GetComponent<GUIText>().text = "+ " + score.ToString();
		}else{
			gui.GetComponent<GUIText>().fontSize = 65;
			gui.GetComponent<GUIText>().color = new Color(0.867f, 0.333f,0.251f); 
		
			gui.GetComponent<GUIText>().text =  score.ToString();
		}
		//	gui.GetComponent<GUIText>().color = new Color(0.867f, 0.984f,0.416f); YELLOW
		//	gui.GetComponent<GUIText>().color = new Color(0.647f, 0.863f, 0.251f); GREEN
		//	gui.GetComponent<GUIText>().color = new Color(0.867f, 0.333f, 0.251f); RED
         
     }

	public void UpdateEnergyBar(){
		float cal_Touch = (float)touchCount/(float)lastDamage;
		SetEnergyBar(cal_Touch);
	}

	void SetEnergyBar (float myScore) {
		energyBar.fillAmount = myScore;
	}

 }		

