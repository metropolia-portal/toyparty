using UnityEngine;
using System.Collections;

public class GUIBackground : MonoBehaviour 
{
	#region UNITY_METHODS
	void Start () 
	{
		GetComponent<GUITexture>().pixelInset = new Rect(-Screen.width/2, -Screen.height/2, Screen.width, Screen.height);
	}
	#endregion
}
