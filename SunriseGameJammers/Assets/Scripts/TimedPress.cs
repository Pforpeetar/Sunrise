using UnityEngine;
using System.Collections;

public class TimedPress : MonoBehaviour {

	public float speed;
	public GameObject cam;
	bool levelDone;
	public LevelManager lManager;
	private Animator animator;
	private bool grounded;
	private float lowPitchRange = 0.75f;
	private float highPitchRange = 1.25f;
	public AudioClip jump;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().velocity = new Vector2(speed,GetComponent<Rigidbody2D>().velocity.y);
		animator = (Animator)GetComponent ("Animator");
		animator.SetBool("Run", true);
		if (cam != null)
		{
			if (cam.GetComponent<Rigidbody2D>())
			{
				cam.GetComponent<Rigidbody2D>().velocity = new Vector2(speed,0);
			}
		}
		levelDone = false;
		grounded = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!levelDone)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(speed,GetComponent<Rigidbody2D>().velocity.y);
			if (Input.GetButtonDown("Action")&& grounded)
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,4);
				grounded = false;
				animator.SetBool("Run",false);
				animator.SetBool("Jump",true);
				GetComponent<AudioSource>().pitch = Random.Range (lowPitchRange,highPitchRange);
				GetComponent<AudioSource>().PlayOneShot(jump);
			}
			if (transform.position.x < cam.transform.position.x-5)
			{
				lManager.FailLevel();
				cam.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			}
		}
		if (grounded)
		{
			animator.SetBool("Run",true);
			animator.SetBool("Jump",false);
		}
		if (transform.position.y < -20)
		{
			lManager.FailLevel();
			LevelDone();
		}
	}

	void LevelDone()
	{
		levelDone = true;
		cam.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
	}

	void GroundSelf()
	{
		grounded = true;
	}
}
