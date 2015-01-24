using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour
{

		public LevelManager lManager;
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnTriggerEnter2D (Collision2D collInfo)
		{
				if (Utilities.hasMatchingTag (Tag.Player, collInfo.gameObject)) {
						lManager.CompleteLevel ();
						collInfo.gameObject.rigidbody2D.velocity = new Vector2(0,0);
				}
		}
}
