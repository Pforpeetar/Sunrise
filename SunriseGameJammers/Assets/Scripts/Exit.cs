using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour
{
		public GameObject mainCamera;
		public LevelManager lManager;
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnTriggerEnter2D (Collider2D collInfo)
		{
				if (Utilities.hasMatchingTag (Tag.Player, collInfo.gameObject)) {
						lManager.CompleteLevel ();
						collInfo.BroadcastMessage("LevelDone");
						collInfo.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
						if (mainCamera.GetComponent<Rigidbody2D>()) {
								mainCamera.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
						}

				}
		}
}
