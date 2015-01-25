using UnityEngine;
using System.Collections;

public class MovingEnemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	rigidbody2D.velocity = new Vector2(-2,0);
	}
}
