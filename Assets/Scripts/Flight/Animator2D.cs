using UnityEngine;
using System.Collections;

public class Animator2D : MonoBehaviour {
	
	public bool autoStart = false;
	public string defaultAnimation = "idle";
	
	Animation2D currentAnimation;
	
	public void PlayAnimation(string name) {
		if (currentAnimation)
			currentAnimation.Stop();
		foreach (Animation2D anim in gameObject.GetComponents<Animation2D>()) {
			if (anim.GetName()==name) {
				currentAnimation = anim;
				break;
			}
		}
		currentAnimation.Restart();
		currentAnimation.Play();
	}
	
	public Animator2D Child(string name) {
		return transform.FindChild(name).GetComponent<Animator2D>();
	}

	// Use this for initialization
	void Start () {
		if (autoStart)
			PlayAnimation(defaultAnimation);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
