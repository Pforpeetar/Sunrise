using UnityEngine;
using System.Collections;

public class QTE : MonoBehaviour
{

		public LevelManager lManager;
		public float timeBeforeQTE;
		public float QTEresponseTime;
		public float humanLeniency;
		private float currentTime;
		private bool QTEactive;
		private bool lvlComplete;
		private float upperBoundsTime;
		public Texture2D backgroundImage;
		public GameObject player;
		public GameObject enemy;
		public Texture2D blackscreen;
		
		// Use this for initialization
		void Start ()
		{
				//lManager.FailLevel();
				currentTime = 0f;
				QTEactive = false;
				lvlComplete = false;
				upperBoundsTime = QTEresponseTime + timeBeforeQTE + humanLeniency;
		}
	
		// Update is called once per frame
		void Update ()
		{
				currentTime += Time.deltaTime;
				if ((currentTime < upperBoundsTime) && (currentTime > timeBeforeQTE)) {
						QTEactive = true;
				} else {
						QTEactive = false;
				}
				if (QTEactive) {
						if (Input.GetButtonDown ("Action")) {
								lvlComplete = true;
								Invoke ("FinishLevel", 2f);
						}
				}
				if (currentTime > upperBoundsTime + 1) {

						if (!lvlComplete) {
								lManager.FailLevel ();
						} else {
						}
				}
		}

		void FinishLevel ()
		{
				lManager.CompleteLevel ();
		}

		void OnGUI ()
		{
				//GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundImage);
				if (QTEactive) {
						GUI.Label (new Rect (Screen.width / 4, Screen.height / 30, Screen.width / 2, Screen.height / 10), "ACT NOW",Utilities.LevelDisplay(null));
				}
		}
}
