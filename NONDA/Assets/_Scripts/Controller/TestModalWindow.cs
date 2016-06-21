using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class TestModalWindow : MonoBehaviour {
	public Sprite icon;
	public string infoItemText; //Information about the item (for instance, if its good for vermicomposting or not)
	public bool isItemHealthy;
	public Transform spawnPoint;
	public GameObject thingToSpawn;
	private ModalInfo modalInfo;
	private DisplayManager displayManager;
	//private UnityAction myYesAction;
	//private UnityAction myCancelAction;
	//reference
	void Awake(){
		modalInfo = ModalInfo.Instance();
		displayManager = DisplayManager.Instance();
	
		//myYesAction = new UnityAction(TestYesFunction);
		//myCancelAction = new UnityAction (TestCancelFunction);
	}
	//Send Icon and information about item clicked
	public void TestIconCancel(){
		Debug.Log("Clicked");
		GameManager.Instance.Pause(true);
		modalInfo.Choice(infoItemText, isItemHealthy, TestCancelFunction, icon);
	}


	//Send to the modal panel to set up the button and functions to call
	public void TestYesCancel(){
		Debug.Log("Clicked");
		//modalInfo.Choice("Você entendeusobre o item?\n O que acha desse item?", myYesAction, myCancelAction);
		modalInfo.Choice("Você entendeu sobre o item?\n O que acha desse item?", TestYesFunction, TestCancelFunction);
	}

//	public void TestIconYesCancel(){
//		Debug.Log("Clicked");
//		modalInfo.Choice("Mais informação sobre icone\n O que você acha?", icon, TestYesFunction, TestCancelFunction);
//	}

	public void TestLambda(){
		modalInfo.Choice ("Você sabia que a banana é uma fruta que pode...", () => {InstantiateObject (thingToSpawn);}, TestCancelFunction );
	}
	//wrapped into UnityActions
	void TestYesFunction(){
		displayManager.DisplayMessage("You gotta win");
	}

	void TestCancelFunction(){
		displayManager.DisplayMessage("You cancel OMG. I give up");
		GameManager.Instance.Pause(false);
	}

	void InstantiateObject(GameObject thingToInstantiate){
		displayManager.DisplayMessage("Spawned and ready");
		Instantiate (thingToInstantiate, spawnPoint.position, spawnPoint.rotation);
	}
}
