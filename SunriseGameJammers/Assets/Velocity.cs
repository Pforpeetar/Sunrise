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
	rigidbody2D.velocity = new Vector2(velocity,0);
		animator = (Animator)GetComponent ("Animator");
		animator.SetBool("Run", true);
		}
	}

	void Stop()
	{
		shouldStop = true;
		rigidbody2D.velocity = new Vector2(0,0);
		animator.SetBool("Run", false);

	}
}
