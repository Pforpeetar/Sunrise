using UnityEngine;
using System.Collections;

public class HoldRelease : MonoBehaviour {
	public LevelManager levMan;
	public float slideSpeed;
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
		//Charges slider bar when action button is held down, activates drawing animation
		if (Input.GetButton ("Action")) {
			animator.SetBool("DrawArrow", true);
			slideAmount+= slideSpeed;
			sliderUpdate();
			//Debug.Log("Slide Amount: " + slideAmount);
			if (slideAmount >= 105) {
				//Debug.Log("TOO HIGH");
				slideAmount = 0;
			}
		}
		//Once arrow/action button is release, trigger shooting animation and fire a projectile
		if (Input.GetButtonUp ("Action")) {
			animator.SetBool("Shoot", true);

			if (slideAmount < minArea) {
				launchProjectile(5f, 0f, 1f);
				slideAmount = 0;
			} 
			else if(slideAmount > maxArea) {
				launchProjectile(10f, 5f, 0f);
				slideAmount = 0;
			}
			else if (slideAmount >= minArea && slideAmount <= maxArea) {
				//Debug.Log("Hit! ");
				animator.SetBool("Shoot", true);
				launchProjectile(10f, 0, 0);
				slideAmount = 0;
				//end = true;
				//Invoke("endLevel", 2);
			} else {
				slideAmount = 0;
				sliderUpdate();
			}
		}
	}

	void sliderUpdate() {
		sliderScale = slideAmount / 100;
		sliderBar.transform.localScale = new Vector3(sliderScale, 1, 1);
		//Debug.Log("Update SliderScale " + sliderBar.transform.localScale);
	}

	void endLevel() {
		//animator.SetBool ("Shoot", false);
		levMan.CompleteLevel();
	}

	//launch projectile takes in a parameter xVel, yVel to calculate x and y velocities. gScale used to apply gravity if need be.
	void launchProjectile(float xVel, float yVel, float gScale) {
		//spawns projectile at location of script's gameobject
		curryInstance = (GameObject)GameObject.Instantiate (curryWeapon, gameObject.transform.position, Quaternion.identity);
		curryInstance.rigidbody2D.velocity = new Vector2(xVel, yVel);
		curryInstance.rigidbody2D.gravityScale = gScale;
		Destroy(curryInstance, 1f);
	}

	void disableShootAnim() {
		animator.SetBool ("Shoot", false);
	}
}
