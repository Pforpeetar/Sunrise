using UnityEngine;
using System.Collections;

public class Mash : MonoBehaviour {

	//This player object will control most of what is goign on in this game.

	public Light spotlight;			//Spotlight object in the scene
	public float mashDuration;		//How long can the player mash?
	public int pressesToWin;		//How much presses needed to pass
	public LevelManager lvlmanager;

	private int timesPressed = 0;	//Counter for # of times pressed
	private float currentTime;
	private float endTime;
	private float startLightFade;
	private float endLightFade;

	// Use this for initialization
	void Start () {
		currentTime = Time.time;
		endTime = currentTime + mashDuration + 1.0f; //Difference from endLightFade and endTime is 3.0 seconds
		startLightFade = currentTime + 0.5f;
		endLightFade = currentTime + 1.0f;

	}
	
	// Update is called once per frame
	void Update () {
	
		//Update currentTime
		currentTime = Time.time;

		//Interpolate light intesity from 8 to 0 within the 0.5 second difference from startLightFade and endLightFade
		//Percentage = A(1-t) + B(t), where A is the start point, B is the end point, over a length of t
		//Since the duration is 0.5 seconds, we can double it here to get a 0.0 - 1.0 second range
		if (currentTime > startLightFade && currentTime < endLightFade) {
			//newIntensity is a number between 8 and 0
			//currentTime - startLightFade will not return a negative number, protected by if currentTime > startLightFade
			float newIntensity = 8 * (1 - (2 * (currentTime - startLightFade))); //Since 0 * anything = 0, cut out the B(t) part
			spotlight.intensity = newIntensity;
		}

		//Allowed to mash when currentTime is within 3 seconds of the endTime
		bool mashCheck = false;
		if (endTime - currentTime < mashDuration && endTime - currentTime > 0.0f) {
			mashCheck = true;
			spotlight.intensity = 0; //Just in-case we missed it above (Which it will)
		}

		//If pushing "Action" and mashCheck == true...
		if (Input.GetButtonDown ("Action") && mashCheck) {
			timesPressed++;
			Debug.Log (timesPressed);

		}

		//Akuma mode ended, lights back on
		if (currentTime > endTime) {
			spotlight.intensity = 8;
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
