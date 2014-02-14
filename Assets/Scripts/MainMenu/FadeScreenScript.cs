using UnityEngine;
using System.Collections;

public class FadeScreenScript : MonoBehaviour {

	#region MEMBERS
	public Texture texture;
	
	Rect _rect;
	GUIStyle _style = new GUIStyle();
	Color _color;
	#endregion
	
	#region UNITY_METHODS
	void Awake()
	{
		_rect = new Rect(0,0,Screen.width, Screen.height);
		_style.fixedWidth = 0;
		_style.fixedHeight = 0;
		_style.stretchHeight = true;
		_style.stretchWidth = true;
		DontDestroyOnLoad(gameObject);
	}

	void OnGUI()
	{
		GUI.depth = 1;
		GUI.color = _color; 
		GUI.DrawTexture(_rect,texture,ScaleMode.StretchToFill);	
	}
	#endregion
	
	#region METHODS	
	public bool FadeBlkScreen(float speed)
	{
		float __speed = speed;
		if(_color.a == 1)__speed *= -1;
		
		float __timer = 0;
		
		if(__timer < 1)
		{
			__timer += Time.deltaTime * speed;
			_color.a += __speed *Time.deltaTime;
			return false;
		}
		if(__speed < 0)_color.a = 0;
		return true;
	}
	#endregion
}
