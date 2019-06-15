using UnityEngine;
using System.Collections;

public class MovingEnemy : MonoBehaviour {

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
			GetComponent<Rigidbody2D>().velocity = new Vector2 (speed, 0);
			if (GetComponent<Rigidbody2D>().velocity.x != 0) {
				animator.SetBool ("Run", true);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (Utilities.hasMatchingTag(Tag.Player, coll.gameObject)) {
			animator.SetBool("Slash", true);
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			coll.BroadcastMessage("LevelDone");
			coll.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			run = false;
			Animator playerAnimator = (Animator)coll.gameObject.GetComponent ("Animator");
			playerAnimator.SetBool("Dead",true);
			//Destroy(coll.gameObject, 1f);
			GetComponent<AudioSource>().Play();
			levMan.FailLevel();
		}
	}


}
