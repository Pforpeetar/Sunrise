using UnityEngine;
using System.Collections;

public class TimedPress : MonoBehaviour {

	public float speed;
	public GameObject cam;
	bool levelDone;
	public LevelManager lManager;
	private Animator animator;
    private Rigidbody2D m_RigidBody;
    private Rigidbody2D cam_RigidBody;
    private AudioSource m_AudioSource;

	private bool grounded;
	private float lowPitchRange = 0.75f;
	private float highPitchRange = 1.25f;
    private Vector2 calcVelocity = Vector2.zero;
	public AudioClip jump;
	// Use this for initialization

    void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_AudioSource = GetComponent<AudioSource>();
    }

	void Start () {
        cam_RigidBody = cam.GetComponent<Rigidbody2D>();
		calcVelocity = new Vector2(speed,m_RigidBody.velocity.y);
		animator = (Animator)GetComponent ("Animator");
		animator.SetBool("Run", true);
		if (cam != null)
		{
			if (cam_RigidBody)
			{
                cam_RigidBody.velocity = new Vector2(speed, 0);
			}
		}
		levelDone = false;
		grounded = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!levelDone)
		{
            calcVelocity = new Vector2(speed, m_RigidBody.velocity.y);
			if (Input.GetButtonDown("Action")&& grounded)
			{
                calcVelocity = new Vector2(m_RigidBody.velocity.x, 4);
				grounded = false;
				animator.SetBool("Run",false);
				animator.SetBool("Jump",true);
				m_AudioSource.pitch = Random.Range (lowPitchRange,highPitchRange);
                m_AudioSource.PlayOneShot(jump);
			}
			if (transform.position.x < cam.transform.position.x-5)
			{
				lManager.FailLevel();
                cam_RigidBody.velocity = new Vector2(0, 0);
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
        m_RigidBody.velocity = calcVelocity;
	}

    void LateUpdate()
    {
        m_RigidBody.velocity = calcVelocity;
    }

	void LevelDone()
	{
		levelDone = true;
        calcVelocity = Vector2.zero;
        cam_RigidBody.velocity = new Vector2(0, 0);
	}

	void GroundSelf()
	{
		grounded = true;
	}
}
