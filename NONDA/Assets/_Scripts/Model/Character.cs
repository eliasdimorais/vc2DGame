using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour {
	#region Protected Variables
	protected bool runNow;
	protected bool facingRight;
	protected Rigidbody2D MyRigidBody{get; set;}
	#endregion

	#region Public
	[SerializeField] private Collider2D typeCollider;
	public Animator MyAnimator {
		get;
		private set;
	}
	public abstract bool IsDead {get;}
	#endregion
	#region Private Variables
	[Range (-5f, 5f)][SerializeField] private float moveSpeed;
	protected float moveSpeedTemp;
	//[SerializeField] private Transform acidBombPosition;
	//[SerializeField] private GameObject acidBombPrefab;
	#endregion

	public float MoveSpeed{
		get {return moveSpeed;}
		set {moveSpeed = value;}
	}

	protected bool FacingRight{
		get {return facingRight;}
		set {facingRight = value;}
	}

	public bool Attack{get; set;}

	public bool TakingDamage {get; set;}

	public Collider2D CharacterCollider{
		get{
			return typeCollider;
		}
	}
	public virtual void Start(){
		facingRight = true;
		MyAnimator = GetComponent<Animator>();
		MyRigidBody = GetComponent<Rigidbody2D>();
		TakingDamage = false;
	}

	//public abstract IEnumerator DealDamage(int damage);

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

	/*public IEnumerator RunTemp(float theScale){
		print(MoveSpeed);
		if(theScale == 1){
			MoveSpeed *= 1.5f;
		}else if(theScale == -1){
			MoveSpeed *= -1.5f;
		}
		yield return new WaitForSeconds(1);
		print(MoveSpeed);
		runNow = false;
	}*/

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
