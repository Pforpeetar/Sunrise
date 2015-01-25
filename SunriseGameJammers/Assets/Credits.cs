using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Credits : MonoBehaviour {
	
	Dictionary<string, string[]> credits; //holds credit info
	public Texture2D background; 
	List<string> keys; 
	string[] currNames;
	string currKey;
	int index;
	public GUIStyle creditWordStyle; //font styles for display set in insppector
	public GUIStyle creditWord;
	public GUIStyle creditWord2;
	void Start () {
		credits = new Dictionary<string, string[]>()
		{
			{ "Sunrise Design Team" , new string[]{"Peter Pham", "Omari Straker", "Carl Lee", "Travis Tran"}},
			{ "Programmers" , new string[]{"Carl Lee", "Peter Pham", "Omari Straker"}},
			{ "Art Assets" , new string[]{"Travis Tran", "\"Sword Slash\"-RPG Maker VX"}},
			{ "Music" , new string[]{"'Chanter' && 'Our Story Begins'", "Kevin MacLeod (incompetech.com)", "Licensed under", "Creative Commons: By Attribution 3.0", "http://creativecommons.org/licenses/by/3.0"}},
			{ "Sound Effects" , new string[]{"This game uses these sounds from freesound:", "rough grunt by Reitanna", "( http://www.freesound.org/people/Reitanna/ )", "sword 02 by nextmaking", "( http://www.freesound.org/people/nextmaking )"
					, "death blood splatter by minian89", "http://www.freesound.org/people/minian89"
						, "katana1 by Taira Komori", "( http://www.freesound.org/people/Taira%20Komori"
					, "bow by Hanbaal", "( http://www.freesound.org/people/Hanbaal )"}},
			{ "Additional Thanks" , new string[]{"SJSU Game Dev Club","Global Game Jam 2015"}},
		};
		keys = new List<string>(credits.Keys); //store keys in a seperate array for easier reference
		index = 0; //use index to keep track of which part of credits we are displaying
		currNames = credits[keys[index]];
		StartCoroutine(UpdateDisplay()); 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Action"))
		{
			GoToMainMenu();
		}
	}
	
	void OnGUI()
	{
		/*
		 * Draws the texture and text to screen based on the current value of index
		 * since dictionary stores a string array, loop through array to print full credits per section
		 */ 
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),background);
		GUI.Label(new Rect (Screen.width/2-50,10,100,50),"Credits",creditWord);
		//string currKey = keys[index];
		GUI.Label(new Rect (Screen.width/2-50,60,100,50),currKey,creditWord2);
		//string[] currNames = credits[currKey];
		//currNames = credits[currKey];
		for (int i = 0; i < currNames.Length; i++) {
			GUI.Label(new Rect (Screen.width/2-50,100+i*25,100,50),currNames[i],creditWordStyle);
		}
		GUI.Label(new Rect (Screen.width/2-50,Screen.height - 65,100,50),"Action (space) for Main Menu",creditWord2);
	}
	/**
	 * Since we draw the credit info based on the value of the index counter,
	 * we increment the value of index every few seconds, thus promptng a redraw
	 * delay between updates dependent on length of string array to be drawn
	 */ 
	IEnumerator UpdateDisplay(){
		index = 0;
		while(index < keys.Count) {
			currKey = keys[index];
			currNames = credits[currKey];
			yield return new WaitForSeconds(1.0f + currNames.Length*.85f);
			//yield return new WaitForSeconds(1.0f);
			index++;
		}
		index = keys.Count - 1; //at this point the while loop is over, just show the last credit
		Invoke("GoToMainMenu",1); //after credits finish go back to main menu
	}
	
	void GoToMainMenu()
	{
		Application.LoadLevel(0);
	}
}
