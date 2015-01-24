using UnityEngine;
using System.Collections;

public class TimedPress : MonoBehaviour {

	public float speed;
	public GameObject cam;
	bool levelDone;
	public LevelManager lManager;
	private Animator animator;
	private bool grounded;
	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = new Vector2(speed,rigidbody2D.velocity.y);
		animator = (Animator)GetComponent ("Animator");
		animator.SetBool("Run", true);
		if (cam != null)
		{
			if (cam.rigidbody2D)
			{
				cam.rigidbody2D.velocity = new Vector2(speed,0);
			}
		}
		levelDone = false;
		grounded = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!levelDone)
		{
			rigidbody2D.velocity = new Vector2(speed,rigidbody2D.velocity.y);
			if (Input.GetButtonDown("Action"))
			{
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,4);
				grounded = false;
				animator.SetBool("Run",false);
				animator.SetBool("Jump",true);
			}
			if (transform.position.x < cam.transform.position.x-5)
			{
				lManager.FailLevel();
			}
		}
		if (grounded)
		{
			animator.SetBool("Run",true);
			animator.SetBool("Jump",false);
		}
	}

	void LevelDone()
	{
		levelDone = true;
	}

	void GroundSelf()
	{
		grounded = true;
	}
}
