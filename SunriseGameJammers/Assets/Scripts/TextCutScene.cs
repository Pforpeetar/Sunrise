using UnityEngine;
using System.Collections;
using System;

public class TextCutScene : MonoBehaviour 
{
	public Texture2D BackGroundImage;
	private string currText;
	public Texture2D dialogTexture;
	public float textScrollSpeed;
	private bool talking;
	private bool textIsScrolling;
	private int currentLine;
	public string[] dialogueLines;
	//int randomImageIndex;
	string displayText;
	public GUIStyle oppaFontStyle = new GUIStyle();
	private Texture2D textureToDraw;
	void Start ()
	{
		StartCutScene(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		/* switched order of statements so that check to see if the line is finished comes before 
		 * check to see if text is scrolling, makes code work so oh well
		 */
		if (talking) {
			if (Input.GetButtonDown ("Action")) {
				if (currText.Equals (dialogueLines[currentLine])) {
					//display next line
					textIsScrolling = false;
					if (currentLine < dialogueLines.Length - 1) { //player not on last line yet
						currentLine++; 
						StartCoroutine (startScrolling ());
					} else {  //player just finished reading the last line
						currentLine = 0;
						currText = "";
						updateNPC (false); //sets all the booleans to display
						Invoke("NextLevel",1f);
					}
				} else if (textIsScrolling) {
					//display full line
					currText = dialogueLines [currentLine];
					textIsScrolling = false;
				}
			}
			//auto skip to the last line if start is pressed
			if(Input.GetKeyDown("p")) {
				currentLine = dialogueLines.Length - 1;
				currText = dialogueLines[currentLine];
				textIsScrolling = false;
				updateNPC(false);
				Invoke("NextLevel",.5f);
			}
		}
	}
	
	IEnumerator startScrolling ()
	{
		textIsScrolling = true;
		int startLine = currentLine;
		displayText = "";
		for (int i = 0; i < dialogueLines[currentLine].Length; i++) {
			if (textIsScrolling && currentLine == startLine) {
				displayText += dialogueLines [currentLine] [i];
				currText = displayText;
				yield return new WaitForSeconds (1 / textScrollSpeed);
			} else {
				yield return true;
			}
		}
	}
	
	public void updateNPC (bool isNPCactive)
	{
		talking = isNPCactive;
	}
	
	public void StartCutScene (GameObject player)
	{
		//ensures text and pic elements are in the right position
		if (dialogueLines.Length == 0) {return;}
		//talkTextGUI.transform.position = new Vector3 (0, -.12f, talkTextGUI.transform.position.z);
		//textBoxTexture.transform.position = new Vector3 (0.3198967f, 0.07225594f, textBoxTexture.transform.position.z);
		//transform.parent.transform.position = new Vector3 (0, 0, -10);
		updateNPC (true);
		currentLine = 0;
		//randomImageIndex = Random.Range(0,npcImages.Length);
		StartCoroutine (startScrolling ());
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),BackGroundImage);
		if (talking) //scale image to screen size
		{
			GUI.DrawTexture(new Rect(-Screen.width/2.5f,Screen.height/1.15f,Screen.width*1.5f,(Screen.height/12)+40),dialogTexture);
			GUI.Label(new Rect(30,Screen.height/1.11f,Screen.width*.85f,(Screen.height/12)+40),currText,oppaFontStyle);
		}
	}

	
	void NextLevel()
	{
			Application.LoadLevel(Application.loadedLevel+1);
	}
}
