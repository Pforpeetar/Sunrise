using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{

		private bool levelComplete;
		private bool levelFailed;
		// Use this for initialization
		void Start ()
		{
	
				levelComplete = false;
				levelFailed = false;
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Input.GetButtonDown ("Action")) {
						if (levelComplete) {		
								Application.LoadLevel (Application.loadedLevel + 1);
		
						}
						if (levelFailed) {
								Application.LoadLevel (Application.loadedLevel);
						}
				}
		}

		public void CompleteLevel ()
		{
				levelComplete = true;
		}

		public void FailLevel ()
		{
				levelFailed = true;
		}

		void OnGUI ()
		{
				if (levelComplete) {
						GUI.Label (new Rect (50, 50, 100, 100), "Level Complete!");
				}
				if (levelFailed) {
						GUI.Label (new Rect (50, 50, 100, 100), "Level Failed...");
				}
		}

		void OnLevelWasLoaded(int level)
	{
		Debug.Log ("Level " + Application.loadedLevel + " loaded");
	}
}
