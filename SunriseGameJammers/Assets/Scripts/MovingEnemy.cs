using UnityEngine;
using System.Collections;

public class MovingEnemy : MonoBehaviour {

	public Animator animator;
	public float speed = -2;
	private bool run = true;
	public LevelManager levMan;
    private Rigidbody2D m_RigidBody;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        m_RigidBody = GetComponent<Rigidbody2D>();
	}
	
    void LateUpdate()
    {
        if (run)
        {
            m_RigidBody.velocity = new Vector2(speed, 0);
            if (m_RigidBody.velocity.x != 0)
            {
                animator.SetBool("Run", true);
            }
        }
    }

	void OnTriggerEnter2D(Collider2D coll) {
		if (Utilities.hasMatchingTag(Tag.Player, coll.gameObject)) {
			animator.SetBool("Slash", true);
			m_RigidBody.velocity = new Vector2(0, 0);
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
