using UnityEngine;
using System.Collections;

public class FlightGameManager : GameManager {
	
	public GameObject dragonPrefab;
	public GameObject playerBulletPrefab;
	public GameObject fairyPrefab;
	public GUIText statusLine;
	public Bounds worldBounds;
	public GameObject backgroundPlane;
	public Camera camera;
	public float fairyDelayMin = 1;
	public float fairyDelayMax = 5;
	public int life = 5;
	
	float fairyDelay;
	
	
	Dragon dragon;
	FlightGUI flightGUI;
	
	public int superAttackCharges = 0;
	
	float superAttackDuration = 3;
	float superAttackTimeLeft = 0;
	
	float fireballCooldown = 0.06f;
	float fireballCooldownRemaining = 0;
	
	
	GameObject superAttackGraphic;
	
	public bool IsOutside(Vector3 pos) {
		return !worldBounds.Contains(pos);
	}
	
	public Dragon GetDragon() {
		return dragon;
	}
	
	// Use this for initialization
	void Start () {
		base.Start();
		SetGameState(GameState.Running);
		GameObject dragonObject = (GameObject) Instantiate(dragonPrefab);
		flightGUI = GetComponent<FlightGUI>();
		dragon = dragonObject.GetComponent<Dragon>();
		worldBounds.extents = new Vector3(worldBounds.extents.x * camera.aspect, 1, worldBounds.extents.z);
		backgroundPlane.transform.localScale = new Vector3(camera.aspect, 1,1);
		
		fairyDelay = Random.Range(fairyDelayMin, fairyDelayMax);
	}
	
	void UpdateDragonPosition() {
		
		Vector2 padData = flightGUI.GetPadDirection();
		
		Vector3 dragonShift = new Vector3 (padData.x, 0, -padData.y) * 0.1f;
			
		dragon.transform.position = new Vector3(
			Mathf.Clamp(dragon.transform.position.x + dragonShift.x, worldBounds.min.x, worldBounds.max.x),
			dragon.transform.position.y + dragonShift.y,
			Mathf.Clamp(dragon.transform.position.z + dragonShift.z, worldBounds.min.z, worldBounds.max.z)
			);
	}
	
	void ShootA() {
		Instantiate(playerBulletPrefab, dragon.transform.position + Vector3.up*0.5f, Quaternion.identity);
	}
	
	void EnableB() {
		dragon.ShowSuper();
	}
	
	void DisableB() {
		dragon.HideSuper();
	}
	
	void GenerateEnemies() {
		return;
		fairyDelay -= Time.fixedDeltaTime;
		if (fairyDelay < 0) {
			fairyDelay = Random.Range(fairyDelayMin, fairyDelayMax);
			Instantiate(fairyPrefab, new Vector3(worldBounds.max.x, 1, Random.Range(worldBounds.min.z*0.99f, worldBounds.max.z*0.99f)), Quaternion.identity);
		}
	}
	
	public void ChargeUp() {
		superAttackCharges ++;
	}
	
	public void RestoreLife() {
		life ++;
	}
	
	void UpdateBullets() {
		if (fireballCooldownRemaining > 0) {
			fireballCooldownRemaining -= Time.fixedDeltaTime;
		}else if (flightGUI.GetButtonA() && (superAttackTimeLeft<=0) ) {
			ShootA();
			fireballCooldownRemaining = fireballCooldown; 
		}
		
		if (flightGUI.GetButtonB() && (superAttackTimeLeft<=0) && (superAttackCharges > 0)) {
			superAttackCharges --;
			superAttackTimeLeft = superAttackDuration;
			EnableB();
		}
		if (superAttackTimeLeft > 0) {
			superAttackTimeLeft -= Time.fixedDeltaTime;
			if (superAttackTimeLeft <= 0) {
				DisableB();
			}
		}
	}
	
	void FixedUpdate() {
		if (!IsGameRunning()) return;
		UpdateDragonPosition();
		UpdateBullets();
		GenerateEnemies();
	}
	
	// Update is called once per frame
	void Update () {
		statusLine.text = superAttackCharges.ToString()+" "+life.ToString();			
	}
	
	public void PlayerDamage(int d) {
		Debug.Log("I'm hit!");
		life --;
		if (life<=0) Death();
	}
	
	void Death() {
		Debug.Log("YOU LOST");
		SetMedal(GameManager.Medal.None);
		EndGame();
	}
}
