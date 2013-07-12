using UnityEngine;
using System.Collections;

public class ParticleGenerator : MonoBehaviour {
	
	public GameObject particlePrefab;
	public float life = 0.5f;
	public float delayAvg = 0.5f;
	public float windupDelay = 0;
	float delay;
	
	// Use this for initialization
	void Start () {
		delayNext();
	}
	
	void delayNext() {
		delay = delayAvg*Random.Range(0.5f, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (windupDelay > 0) {
			windupDelay -= Time.deltaTime;
			return;
		}
		delay -= Time.deltaTime;
		if (delay<0) {
			Instantiate(particlePrefab, transform.position+Vector3.up*Random.Range(-0.3f,0.3f)+ Vector3.forward*Random.Range(-0.3f,0.3f), Quaternion.identity);
			delayNext();
		}
		life -= Time.deltaTime;
		if (life < 0) Destroy(gameObject);
	}
}
