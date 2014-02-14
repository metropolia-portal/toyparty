using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioScript : MonoBehaviour 
{
	#region MEMBERS
	AudioSource _audioSource;
	#endregion
	
	#region UNITY_METHODS
	void Awake() 
	{
		transform.position = Camera.main.transform.position;	
	}
	
	void Start () 
	{
		_audioSource = GetComponent<AudioSource>();
		if(_audioSource == null) gameObject.AddComponent<AudioSource>();
	}
	#endregion
	
	#region METHODS
	public bool FadeOutVolume(float speed)
	{
		_audioSource.volume -= speed;
		if(_audioSource.volume <= 0f)return true;
		return false;
	}
	#endregion
}
