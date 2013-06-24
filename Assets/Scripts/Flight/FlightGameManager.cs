using UnityEngine;
using System.Collections;

public class FlightGameManager : GameManager {
	
	public Texture2D padTexture;
	public Texture2D padNeutralTexture;
	public Texture2D buttonATexture;
	public Texture2D buttonBTexture;
	public GameObject dragon;
	public Bounds dragonBounds;
	public GUIText statusLine;
	public GameObject bulletPrefab;
	public GameObject fairyPrefab;
	public float attackSpeed = 0.3f;
	
	public float fairyDelayAvg = 10f;
	float fairyDelay;
	
	bool shootA = false;
	bool shootB = false;
	
	int superAttackLeft = 3;
	
	float attackCooldown = 0;
	float superAttackCooldown = 0;
	float superAttackDelay = 1f;
	float invulnurability = 0;
	public float hitInvulnurabilityTime = 1f;
	
	public int life = 10;
	
	// Use this for initialization
	void Start () {
	
		fairyDelay = Random.Range(fairyDelayAvg*1.5f,fairyDelayAvg*2f);
	
	}
	
	Vector2 padCenter = new Vector2(Screen.width/10,Screen.height - Screen.width/10);
	Vector2 buttonACenter = new Vector2(Screen.width - 1.5f*Screen.width/15,Screen.height - Screen.width/15);
	Vector2 buttonBCenter = new Vector2(Screen.width - 1*Screen.width/15,Screen.height - 3*Screen.width/15);
	
	float buttonRadius = Screen.width/15;
	float padRadius = Screen.width/10;
	float padRadiusNeutral = Screen.width/40;
	string stat = "";
	Vector2 analogPad;
	
	int padTouch = -1;
	
	// Update is called once per frame
	void Update () {
		Vector2 mpos;
		
		fairyDelay -= Time.deltaTime;
		if ( fairyDelay <=0 ) {
			Instantiate(fairyPrefab, new Vector3(10,0,Random.Range(-5f, 5f)), Quaternion.identity);
			fairyDelay = Random.Range(fairyDelayAvg*1.5f,fairyDelayAvg*2f);
		}
		
		bool padDetected = false;
		bool buttonADetected = false;
		bool buttonBDetected = false;	
		
		
		padTouch = -1;
#if UNITY_ANDROID || UNITY_IOS
		if (padTouch > -1) {
			if ((Input.touches.Length<padTouch)||(Input.touches[padTouch].phase == TouchPhase.Ended)||(Input.touches[padTouch].phase == TouchPhase.Canceled)) { // HER FACE
				padTouch = -1;
			}
			
			mpos = new Vector2(Input.GetTouch(padTouch).position.x, Screen.height-Input.GetTouch(padTouch).position.y);
			
			analogPad = (Vector2.Scale((padCenter - mpos),new Vector2(-1,1))/padRadius);
			
			if (analogPad.magnitude>1) analogPad.Normalize();
			
			analogPad *= 0.1f;
			
			padDetected = true;
			
		}
		
		for (int i=0; i<Input.touchCount; i++) {
		
			mpos = new Vector2(Input.GetTouch(i).position.x, Screen.height-Input.GetTouch(i).position.y);
			
			if (!padDetected) { 
				padDetected = checkPad(mpos);
				if (padDetected) padTouch = i;
			}
			if (!buttonADetected) buttonADetected = checkButtonA(mpos);
			if (!buttonBDetected) buttonBDetected = checkButtonB(mpos);
			

		}
		
#else	
		mpos = new Vector2(Input.mousePosition.x, Screen.height-Input.mousePosition.y);
		
		if (!padDetected) padDetected = checkPad(mpos);
		if (!buttonADetected) buttonADetected = checkButtonA(mpos);
		if (!buttonBDetected) buttonBDetected = checkButtonB(mpos);
#endif		

		if (padDetected) {
			float dragonX = Mathf.Clamp(dragon.transform.position.x + analogPad.x, dragonBounds.min.x, dragonBounds.max.x);
			float dragonZ = Mathf.Clamp(dragon.transform.position.z + analogPad.y, dragonBounds.min.y, dragonBounds.max.y);
			dragon.transform.position = new Vector3(dragonX, 0, dragonZ);
		}
		
		stat = "";
	
		
		if (invulnurability>0)  { 
			invulnurability -= Time.deltaTime;
			stat += "i";
		}
		
		if (padDetected) stat += "P";
		if (buttonADetected) stat += "A";
		if (buttonBDetected) stat += "B";
		
		stat = "    "+Input.touchCount.ToString()+" "+padTouch.ToString();
		for (int i=0; i<Input.touchCount; i++) {
			stat += "\n             "+Input.GetTouch(i).position.ToString()+" "+Input.GetTouch(i).phase.ToString();
		}
		
		stat += "\n====";
		
		for (int i=0; i<Input.touches.Length; i++) {
			stat += "\n             "+Input.touches[i].position.ToString()+" "+Input.touches[i].phase.ToString();
		}
		
		
		if (buttonADetected) shootA = true;
		if (buttonBDetected) shootB = true;
		
	
		statusLine.text = stat;
		
		attackCooldown -= Time.deltaTime;
		
		if (attackCooldown <= 0) {
			attackCooldown = attackSpeed;
			if (shootA) ShootA();
			shootA = false;
		}
		
		if (superAttackCooldown>0) superAttackCooldown -= Time.deltaTime;
		
		if ((superAttackCooldown <= 0)&&buttonBDetected) {
			superAttackCooldown = superAttackDelay;
			if (buttonBDetected) ShootB();
			buttonBDetected = false;
		}		
			

	}
	
	public void AddLife(int l) {
		life += l;
	}
	
	public void AddPower(int p) {
		superAttackLeft += p;
	}
	
	public void PlayerDamage() {
		if (invulnurability<=0) {
			invulnurability = hitInvulnurabilityTime;
			life --;
			Debug.Log(life);
			if (life<=0) {
				Debug.Log("DED");
				Destroy(dragon);
			}
		}
	}
	
	void ShootA() {
		Instantiate(bulletPrefab, dragon.transform.position, Quaternion.identity);
	}
	
	void ShootB() {
		if (superAttackLeft>0) {
			superAttackLeft --;
			
			for (int i=-3; i<=3; i++) {
				Instantiate(bulletPrefab, dragon.transform.position, Quaternion.Euler(0,i*10,0));
			}
		}
	}
	
	bool checkPad(Vector2 mpos) {
		float d = Vector2.Distance(mpos, padCenter);
		
		if ((d<padRadius)&&(d>padRadiusNeutral)) {
			
			

			analogPad = (Vector2.Scale((padCenter - mpos),new Vector2(-1,1))/padRadius);
			
			if (analogPad.magnitude>1) analogPad.Normalize();
			
			analogPad *= 0.1f;
			
			
			return true;
			
			
		} else
			return false;
	}
	
	bool checkButtonA(Vector2 mpos) {
		float d = Vector2.Distance(mpos, buttonACenter);
		if (d<buttonRadius) {
			return true;		
		} else
			return false;
	}
	bool checkButtonB(Vector2 mpos) {
		float d = Vector2.Distance(mpos, buttonBCenter);
		if (d<buttonRadius) {
			return true;		
		} else
			return false;
	}	
	
	void OnGUI() {
		GUI.DrawTexture(new Rect(padCenter.x-padRadius,padCenter.y-padRadius,padRadius*2,padRadius*2), padTexture);
		GUI.DrawTexture(new Rect(padCenter.x-padRadiusNeutral,padCenter.y-padRadiusNeutral,padRadiusNeutral*2,padRadiusNeutral*2), padNeutralTexture);
		
		
		GUI.DrawTexture(new Rect(buttonACenter.x-buttonRadius,buttonACenter.y-buttonRadius,buttonRadius*2,buttonRadius*2), buttonATexture);
		GUI.DrawTexture(new Rect(buttonBCenter.x-buttonRadius,buttonBCenter.y-buttonRadius,buttonRadius*2,buttonRadius*2), buttonBTexture);
		
	}
}
