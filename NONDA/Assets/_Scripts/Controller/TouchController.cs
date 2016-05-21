using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TouchController : MonoBehaviour {
	public Sprite mySprite;
	public int touchCount;
	public enum ItemType{BANANA, BREAD, APPLE, LETTUCE};
	public ItemType itemType;
	public uint points;

	void Awake () {
		gameObject.GetComponent<SpriteRenderer>().sprite = mySprite;
		Debug.Log(touchCount);
	}

	void OnMouseDown () {
		touchCount--;
		Debug.Log(touchCount);
		if(touchCount <= 0){
			GameManager.Instance.UpdateScore(points);
			DestroyObject(gameObject);
			switch(itemType){
				case ItemType.BANANA:
					
					break;
				case ItemType.BREAD:
					break;
				case ItemType.APPLE:
					break;
				case ItemType.LETTUCE:
					break;	
			}
		}
//		Debug.Log("Entrou aqui "  + gameObject.name); //NAO DELETAR
//		ItemController.DestroyObject(gameObject);	 //NAO DELETAR
	}
 }		

