using UnityEngine;
using System.Collections;

public class MashEnemyMovement : MonoBehaviour {

	public float force;

	// Use this for initialization
	void Start () {
		//Apply force

		//If facing left
		if (transform.position.x > 0) { //omg transform.rotation.y returns a quaternion and not degrees
			GetComponent<Rigidbody2D>().AddForce (new Vector2(-1.0f, 0.0f) * force);
		}
		else {
			GetComponent<Rigidbody2D>().AddForce (Vector2.right * force);
		}
	}

	// Update is called once per frame
	void Update () {

	}

	//When player wins or loses, apply force given towards direction
	void reapplyForce(float f, Vector2 direction) {
		GetComponent<Rigidbody2D>().AddForce (direction * f);
	}
}
