using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour {
	
	GameObject superAttackGraphic;
	FlightGameManager gameManager;

	// Use this for initialization
	void Start () {
		superAttackGraphic = transform.FindChild("FireLine").gameObject;
		superAttackGraphic.SetActive(false);
		gameManager = GameObject.Find("GameManager").GetComponent<FlightGameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void ShowSuper() {
		superAttackGraphic.SetActive(true);
	}
	
	public void HideSuper() {
		superAttackGraphic.SetActive(false);
	}
	
	public void Damage(int d) {
		gameManager.PlayerDamage(d);
	}
	
}
