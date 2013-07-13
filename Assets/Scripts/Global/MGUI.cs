using UnityEngine;
using System.Collections;

// GUI utils, and common settings for GUI
public static class MGUI {
	// Unit for margins
	public static float Margin = Screen.width/45;
	public static float hoverButtonSizeIncrease = 1.2f;
	public static GUIStyle NoStyle = new GUIStyle();
	
	//width of a standart button in menus, used for same sizes in different scenes
	public static float menuButtonWidth = Screen.width/7;
	
	public static bool HoveredButton(Rect pos, Texture image) {
		Rect rect = pos;
		if(pos.Contains(InputManager.MouseScreenToGUI())) {
			rect = new Rect(pos.x - pos.width * (hoverButtonSizeIncrease - 1) / 2f, pos.y - pos.height * (hoverButtonSizeIncrease - 1) / 2f, pos.width * hoverButtonSizeIncrease , pos.height * hoverButtonSizeIncrease);
		}
		
		return GUI.Button(rect, image, NoStyle);
	}
	
	//Returns inner rectangle centered in outer rectangle, inner.x and y are used for offset
	public static Rect centerInRect(Rect inner, Rect outer) {
		return new Rect(outer.x + (outer.width - inner.width)/2f + inner.x, outer.y + (outer.height - inner.height)/2f +  inner.y,inner.width, inner.height);
	}
}
