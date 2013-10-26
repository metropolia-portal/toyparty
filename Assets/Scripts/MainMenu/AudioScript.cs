using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioScript : MonoBehaviour 
{
	#region MEMBERS
	AudioSource audioSource;
	static AudioScript single;
	#endregion
	#region UNITY_METHODS
	void Awake() 
	{
		if(single == null)
		{
			single = this;
			transform.position = Camera.main.transform.position;
			DontDestroyOnLoad(gameObject);
		}else{
			DestroyImmediate(gameObject);
		}	
	}
	
	void Start () 
	{
		audioSource = GetComponent<AudioSource>();
		if(audioSource == null)gameObject.AddComponent<AudioSource>();
	}
	#endregion
	
	#region METHODS
	public IEnumerator FadeOutVolume()
	{
		while(audioSource.volume > 0f)
		{
			audioSource.volume -= Time.deltaTime;
			yield return null;
		}
		Destroy (gameObject);
	}
	#endregion
}
