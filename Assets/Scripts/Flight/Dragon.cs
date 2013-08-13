using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour {
	
	GameObject superAttackGraphic;
	FlightGameManager gameManager;
	float invulTimeLeft = 0;
	Animator2D animator;
	public float invulnurabilityTime = 1.5f;	

	// Use this for initialization
	void Start () {
		superAttackGraphic = transform.FindChild("FireLine").gameObject;
		superAttackGraphic.SetActive(false);
		gameManager = GameObject.Find("GameManager").GetComponent<FlightGameManager>();
		animator = GetComponent<Animator2D>().Child("Plane");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate() {
		if (invulTimeLeft > 0) { 
			invulTimeLeft -= Time.fixedDeltaTime;
			int phase = ((int)((invulTimeLeft*20)%10))%2;
			if (phase == 0) {
				animator.gameObject.SetActive(false);
			} else {
				animator.gameObject.SetActive(true);
			}
		} else {
			animator.gameObject.SetActive(true);
		}		
	}
	
	public void ShowSuper() {
		superAttackGraphic.SetActive(true);
	}
	
	public void HideSuper() {
		superAttackGraphic.SetActive(false);
	}
	
	public void Damage(int d) {
		Debug.Log("dragon damage");
		if (invulTimeLeft > 0) return;
		invulTimeLeft = invulnurabilityTime;
		gameManager.PlayerDamage(d);
	}
	
}
