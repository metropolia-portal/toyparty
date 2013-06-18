using UnityEngine;
using System.Collections;

public class RandomMaterial : MonoBehaviour {
	
	public Material[] materialList;

	// Use this for initialization
	void Start () {
		Material mat = materialList[Random.Range(0, materialList.Length)];
		gameObject.renderer.material = mat;
		Debug.Log(renderer.material);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
