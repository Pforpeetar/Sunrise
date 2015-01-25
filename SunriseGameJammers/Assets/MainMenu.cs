using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Texture2D background;
	public GUIStyle gameNameFont;
	public GUIStyle pressPlayPrompt;
	private bool drawTheThing;
	private bool drawTheThing2;
	// Use this for initialization
	void Start () {
		drawTheThing = true; //draw the "Press Start" prompt by default
		drawTheThing2 = true; //same with credits prompt
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Action"))
		{
			audio.Play();
			Invoke("NextLevel",1);
			InvokeRepeating("TryFlickerEffect",0,.075f);
		}
		if (Input.GetButtonDown("Credits"))
		{
			audio.Play();
			Invoke("GoToCredits",1);
			InvokeRepeating("TryFlickerEffect2",0,.075f);

		}
	}

	void TryFlickerEffect()
	{
		drawTheThing = !drawTheThing; //flip the boolean to control whether or not text is being drawn on the screen
	}
	void TryFlickerEffect2()
	{
		drawTheThing2 = !drawTheThing2; //flip the boolean to control whether or not text is being drawn on the screen
	}

	void NextLevel()
	{
		Application.LoadLevel(Application.loadedLevel+1);
	}

	void GoToCredits()
	{
		Application.LoadLevel("Credits");
	}
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), background);
		GUI.Label(new Rect(Screen.width/4,Screen.height/30,Screen.width/2,Screen.height/3),"SAMURAI 13",gameNameFont);
		if (drawTheThing)
		{
		//By alternating the boolean, message will dissapear and reappear thus looking like it's flickering.
		GUI.Label(new Rect(Screen.width/4,3.5f*Screen.height/5,Screen.width/2,Screen.height/7),"Press Space to Play",pressPlayPrompt);
		}
		if (drawTheThing2)
		{
			GUI.Label(new Rect(Screen.width/4,4*Screen.height/5,Screen.width/2,Screen.height/7),"Press C for Credits",pressPlayPrompt);

		}
}
}