using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{

		private bool levelComplete;
		private bool levelFailed;
		public GUIStyle displayStyle;
		// Use this for initialization
		void Start ()
		{
				displayStyle = Utilities.LevelDisplay(displayStyle);
				levelComplete = false;
				levelFailed = false;
		}
	
		// Update is called once per frame
		void Update ()
		{
		if (Input.GetButtonDown("Escape"))
		{
			Application.Quit();
		}
				if (Input.GetButtonDown ("Action")) {
						if (levelComplete) {		
								Application.LoadLevel (Application.loadedLevel + 1);
		
						}
						if (levelFailed) {
								Application.LoadLevel (Application.loadedLevel);
						}
				}
		}

		public void CompleteLevel () //called by external script when level is complete
		{
				if (!levelFailed) {
				levelComplete = true;
		}
		}

	public void FailLevel () //called by external script when user messed up and fails level
		{
		if (!levelComplete) {
			levelFailed = true;
		}
		}

		void OnGUI ()
		{
				if (levelComplete) {
						GUI.Label (new Rect (Screen.width/4, Screen.height/30, Screen.width/2, Screen.height/10), "Level Complete!",displayStyle);
			//GUI.Label (new Rect (Screen.width/4, Screen.height - Screen.height/30, Screen.width/2, Screen.height/100), "Press Action to Advance...",displayStyle);
			GUI.Label (new Rect (Screen.width/4, Screen.height/30, Screen.width/2, Screen.height/100), "Press Action to Advance...",displayStyle);

		}
				if (levelFailed) {
			GUI.Label (new Rect (Screen.width/4, Screen.height/30, Screen.width/2, Screen.height/100), "Level Failed...",displayStyle);
			GUI.Label (new Rect (Screen.width/4, 3* Screen.height/30, Screen.width/2, Screen.height/100), "Press Action to Try Again.",displayStyle);

		}
		}

		void OnLevelWasLoaded(int level)
	{
		//Debug.Log ("Level " + Application.loadedLevel + " loaded");
	}
}
