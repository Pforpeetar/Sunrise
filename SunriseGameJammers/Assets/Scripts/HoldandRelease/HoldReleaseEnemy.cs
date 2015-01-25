using UnityEngine;
using System.Collections;

public class HoldReleaseEnemy : MonoBehaviour {
	public Animator animator;
	public float speed = -2;
	private bool run = true;
	public LevelManager levMan;
	// Use this for initialization
	void Start () {
		animator = (Animator)GetComponent("Animator");
	}
	
	// Update is called once per frame
	void Update () {
		if (run) {
			rigidbody2D.velocity = new Vector2 (speed, 0);
			if (rigidbody2D.velocity.x != 0) {
				animator.SetBool ("Run", true);
				}
			}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.CompareTag ("Projectile")) {
			Destroy(coll.gameObject);
			rigidbody2D.velocity = new Vector2(0, 0);
			gameObject.collider2D.enabled = false;
			run = false;
			audio.Play();
			Destroy (gameObject, 1f);
		} 
		if (Utilities.hasMatchingTag(Tag.Player, coll.gameObject)) {
			animator.SetBool("Slash", true);
			rigidbody2D.velocity = new Vector2(0, 0);
			run = false;
			Destroy(coll.gameObject, 1f);
			levMan.FailLevel();
		}
	}
}
