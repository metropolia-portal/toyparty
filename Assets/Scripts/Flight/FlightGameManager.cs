using UnityEngine;
using System.Collections;

public class FlightGameManager : GameManager {
	
	public GameObject dragonPrefab;
	public GameObject playerBulletPrefab;
	public GameObject fairyPrefab;
	public GUIText statusLine;
	public Bounds worldBounds;
	public GameObject backgroundPlane;
	public Camera cam;
	public float fairyDelayMin = 1;
	public float fairyDelayMax = 5;
	public int life = 5;
	int score = 0;
	public FlightSoundManager soundManager;
	
	float fairyDelay;
	
	
	Dragon dragon;
	FlightGUI flightGUI;
	
	public int superAttackCharges = 0;
	
	float superAttackDuration = 1.5f;
	float superAttackTimeLeft = 0;
	
	float fireballCooldown = 0.3f;
	float fireballCooldownRemaining = 0;
	
	
	GameObject superAttackGraphic;
	
	public void ShowBossLife() {
		flightGUI.ShowBossLife(true);
	}
	
	public void HideBossLife() {
		flightGUI.ShowBossLife(false);
	}
	
	public void SetBossLife(float l) {
		flightGUI.SetBossLife(l);
	}
	
	public bool IsOutside(Vector3 pos) {
		return !worldBounds.Contains(pos);
	}
	
	public Dragon GetDragon() {
		return dragon;
	}
	
	// Use this for initialization
	public override void Start () {
		soundManager = GetComponent<FlightSoundManager>();
		base.Start();
		SetGameState(GameState.Running);
		GameObject dragonObject = (GameObject) Instantiate(dragonPrefab, new Vector3(-2*cam.aspect,0,0), Quaternion.identity);
		GetComponent<ScoreGUI>().SetMedalRequirements(bronzeMedalScore, silverMedalScore, goldMedalScore);
		flightGUI = GetComponent<FlightGUI>();
		dragon = dragonObject.GetComponent<Dragon>();
		worldBounds.extents = new Vector3(worldBounds.extents.x * cam.aspect, 1, worldBounds.extents.z);
		backgroundPlane.transform.localScale = new Vector3(cam.aspect, 1,1);
		
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
		GetDragon().weaponAudioSource.clip = soundManager.AttackA;
		GetDragon().weaponAudioSource.Play();
	}
	
	void EnableB() {
		dragon.ShowSuper();
	}
	
	void DisableB() {
		dragon.HideSuper();
	}
	
	void ShootC() {
		for (int i=-60;i<60;i+=10)
			Instantiate(playerBulletPrefab, dragon.transform.position + Vector3.up*0.5f, Quaternion.Euler(0,i,0));
		GetDragon().weaponAudioSource.clip = soundManager.AttackB;
		GetDragon().weaponAudioSource.Play();
	}
	
	void GenerateEnemies() {
		fairyDelay -= Time.fixedDeltaTime;
		if (fairyDelay < 0) {
			fairyDelay = Random.Range(fairyDelayMin, fairyDelayMax);
			Instantiate(fairyPrefab, new Vector3(worldBounds.max.x, 1, Random.Range(worldBounds.min.z*0.99f, worldBounds.max.z*0.99f)), Quaternion.identity);
		}
	}
	
	public void ChargeUp() {
		superAttackCharges ++;
	}
	
	public void OnFairyDeath(int s) {
		score +=s;
		GetComponent<ScoreGUI>().SetScore(score);
		
	}
	
	public void RestoreLife() {
		if (life < 3) {
			life ++;
			GetComponent<ScoreGUI>().SetMaxMedals(life);
		}	else {
			score += 20;
			GetComponent<ScoreGUI>().SetScore(score);
		}
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
			ShootC();
			dragon.INDOMITABLE();
		}
		if (superAttackTimeLeft > 0) {
			superAttackTimeLeft -= Time.fixedDeltaTime;
			if (superAttackTimeLeft <= 0) {
				ShootC ();
			}
		}
	}
	
	void FixedUpdate() {
		if (!IsGameRunning()) return;
		UpdateDragonPosition();
		UpdateBullets();
	}
	
	// Update is called once per frame
	void Update () {
		//statusLine.text = superAttackCharges.ToString()+" "+life.ToString();			
	}
	
	public void PlayerDamage(int d) {
		GetDragon().damageAudioSource.clip = soundManager.DragonDeath;
		GetDragon().damageAudioSource.Play();
		life --;
		GetComponent<ScoreGUI>().SetMaxMedals(life);
		if (life<=0) Death();
	}
	
	void Death() {
		Debug.Log("YOU LOST");
		SetMedal(GameManager.Medal.None);
		EndGame();
	}
	
	public void OnExit() {
		int result = 0;
		
				if (score > bronzeMedalScore) result = 1;
				if (score > silverMedalScore) result = 2;
				if (score > goldMedalScore) result = 3;
				if (result > life) result = life;
				
				if (result == 3) SetMedal(Medal.Gold);
				else if (result == 2) SetMedal(Medal.Silver);
				else if (result == 1) SetMedal(Medal.Bronze);
				else if (result == 0) SetMedal(Medal.None);
				EndGame ();
		
	}	
	
	

	
	
}
