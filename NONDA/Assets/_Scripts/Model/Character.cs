using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour {
	#region Protected Variables
	[SerializeField] protected float totalLife;
	protected bool facingRight;
	protected Rigidbody2D MyRigidBody{get; set;}
	#endregion

	#region Public
	public Animator MyAnimator {
		get;
		private set;
	}
	public abstract bool IsDead {get;}
	#endregion
	#region Private Variables
	[SerializeField] protected Text lifeText;
	[Range (-3f, 3f)][SerializeField] private float moveSpeed;
	[Range (0f, 18f)][SerializeField] private float jumpForce;
	//[SerializeField] private Transform acidBombPosition;
	//[SerializeField] private GameObject acidBombPrefab;
	#endregion

	public float MoveSpeed{
		get {return moveSpeed;}
		set {moveSpeed = value;}
	}

	public float JumpForce{
		get {return jumpForce;}
		set {jumpForce = value;}
	}

	protected bool FacingRight{
		get {return facingRight;}
		set {facingRight = value;}
	}

	public float Life{
		get{
			return totalLife;
		}
		set 
		{
			lifeText.text = value.ToString();
			this.totalLife = value;
		}
	}

	public bool Attack{get; set;}

	public bool TakingDamage {get; set;}

	public virtual void Start(){
		facingRight = true;
		MyAnimator = GetComponent<Animator>();
		MyRigidBody = GetComponent<Rigidbody2D>();
	}

	public abstract IEnumerator DealDamage(uint damage);

	//Make Character change to oposite direction
	public void ChangeDirection(){
		#if UNITY_ANDROID
			facingRight = !facingRight;
			MoveSpeed *= -1;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		#endif
		}

	public void ThrowAcidBomb(int value){
//		if(facingRight){
//			GameObject tmp = (GameObject)Instantiate(acidBombPrefab, acidBombPrefab.position, Quaternion.identity);
//			tmp.GetComponent<AcidBomb>().Inicialize(Vector2.right);
//		}else{
//			GameObject tmp = (GameObject)Instantiate(acidBombPrefab, acidBombPrefab.position, Quaternion.identity);
//			tmp.GetComponent<AcidBomb>().Inicialize(Vector2.left);
//		}
	}

	public void BirdAttack(){
		//Se precisar habilitar desabilitar
		//BeakCollider.enabled = !BeakCollider.enabled;
	}


}
