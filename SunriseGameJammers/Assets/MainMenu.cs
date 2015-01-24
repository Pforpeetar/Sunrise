using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Texture2D background;
	public GUIStyle gameNameFont;
	public GUIStyle pressPlayPrompt;
	private bool drawTheThing;
	// Use this for initialization
	void Start () {
		drawTheThing = true; //draw the "Press Start" prompt by default
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Action"))
		{
			Invoke("NextLevel",1);
			InvokeRepeating("TryFlickerEffect",0,.075f);
		}
	}

	void TryFlickerEffect()
	{
		drawTheThing = !drawTheThing; //flip the boolean to control whether or not text is being drawn on the screen
	}
	void NextLevel()
	{
		Application.LoadLevel(Application.loadedLevel+1);
	}
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), background);
		GUI.Label(new Rect(Screen.width/4,Screen.height/30,Screen.width/2,Screen.height/3),"SUNRISE",gameNameFont);
		if (drawTheThing)
		{
		//By alternating the boolean, message will dissapear and reappear thus looking like it's flickering.
		GUI.Label(new Rect(Screen.width/4,4*Screen.height/5,Screen.width/2,Screen.height/7),"Press Space to Play",pressPlayPrompt);
	
		}
}
}