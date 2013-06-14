using UnityEngine;
using System.Collections;

public class FloatingScore : MonoBehaviour {
	
	 public float liveTime = 2f;
	 public float startScoreScale = 0.5f;
	 public float maxScoreScale = 1f; //initial score scale should be set in prefub
	 public float scaleTime = 1f;

	void Update(){
		transform.Translate(0, Time.deltaTime, 0);	
	}
	
	//"launches" score
	public void FireScore(string scoreText){
		TextMesh textMesh = GetComponent<TextMesh>();
		textMesh.text = scoreText;
		
		StartCoroutine(GrowSize());
	}
	
	public IEnumerator GrowSize() {
		
		float size = startScoreScale;
		
		while(size < maxScoreScale) {
			size = size + Time.deltaTime * (maxScoreScale - startScoreScale) / scaleTime;
			transform.localScale = Vector3.one * size;
			yield return null;
		}
		
		Destroy (gameObject, liveTime - scaleTime);
		
	}
}
