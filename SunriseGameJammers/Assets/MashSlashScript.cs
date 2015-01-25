using UnityEngine;
using System.Collections;

public class MashSlashScript : MonoBehaviour {

	//The animator calls this function at the end of its animation
	void destroySelf () {
		Destroy (gameObject);
	}
}
