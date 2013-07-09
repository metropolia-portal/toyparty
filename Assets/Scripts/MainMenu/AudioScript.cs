using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioScript : MonoBehaviour {

	AudioSource audioSource;
	static AudioScript single;
	void Awake() {
		if(single == null)
		{
			single = this;
			transform.position = Camera.main.transform.position;
			DontDestroyOnLoad(gameObject);
		}else{
			DestroyImmediate(gameObject);
		}	
	}
	
	// Update is called once per frame
	void Start () {
		audioSource = GetComponent<AudioSource>();
		if(audioSource == null)gameObject.AddComponent<AudioSource>();
	}
	public IEnumerator FadeOutVolume(){
		while(audioSource.volume > 0f){
			audioSource.volume -= Time.deltaTime;
			yield return null;
		}
		Destroy (gameObject);
	}
}
