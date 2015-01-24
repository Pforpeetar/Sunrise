using UnityEngine;
using System.Collections;

public class Mash : MonoBehaviour {

	private int timesPressed = 0;
	private float currentTime;
	private float endTime;

	// Use this for initialization
	void Start () {
		currentTime = Time.time;
		endTime = currentTime + 2.5f;
	}
	
	// Update is called once per frame
	void Update () {
	
		currentTime = Time.time;

		if (Input.GetButtonDown ("Action") && currentTime < endTime) {
			timesPressed++;
			Debug.Log (timesPressed);
		}
	}
}
