using UnityEngine;
using System.Collections;

public class PaddleAnimation : MonoBehaviour {

	
	#region PUBLIC_MEMBERS
	public int spritesAmount = 5;
	
	public int leftIndexOffset = 3;
	public int rightIndexOffset = 1;
	
	public int idleIndex = 0;
	
	public int enableAnimationOffset = -1;

	public float convertionAnimationFPS = 0.2f;
	public Texture normalSpriteSheet;
	#endregion
	#region PRIVATE_MEMBERS
	
	float displacementLimit = 0.05f;
	float holdTime = 0.3f;
	
	bool movementAnimationEnable = true;
	float lastChangeTime = 0;
	Vector3 lastPosition;
	#endregion

	
	// Update is called once per frame
	void Update () {
		if(movementAnimationEnable) {
			AnimateMovement();
		}
	}
	
	public IEnumerator AnimateEnable() {
		if(enableAnimationOffset >= 0) yield return StartCoroutine(AnimateFrames(enableAnimationOffset, idleIndex, convertionAnimationFPS));
	}
	
	public IEnumerator AnimateDisable() {
		if(enableAnimationOffset >= 0) yield return StartCoroutine(AnimateFrames(idleIndex, enableAnimationOffset, convertionAnimationFPS));
	}
	
	IEnumerator AnimateFrames(int begin, int end, float fps) {
		movementAnimationEnable = false;
		int offset=1;
		if(begin > end) offset = -1;
		
		for( int i = begin; i!= end + offset; i += offset) {
			setFrame(i);
			yield return new WaitForSeconds(fps);
		}
		
		movementAnimationEnable = true;
	}
	void AnimateMovement() {	
		int newInd;
		if(transform.position != lastPosition )
		{
			if(Mathf.Abs(transform.position.x-lastPosition.x) < displacementLimit) {
				if(transform.position.x > lastPosition.x)
					newInd = rightIndexOffset;
				else
					newInd = leftIndexOffset;
			} else {
				if(transform.position.x > lastPosition.x)
					newInd = rightIndexOffset + 1;
				else
					newInd = leftIndexOffset + 1;
			}
		}
		else
			newInd = idleIndex;
			
		if(newInd != currFrameInd && Time.time - lastChangeTime > holdTime) {
			
			setFrame(newInd);
			lastChangeTime = Time.time;
		}
		
		lastPosition = transform.position;	
	}
	
	int currFrameInd = 0;
	
	void setFrame(int ind) {
		currFrameInd = ind;
		
		Vector2 size = new Vector2( 1f / spritesAmount, 1f );		
		
		transform.Find ("Plane").renderer.material.mainTexture = normalSpriteSheet;
		
		transform.Find ("Plane").renderer.material.SetTextureScale ("_MainTex", size);
		
		Vector2 offset = new Vector2(size.x*ind,0);
		transform.Find ("Plane").renderer.material.SetTextureOffset("_MainTex", offset);
	}
}
