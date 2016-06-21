using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TouchController : MonoBehaviour {
	public Sprite mySprite;
	public int touchCount;
	public enum ItemType{BANANA, BREAD, APPLE, LETTUCE, TOUCH_COLOR};
	public ItemType itemType;
	public int points;
	public bool healthy;
	public Transform pointsPrefab;
	float posX;
	float posY;

	private Vector3 defaultScale = new Vector3(1,1,1);
	private Vector3 newScale = new Vector3(2,2,2); //Twice the size.
	private bool scaled;

	private ItemController itemController;

	void Awake () {
		gameObject.GetComponent<SpriteRenderer>().sprite = mySprite;

		//Debug.Log(touchCount);
	}

	void OnMouseDown () {
		touchCount--;
		if(!scaled){
                transform.localScale = newScale;
                scaled = true;
        }
		if(touchCount <= 0){
		//	AudioManager.Instance.PlaySound("ItemCollected");
			GameManager.Instance.UpdateScore2(points);
			switch(itemType){
				case ItemType.BANANA:
					SpawnPoints(points, gameObject.transform.position.x , gameObject.transform.position.y);

					//Animacao de Dano e adicionar + numero 
					break;
				case ItemType.BREAD:
					//SpawnPoints(points, transform.position.x, transform.position.y);
					break;
				case ItemType.APPLE:
					break;
				case ItemType.LETTUCE:
					SpawnPoints(points, transform.position.x, transform.position.y);
					break;	
			}
			//itemController.DestroyItem(gameObject);	 //NAO DELETAR
			DestroyObject(gameObject);
		}
//		Debug.Log("Entrou aqui "  + gameObject.name); //NAO DELETAR
		
	}

	void OnMouseUp(){
		transform.localScale = defaultScale;
	}

	void  SpawnPoints ( float points ,   float x ,   float y  ){
         x = Mathf.Clamp(x,0.6f,0.8f); // clamp position to screen to ensure
         y = Mathf.Clamp(y,0.6f,0.8f);  // the string will be visible
		Transform gui = Instantiate(pointsPrefab,new Vector3(x,y,0),Quaternion.identity) as Transform;

		if(healthy){
			gui.GetComponent<GUIText>().fontSize = 65;
			gui.GetComponent<GUIText>().text = "+ " + points.ToString();
		}else{
			gui.GetComponent<GUIText>().color = Color.red;
			gui.GetComponent<GUIText>().text = "- " + points.ToString();
		}
         
     }

 }		

