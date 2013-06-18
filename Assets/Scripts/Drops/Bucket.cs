using UnityEngine;
using System.Collections;

public class Bucket : MonoBehaviour {
	
	InputManager inputManager;
	public Camera cam;
	public DropsGameManager gameManager;
	
	// Use this for initialization
	void Start () {
		inputManager = GetComponent<InputManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager.IsGameRunning()) {
			Ray ray = cam.ScreenPointToRay(inputManager.GetCursorPosition());
		   	RaycastHit hit;
	
		    if (Physics.Raycast(ray, out hit)) {
				transform.position = new Vector3(
					Mathf.Clamp(hit.point.x, -9f, 9f)
					, 1, -4);
			}
		}
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Toy")) {
			gameManager.OnToy();
		} else
		if (other.CompareTag("Bomb")) {
			Debug.Log("Kablam, sir.");
			gameManager.OnBomb();
		} else return;
		
		Destroy(other.gameObject);
		
	}
}
