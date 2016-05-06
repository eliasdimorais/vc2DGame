using UnityEngine;
using System.Collections;

public class Enemy : Character {

	#region Private variable
	private IEnemyState currentState;
	[SerializeField]private float fightRange;
	public float movementSpeed = 1.3f;
	#endregion
	#region Public variable
	public GameObject Target {get; set;}
	public bool InAttackMode{
		get
		{
			if(Target != null){
				return Vector2.Distance(transform.position, Target.transform.position) <= fightRange; 
			}
		return false;
		}
	}
	#endregion

	public override void Start () {
		base.Start();
		ChangeState(new IdleState());
	}

	void Update () {
		if(!IsDead){
			currentState.Execute();
			//LookAtTheTarget();
		}

	}

	public void Move(){
		if(!Attack){
			MyAnimator.SetFloat("Speed", 1);
			transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime)); //move functions
		}
	}

	public void ChangeState(IEnemyState newState){
		if(currentState != null){
			currentState.Exit();
		}
		currentState = newState;

		currentState.Enter(this);
	}

	public Vector2 GetDirection(){
		return facingRight ? Vector2.right : Vector2.left;
	}

	private void FollowTarget(){
		if(Target != null){
			float xDir = Target.transform.position.x - transform.position.x;
			if(xDir < 0 && facingRight || xDir > 0 && !facingRight){
					ChangeDirection();

			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		//base.OnTriggerEnter2D(other);
		var tag = other.gameObject.tag; 
		if (tag == "Edge"){
			ChangeDirection();
		}
		if(other.tag == "Touch"){
			StartCoroutine(TakeDamage());
		}
	}

	#region implemented abstract members of Character

	public override IEnumerator TakeDamage ()
	{
		for (int i = 0; i < Input.touchCount; ++i) {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
				health -= 10;
			if(!IsDead){
				MyAnimator.SetTrigger("Damage");
			}
			else{
				MyAnimator.SetTrigger("Dead");
				Debug.Log("Dead");
				yield return null;
			}
        }	
	}

	public override bool IsDead {
		get {
			return health <= 0;
		}
	}
	#endregion
}
