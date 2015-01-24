using UnityEngine;
using System.Collections;
using System.IO;
using System;

public static class Utilities
{

	/*
	 * Checks passed in Game object to see if any of it's tags are of the requested value
	 */ 
	public static bool hasMatchingTag(Tag tagToCheckFor, GameObject objectToCheck)
	{
		MultiTagScript mult =  objectToCheck.GetComponent<MultiTagScript>();
		if (mult != null) { return mult.objectHasTag(tagToCheckFor);}
		return false;
	}

}

