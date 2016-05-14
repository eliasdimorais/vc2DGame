using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour {

	[SerializeField] protected float totalValue;
	[SerializeField] protected float itemDamageValue;
	public Animator MyAnimator {
		get;
		private set;
	}

   	public abstract IEnumerator TakeDamage();

	void Start () {
	
	}

	void Update () {
	
	}

	void Inicialize(Transform position){

	}

	public bool IsDead {
		get {
			return totalValue <= 0;
		}
	}
}
