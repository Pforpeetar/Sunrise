using UnityEngine;
using System.Collections;

public class Velocity : MonoBehaviour {

	public float velocity;
	private Animator animator;
	private bool shouldStop;
    private Rigidbody2D m_RigidBody;
    
	// Use this for initialization
	void Start () {
	shouldStop = false;
         animator = GetComponent<Animator>();
         m_RigidBody = GetComponent<Rigidbody2D>();
	}

    void LateUpdate()
    {
        if (!shouldStop)
        {
            m_RigidBody.velocity = new Vector2(velocity, 0);
            if (GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                animator.SetBool("Run", true);
            }
            else if (GetComponent<Rigidbody2D>().velocity.x < 0)
            {
                animator.SetBool("eRun", true);
            }
        }
    }

	void Stop()
	{
		shouldStop = true;
		m_RigidBody.velocity = new Vector2(0,0);
		animator.SetBool("Run", false);
		animator.SetBool("eRun", false);
	}
}
