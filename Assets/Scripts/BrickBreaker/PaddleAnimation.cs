using UnityEngine;
using System.Collections;

public class PaddleAnimation : MonoBehaviour {

	public Texture normalSpriteSheet;
	
	public int spritesAmount = 5;
	
	public int leftIndexOffset = 3;
	public int rightIndexOffset = 1;
	
	public int idleIndex = 0;
	
	public int enableAnimationOffset = -1;

	public float convertionAnimationFPS = 0.2f;
	
	float minDisplacement = 0.01f; //min displaycement for the robot to start animating
	float displacementLimit = 0.05f;
	float holdTime = 0.3f;
	

	float movementAnimationSPF = 0.2f;
	
	bool movementAnimationEnable = true;
	
	public IEnumerator AnimateEnable() {
		//yield return StartCoroutine(AnimateFrames(enableAnimationOffset, spritesAmount-1));
		//print ("animation enable start");
		if(enableAnimationOffset >= 0) yield return StartCoroutine(AnimateFrames(enableAnimationOffset, idleIndex, convertionAnimationFPS));
		//print ("animation enable finished");
	}
	
	public IEnumerator AnimateDisable() {
		//print ("animation disable start");
		//yield return new WaitForSeconds(1.0f);
		if(enableAnimationOffset >= 0) yield return StartCoroutine(AnimateFrames(idleIndex, enableAnimationOffset, convertionAnimationFPS));
		//print ("animation disable finished");;
	}
	
	IEnumerator AnimateFrames(int begin, int end, float fps) {
		movementAnimationEnable = false;
		
		//print("AnimateFrames");
		
		int offset=1;
		if(begin > end) offset = -1;
		
		for( int i = begin; i!= end + offset; i += offset) {
		//print ("frame " + i + " at time "+ Time.time);
			setFrame(i);
			yield return new WaitForSeconds(fps);
		}
		
		movementAnimationEnable = true;
	}
	
	int[] movementArray;
	// Use this for initialization
	void Start () {
		
		movementArray = new int[5] { leftIndexOffset, leftIndexOffset+1, idleIndex, rightIndexOffset, rightIndexOffset+1 };
		//setFrame(0);
		//setFrame(idleIndex);
	}
	
	// Update is called once per frame
	void Update () {
		if(movementAnimationEnable) {
			AnimateMovement();
		}
	}
	
	float lastChangeTime = 0;
	Vector3 lastPosition;
	
	int currInd = 0;
	
	
	int oldInd;
	
	void AnimateMovement() {
		
//		int targetInd = 0;
//		
//		if(Mathf.Abs(transform.position.x - lastPosition.x)> minDisplacement) {
//			if( transform.position.x > lastPosition.x )
//				targetInd = 4;
//			else if ( transform.position.x < lastPosition.x )
//				targetInd = 0;
//		}
//		else
//			targetInd = 2;
//		
//		//print (targetInd);
//		print (lastPosition.x - transform.position.x);
//
//
//		if( currInd != targetInd && Time.time - lastChangeTime > movementAnimationSPF ) {
//			
//			int offset = 1;
//			if(currInd > targetInd) offset = -1;
//			
//			currInd += offset;	
//			
//			lastChangeTime = Time.time;
//			setFrame( movementArray[currInd] );
//			//print (movementArray[currInd] );
//		}
		
		
//		print (Mathf.Abs(transform.position.x-lastPosition.x));
//		int newInd;
//		
//		if(transform.position != lastPosition )
//		{
//			if(transform.position.x > lastPosition.x)
//				newInd = rightIndexOffset;
//			else
//				newInd = leftIndexOffset;
//		}
//		else
//			newInd = idleIndex;
//		
//		if(oldInd != newInd) {
////			if(newInd == idleIndex) 
////				setFrame(newInd);
////			else 
//			if(newInd == rightIndexOffset)
//				StartCoroutine(AnimateFrames(rightIndexOffset,rightIndexOffset+1, movementAnimationFPS));
//			else if(newInd == leftIndexOffset)
//				StartCoroutine(AnimateFrames(leftIndexOffset,leftIndexOffset+1,movementAnimationFPS));
//		}
		
//		if(newInd != currFrameInd && Time.time - lastChangeTime > holdTime) {
//			
//			if(newInd == idleIndex) 
//				setFrame(newInd);
//			else if(newInd == rightIndexOffset)
//				StartCoroutine(AnimateFrames(rightIndexOffset,rightIndexOffset+1));
//			else
//				StartCoroutine(AnimateFrames(leftIndexOffset,rightIndexOffset+1));
//		}
		
//		
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
		
		oldInd = newInd;
		
		lastPosition = transform.position;	
	}
	
	int currFrameInd = 0;
	
	void setFrame(int ind) {
		currFrameInd = ind;
		
		Vector2 size = new Vector2( 1f / spritesAmount, 1f );
		
		//int w  = idleTexture.width  / spritesAmount;
		//int h = idleTexture.height;
		
		
		transform.Find ("Plane").renderer.material.mainTexture = normalSpriteSheet;
		
		transform.Find ("Plane").renderer.material.SetTextureScale ("_MainTex", size);
		
		Vector2 offset = new Vector2(size.x*ind,0);
		transform.Find ("Plane").renderer.material.SetTextureOffset("_MainTex", offset);
	}
}
