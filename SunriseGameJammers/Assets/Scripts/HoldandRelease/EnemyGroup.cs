using UnityEngine;
using System.Collections;

public class EnemyGroup : MonoBehaviour {
	
	private int numOfChildEnemies;
	private int numofNonChildren;
	public LevelManager levMan;
	// Use this for initialization
	void Start () {
		numOfChildEnemies = 0;
		numofNonChildren = 0;
		foreach (Transform child in transform)
		{
				numOfChildEnemies++;
		}
		numofNonChildren = transform.childCount - numOfChildEnemies;
	}
	
	// Update is called once per frame
	void Update () {
			if (transform.childCount == numofNonChildren)
			{
				levMan.CompleteLevel();
			}
	}
}