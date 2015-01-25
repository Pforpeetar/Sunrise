using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BGMPlayer : MonoBehaviour {
	public static BGMPlayer soundManager;
	public AudioClip mainTheme;
	public AudioClip stageMusic;
	private List<string> mainThemeStages = new List<string>(){"Main Menu","Credits"};
	// Use this for initialization
	void Awake()
	{
		if (!soundManager) {
			soundManager = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	void Start () {
	}

	void OnLevelWasLoaded(int level)
	{
		if (audio == null) {return;}
		AudioClip newClip;
		if(mainThemeStages.Contains(Application.loadedLevelName))
		{
			newClip = mainTheme;
		}
		else
		{
			newClip = stageMusic;
		}
		if (audio.clip != newClip)
		{
			audio.clip = newClip;
			audio.Play();
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
