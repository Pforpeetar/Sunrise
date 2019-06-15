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
	public float minArea;
	public float maxArea;
	public float areaOffset;
	private SpriteRenderer sliderBar; //slider bar sprite to be used
	private Vector3 sliderVector; //Vector of slider
	private float sliderScale; //Scale relative to duration of button held down
	private float slideAmount;

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
			if (slideAmount >= 105) {
				slideAmount = 0;
			}
		}
		//Once arrow/action button is release, trigger shooting animation and fire a projectile
		if (Input.GetButtonUp ("Action")) {
			animator.SetBool("Shoot", true);
			GetComponent<AudioSource>().Play();
			if (slideAmount < minArea - areaOffset) {
				launchProjectile(5f, -1f, 2f);
				slideAmount = 0;
			} 
			else if(slideAmount > maxArea + areaOffset) {
				launchProjectile(10f, 5f, 0.5f);
				slideAmount = 0;
			}
			else if (slideAmount >= minArea - areaOffset && slideAmount <= maxArea + areaOffset) {
				//Debug.Log("Hit! ");
				//animator.SetBool("Shoot", true);
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
		curryInstance = (GameObject)GameObject.Instantiate (curryWeapon, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f), Quaternion.identity);
		curryInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
		curryInstance.GetComponent<Rigidbody2D>().gravityScale = gScale;
		Destroy(curryInstance, 2f);
	}

	void disableShootAnim() {
		animator.SetBool ("Shoot", false);
	}
}
