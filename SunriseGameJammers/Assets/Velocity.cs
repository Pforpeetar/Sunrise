using UnityEngine;
using System.Collections;

public class Velocity : MonoBehaviour {

	public float velocity;
	private Animator animator;
	private bool shouldStop;
	// Use this for initialization
	void Start () {
	shouldStop = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!shouldStop) {
	GetComponent<Rigidbody2D>().velocity = new Vector2(velocity,0);
		animator = (Animator)GetComponent ("Animator");
		if (GetComponent<Rigidbody2D>().velocity.x > 0) {
				animator.SetBool("Run", true);
			} else if (GetComponent<Rigidbody2D>().velocity.x < 0) {
				animator.SetBool("eRun", true);
			}
		}
	}

	void Stop()
	{
		shouldStop = true;
		GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
		animator.SetBool("Run", false);
		animator.SetBool("eRun", false);
	}
}
