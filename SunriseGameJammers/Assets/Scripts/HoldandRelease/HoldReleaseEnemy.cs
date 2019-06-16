using UnityEngine;
using System.Collections;

public class HoldReleaseEnemy : MonoBehaviour {
	public Animator animator;
    private Rigidbody2D m_RigidBody;
    private Collider2D m_Collider;
    private AudioSource m_AudioSource;
	public float speed = -2;
	private bool run = true;
	public LevelManager levMan;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        m_RigidBody = GetComponent<Rigidbody2D>(); 
        m_AudioSource = GetComponent<AudioSource>();
        m_Collider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (run) {
			m_RigidBody.velocity = new Vector2 (speed, 0);
            if (m_RigidBody.velocity.x != 0)
            {
				animator.SetBool ("Run", true);
				}
			}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.CompareTag ("Projectile")) {
			Destroy(coll.gameObject);
            m_RigidBody.velocity = new Vector2(0, 0);
            m_Collider.enabled = false;
			run = false;
			m_AudioSource.Play();
			animator.SetBool("Dead", true);
			Destroy (gameObject, 1f);
		}
        if (!run) { return; } //use run as a flag of if the player is dead and level is over 
		if (Utilities.hasMatchingTag(Tag.Player, coll.gameObject)) {
			animator.SetBool("Slash", true);
            GetComponent<AudioSource>().Play();
            Invoke("StopSlashing", .25f);
			m_RigidBody.velocity = new Vector2(0, 0);
			run = false;
			Destroy(coll.gameObject, 1f);
			levMan.FailLevel();
		}
	}

    void StopSlashing()
    {
        animator.SetBool("Slash", false);
        animator.SetBool("Run", false);
    }
}
