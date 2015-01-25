using UnityEngine;
using System.Collections;

public class FontDefault : MonoBehaviour {

	public GUIStyle levelDisplayFont;
	// Use this for initialization
	void Awake () {
		Utilities.LevelDisplay(levelDisplayFont);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
