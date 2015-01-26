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
		public GameObject mashSlash;
		public GameObject fader;
		private bool fadeActive;
		private bool peterYoucanOnlyPressItOnceForThisStage;
		public AudioClip slash;
		public AudioClip death;
		private bool levelComplete;
		private bool levelFailed;
	public GUIStyle displayStyle;
		// Use this for initialization
		void Start ()
		{
				//lManager.FailLevel();
				currentTime = 0f;
				QTEactive = false;
				lvlComplete = false;
				upperBoundsTime = QTEresponseTime + timeBeforeQTE + humanLeniency;
				Color newA = new Color (fader.renderer.material.color.r, fader.renderer.material.color.g, fader.renderer.material.color.b, 0.0f);
				fader.renderer.material.color = newA;
				fadeActive = false;
				peterYoucanOnlyPressItOnceForThisStage = true;
		levelComplete = false;
		levelFailed = false;
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
				if (Input.GetButtonDown ("Action") && (peterYoucanOnlyPressItOnceForThisStage)) {
						peterYoucanOnlyPressItOnceForThisStage = false;
						if (QTEactive) {
								lvlComplete = true;
						}
				}
				if ((currentTime > upperBoundsTime + .25f) && (!fadeActive)) {
						fadeActive = true;
						StartCoroutine (FadeScreenThenUnfade ());
		
				}
		}

		void JudgeLevel ()
		{
				if (lvlComplete) {
						lManager.CompleteLevel (false);
			levelComplete = true;
				} else {
						lManager.FailLevel (false);
			levelFailed = true;
				}
				
		}

		IEnumerator FadeScreenThenUnfade ()
		{
				float fadeDuration = 0;
				GameObject temp = (GameObject)Instantiate (mashSlash, new Vector3 (0, 0, 0), Quaternion.Euler (0, 0, 0));
				temp.transform.localScale = new Vector3 (4, 4, 1);
				if (audio&&slash)
				{
					audio.PlayOneShot(slash,1);
					audio.PlayOneShot(slash,1);
				}
				GameObject temp2 = (GameObject)Instantiate (mashSlash, new Vector3 (0, 0, 0), Quaternion.Euler (0, 0, 0));
				temp2.transform.localScale = new Vector3 (-4, 4, 1);
				Invoke ("JudgeLevel", 2f);
				while (fadeDuration < 1.1f) {
						fadeDuration += .1f;
						float alpha = fadeDuration;
						Color newA = new Color (fader.renderer.material.color.r, fader.renderer.material.color.g, fader.renderer.material.color.b, alpha);
						fader.renderer.material.color = newA;
						yield return new WaitForSeconds (.1f);
				}
				player.BroadcastMessage ("Stop");
				enemy.BroadcastMessage ("Stop");
				if (audio && death)
				{
					audio.PlayOneShot(death,.3f);
					audio.PlayOneShot(death,.3f);
				}
				if (lvlComplete) {
						//enemy.transform.Rotate (new Vector3 (0, 0, 90));
			Animator animator = (Animator)enemy.gameObject.GetComponent ("Animator");
			animator.SetBool("Dead",true);					
				} else {
					//	player.transform.Rotate (new Vector3 (0, 0, -90));
			Animator animator = (Animator)player.gameObject.GetComponent ("Animator");
			animator.SetBool("Dead",true);
				}
		}

		void OnGUI ()
		{
				//GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundImage);
				if (QTEactive) {
						GUI.Label (new Rect (Screen.width / 4, Screen.height / 30, Screen.width / 2, Screen.height / 10), "ACT NOW", Utilities.LevelDisplay (null));
				}
		if (levelComplete) {
			GUI.Label (new Rect (Screen.width/4, Screen.height/30, Screen.width/2, Screen.height/10), "Level Complete!",displayStyle);
			//GUI.Label (new Rect (Screen.width/4, Screen.height - Screen.height/30, Screen.width/2, Screen.height/100), "Press Action to Advance...",displayStyle);
			GUI.Label (new Rect (Screen.width/4, 3*Screen.height/30, Screen.width/2, Screen.height/100), "Press Action to Advance...",displayStyle);
			
		}
		if (levelFailed) {
			GUI.Label (new Rect (Screen.width/4, Screen.height/30, Screen.width/2, Screen.height/100), "Level Failed...",displayStyle);
			GUI.Label (new Rect (Screen.width/4, 3* Screen.height/30, Screen.width/2, Screen.height/100), "Press Action to Try Again.",displayStyle);
			
		}
		}
}
