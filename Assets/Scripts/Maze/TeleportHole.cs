using UnityEngine;
using System.Collections;

public class TeleportHole : MonoBehaviour {
	
	public TeleportHole[] exitHoles;
	public Transform exit;
	TeleportHole target;
	public bool end = false;
	
	public float cooldown = 0;
	
	float delay;
	
	int state = 0;
	Mouse mouse;
	
	// Use this for initialization
	void Start () {
		mouse = GameObject.Find("Mouse").GetComponent<Mouse>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (cooldown > 0) cooldown -= Time.deltaTime;
		if (state == 1) {
			delay -= Time.deltaTime;
			mouse.gameObject.transform.position = mouse.gameObject.transform.position + (delay)*(transform.position-mouse.gameObject.transform.position+Vector3.up)/5;
			
			if (delay<0) {
				
				mouse.gameObject.transform.position= transform.position + Vector3.up;
				//GameObject.Find("Mouse").GetComponent<Mouse>().transform.position = target.exit.position;
				state = 2;
				delay = 1;
			}
			
		}
		
		if (state == 2) {
			delay -= Time.deltaTime;
			if (delay > 0.1f) {
				Debug.Log(delay);
				mouse.model.localScale = new Vector3(1* delay,1,1* delay) ;
				mouse.model.rotation *= Quaternion.Euler(0,delay*180,0);
			}
			if (delay <0) {
				target = exitHoles[Random.Range(0,exitHoles.Length)];
				target.cooldown = 3;
				mouse.gameObject.transform.position = target.transform.position;
				state = 3;
				delay = 1f;
				if (target.end) {
					GameObject.Find("GameManager").GetComponent<MazeGameManager>().OnExit();
				}
			}
		}
		
		if (state == 3) {
			delay -= Time.deltaTime;
			if ((delay > 0.1f) && (delay < 1)) {
				
				mouse.model.localScale = new Vector3(1,1,1) * (1f-delay);
				mouse.model.rotation *= Quaternion.Euler(0,delay*180,0);
			}
			if (delay < 0) {
				state = 4;
				delay = 0.5f;
			}
			
		}
		
		if (state == 4) {
			mouse.gameObject.transform.position = mouse.gameObject.transform.position + (delay)*(target.exit.position-mouse.gameObject.transform.position+Vector3.up)/10;
			delay -= Time.deltaTime;
			if (delay < 0) {
				mouse.gameObject.transform.localScale = new Vector3(1,1,1);
				mouse.SetSpeedModifier(1);
				state = 0;
			}
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (cooldown > 0) return;
		
		if (other.gameObject.tag == "Player") {
			cooldown = 5;
			state = 1;
			mouse.SetSpeedModifier(0);
			delay = 0.5f;
		}
	}
	
}
