using UnityEngine;
using System.Collections;

public class HoldRelease : MonoBehaviour {
	public LevelManager levMan;
	public GameObject slider;
	private SpriteRenderer sliderBar; //slider bar sprite to be used
	private Vector3 sliderVector; //Vector of slider
	private float sliderScale; //Scale relative to duration of button held down
	private float slideAmount;
	public float minArea;
	public float maxArea;
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = (Animator)GetComponent ("Animator");
		sliderBar = slider.GetComponent<SpriteRenderer> ();
		sliderVector = sliderBar.transform.localScale;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Action")) {
			slideAmount++;
			sliderScale = slideAmount / 100;
			sliderBar.transform.localScale = new Vector3(sliderVector.x * sliderScale, 1, 1);
			Debug.Log("Slide Amount: " + slideAmount);
			if (slideAmount > 100) {
				Debug.Log("TOO HIGH");
				slideAmount = 0;
			}
		}

		if (Input.GetButtonUp ("Action")) {
			if (slideAmount > minArea && slideAmount < maxArea) {
				Debug.Log("Hit! ");
				animator.SetBool("Shoot", true);
			} else {
				slideAmount = 0;
			}
		}
		//levMan.CompleteLevel ();
	}
}
