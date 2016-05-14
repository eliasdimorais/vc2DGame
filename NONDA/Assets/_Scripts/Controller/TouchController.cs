using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TouchController : MonoBehaviour {
	private bool touchedOn = false;
	//public GameObject itemPrefab; 
	//public GameObject[] enemyPrefab;

    Vector3 touchPosWorld;
    TouchPhase touchPhase = TouchPhase.Ended;
	public Touch tuch;
	public Animator MyAnimator {
		get;
		private set;
	}
    	
	void Start () {
//		enemyPrefab = GameObject.FindWithTag("Enemy");
		//itemPrefab = GameObject.FindGameObjectsWithTag("Item");
	}

    void Update() {

    	if(touchedOn){
			Debug.Log(gameObject.name);	
    		//verificar o tipo do toque - se foi um item ou se foi um inimigo
    		//UpdatePoints();
    	 }
//		if (Input.touchCount > 0 && Input.GetTouch(0).phase == touchPhase) {
//			Vector3 touchPosition = Input.GetTouch(0).position;
//			//Vector3 g = Camera.allCameras[0].ScreenToWorldPoint(touchPosition);
//
//			Vector2 touchPosWorld2D = new Vector2(g.x, g.y);
//			//RaycastHit2D hitInformation = Physics2D.Raycast(transform.position, touchPosWorld2D);
//			//RaycastHit hitInfo = Camera.allCameras[0].ScreenPointToRay(touchPosition);
//			Ray rayhit = Camera.allCameras[0].ScreenPointToRay(touchPosition);
//		
//			Debug.Log(rayhit);
//			if (Physics.Raycast(rayhit, hit){
//				Debug.Log(GameObject.Find(hit));
//			}
//		}
    }

	void OnMouseDown () {
		touchedOn = true;
		if (Input.GetTouch(0).phase == TouchPhase.Began && Input.touchCount != 0)
		   	{	
				Debug.Log(gameObject.name);
				ItemController.Destroy(gameObject);

		   }
	}

 }		

