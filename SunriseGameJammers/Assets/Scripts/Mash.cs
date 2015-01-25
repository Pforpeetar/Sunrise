using UnityEngine;
using System.Collections;

public class Mash : MonoBehaviour {

	//This player object will control most of what is going on in this game.
	public float mashDuration;		//How long can the player mash?
	public float fadeDuration;		//How long will it take to fade to black
	public int pressesToWin;		//How much presses needed to pass
	public LevelManager lvlmanager;
	public GameObject mashSlash;
	public GameObject fader;

	private int timesPressed = 0;	//Counter for # of times pressed
	private float currentTime;
	private float endTime;
	private float startLightFade;
	private float endLightFade;

	//Setup Timer
	//Spawn Enemies 
	void Start () {
		currentTime = Time.time;
		endTime = currentTime + mashDuration + 0.5f + fadeDuration; //Difference from endLightFade and endTime is 3.0 seconds
		startLightFade = currentTime + 0.5f;
		endLightFade = currentTime + 0.5f + fadeDuration;
		Color newA = new Color (fader.renderer.material.color.r, fader.renderer.material.color.g, fader.renderer.material.color.b, 0.0f);
		fader.renderer.material.color = newA;
	}
	
	// Update is called once per frame
	void Update () {
	
		//Update currentTime
		currentTime = Time.time;

		//Dim the lights (Fade to black)
		//Interpolation through time formula = A(1-t) + B(t)
		//Alpha 0.0 = Transparent, Alpha 1.0 = Opaque, so A = 0.0 and B = 1.0
		//We have t range from 0 to fadeDuration, but it needs to be scaled to a 0 to 1 range
		//1/fadeDuration * currentTime - startLightFade
		if (currentTime > startLightFade && currentTime < endLightFade) {
			float alpha = (1/fadeDuration) * (currentTime - startLightFade); //Never a negative number thankts to currentTime > startLightFade
			Color newA = new Color (fader.renderer.material.color.r, fader.renderer.material.color.g, fader.renderer.material.color.b, alpha);
			fader.renderer.material.color = newA;
		}

		//Allowed to mash when currentTime is within 3 seconds of the endTime
		bool mashCheck = false;
		if (endTime - currentTime < mashDuration && endTime - currentTime > 0.0f) {
			mashCheck = true;
			//Last transparency check on fader
			if (fader.renderer.material.color.a != 1.0f) {
				Color newA = new Color (fader.renderer.material.color.r, fader.renderer.material.color.g, fader.renderer.material.color.b, 1.0f);
				fader.renderer.material.color = newA;
			}
		}

		//If pushing "Action" and mashCheck == true...0
		//Spawn a slash effect for each press
		//Play a sound for each press (Or I could link sound with the prefab spawning...)
		if (Input.GetButtonDown ("Action") && mashCheck) {
			timesPressed++;
			//Spawn prefab
			Vector3 newPos = new Vector3 (Random.Range (-2.0f, 2.0f), Random.Range (-3.0f, 1.0f), -2.0f);
			Quaternion newRot = Quaternion.Euler (0, 0, Random.Range (0.0f, 360.0f));
			Instantiate(mashSlash, newPos, newRot);
			Debug.Log (timesPressed);
		}

		//Akuma mode ended, lights back on
		if (currentTime > endTime) {
			if (fader.renderer.material.color.a != 0.0f) {
				Color newA = new Color (fader.renderer.material.color.r, fader.renderer.material.color.g, fader.renderer.material.color.b, 0.0f);
				fader.renderer.material.color = newA;
			}
		}

		//1 second delay after above, check win/lose
		if (currentTime > endTime + 1.0f) {
			//Check win
			if (timesPressed >= pressesToWin) {
				lvlmanager.CompleteLevel();
			}
			//Else lose
			else {
				lvlmanager.FailLevel();
			}
		}
	}
}
