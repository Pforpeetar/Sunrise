using UnityEngine;
using System.Collections;

public class HoldRelease : MonoBehaviour {
	public LevelManager levMan;
	public GameObject curryWeapon;
	private GameObject curryInstance;
	public GameObject slider;
	public GameObject minIndicator;
	public GameObject maxIndicator;
	private SpriteRenderer sliderBar; //slider bar sprite to be used
	private Vector3 sliderVector; //Vector of slider
	private float sliderScale; //Scale relative to duration of button held down
	private float slideAmount;
	public float minArea;
	public float maxArea;
	private Animator animator;
	private bool end = false;

	// Use this for initialization
	void Start () {
		animator = (Animator)GetComponent ("Animator");
		sliderBar = slider.GetComponent<SpriteRenderer> ();
		sliderVector = sliderBar.transform.localScale;
		minIndicator.transform.position = new Vector3 (5 * minArea / 100, slider.transform.position.y);
		maxIndicator.transform.position = new Vector3 (5 * maxArea / 100, slider.transform.position.y);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Action") && !end) {
			slideAmount+= 5;
			sliderUpdate();
			//Debug.Log("Slide Amount: " + slideAmount);
			if (slideAmount > 100) {
				Debug.Log("TOO HIGH");
				slideAmount = 0;
			}
		}

		if (Input.GetButtonUp ("Action") && !end) {
			if (slideAmount > minArea && slideAmount < maxArea) {
				Debug.Log("Hit! ");
				animator.SetBool("Shoot", true);
				curryInstance = (GameObject)GameObject.Instantiate (curryWeapon, gameObject.transform.position, Quaternion.identity);
				curryInstance.rigidbody2D.velocity = new Vector2(10f, 0);
				end = true;
				Invoke("endLevel", 2);
			} else {
				slideAmount = 0;
				sliderUpdate();
				animator.SetBool("Shoot", false);
				levMan.FailLevel();
			}
		}
		//levMan.CompleteLevel ();
	}

	void sliderUpdate() {
		sliderScale = slideAmount / 100;
		sliderBar.transform.localScale = new Vector3(sliderScale, 1, 1);
		Debug.Log("Update SliderScale " + sliderBar.transform.localScale);
	}

	void endLevel() {
		animator.SetBool ("Shoot", false);
		levMan.CompleteLevel();
	}
}
