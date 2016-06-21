using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalInfo : MonoBehaviour {
	public Text infoItem;
	public Text titleItem;
	public Image iconImage;
	public Button button1;
	public Button button2;
	public GameObject modalItemInfo;

	public static ModalInfo modalInfo;
	public static ModalInfo Instance(){
		if(!modalInfo){
			modalInfo = FindObjectOfType(typeof (ModalInfo)) as ModalInfo;
			if(!modalInfo)
				Debug.LogError("There is no ModalPanel script on a GameObject in your scene");
		}
		return modalInfo;
	}

	//annoucement with icon
//	public void Choice(string infoItem, Sprite iconImage, UnityAction cancelEvent){
//		modalItemInfo.SetActive(true);
//	
//		cancelButton.onClick.RemoveAllListeners();
//		cancelButton.onClick.AddListener(cancelEvent);
//		cancelButton.onClick.AddListener(ClosePanel);
//
//		this.infoItem.text = infoItem;
//
//		this.iconImage.sprite = iconImage;
//		this.iconImage.gameObject.SetActive(true);
//		yesButton.gameObject.SetActive(false);
//		cancelButton.gameObject.SetActive(true);
//
//	}

	public void Choice(string infoItem, bool isItemHealthy, UnityAction cancelEvent,  Sprite iconImage = null){
		modalItemInfo.SetActive(true);
	
		button2.onClick.RemoveAllListeners();
		button2.onClick.AddListener(cancelEvent);
		button2.onClick.AddListener(ClosePanel);

		this.infoItem.text = infoItem;

		if(isItemHealthy){
			titleItem.text = "ALIMENTO SAUDÁVEL";
			//titleItem.color = Color.yellow;
			titleItem.color = new Color(255, 215,87, 1);
		}else{
			titleItem.text = "NÃO É ALIMENTO SAUDÁVEL";
			titleItem.color = Color.red;
		}

		if(iconImage)
			this.iconImage.sprite = iconImage;

		if(iconImage)
			this.iconImage.gameObject.SetActive(true);
		else
			this.iconImage.gameObject.SetActive(false);

		this.iconImage.sprite = iconImage;
		this.iconImage.gameObject.SetActive(true);
		button1.gameObject.SetActive(false);
		button2.gameObject.SetActive(true);

	}

	//info with yes and cancel event
	public void Choice(string infoItem, UnityAction yesEvent, UnityAction cancelEvent){
		modalItemInfo.SetActive(true);
		button1.onClick.RemoveAllListeners();
		button1.onClick.AddListener(yesEvent);
		button1.onClick.AddListener(ClosePanel);

		button2.onClick.RemoveAllListeners();
		button2.onClick.AddListener(cancelEvent);
		button2.onClick.AddListener(ClosePanel);

		this.infoItem.text = infoItem;

		this.iconImage.gameObject.SetActive(false);
		button1.gameObject.SetActive(true);
		button2.gameObject.SetActive(true);

	}

	//info with icon and yes/cancel event
	public void Choice(string infoItem, Sprite iconImage, UnityAction yesEvent, UnityAction cancelEvent){
		modalItemInfo.SetActive(true);
		button1.onClick.RemoveAllListeners();
		button1.onClick.AddListener(yesEvent);
		button1.onClick.AddListener(ClosePanel);

		button2.onClick.RemoveAllListeners();
		button2.onClick.AddListener(cancelEvent);
		button2.onClick.AddListener(ClosePanel);

		this.infoItem.text = infoItem;

		this.iconImage.sprite = iconImage;
		this.iconImage.gameObject.SetActive(true);
		button1.gameObject.SetActive(true);
		button2.gameObject.SetActive(true);

	}

	void ClosePanel(){
		modalItemInfo.SetActive(false);
	}
}
