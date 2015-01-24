using UnityEngine;
using System.Collections;
using System.IO;
using System;

public static class Utilities
{
	static GUIStyle levelDisplayStyle;

	/*
	 * Checks passed in Game object to see if any of it's tags are of the requested value
	 */ 
	public static bool hasMatchingTag(Tag tagToCheckFor, GameObject objectToCheck)
	{
		MultiTagScript mult =  objectToCheck.GetComponent<MultiTagScript>();
		if (mult != null) { return mult.objectHasTag(tagToCheckFor);}
		return false;
	}

	/*
	 * This is all just a workaround to make up for my bullshit prefab not working.
	 * 
	 */ 
	public static GUIStyle LevelDisplay(GUIStyle daStyle)
	{
		if (levelDisplayStyle == null)
		{
			levelDisplayStyle = daStyle;
			return daStyle;
		}
		else
		{
			return levelDisplayStyle;
		}
	}
}

