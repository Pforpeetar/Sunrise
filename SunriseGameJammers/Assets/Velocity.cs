using UnityEngine;
using System.Collections;

public class Velocity : MonoBehaviour {

	public float velocity;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	rigidbody2D.velocity = new Vector2(velocity,0);
	}
}
