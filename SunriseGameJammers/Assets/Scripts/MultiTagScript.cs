using UnityEngine;
using System.Collections;

public enum Tag{Player,Enemy,Exit};
public class MultiTagScript : MonoBehaviour
{

	public Tag[] objectTags;

	public bool objectHasTag (Tag tagToCheckFor)
	{
			if ((objectTags.Length == 0)) {
					return false;
			}
			foreach (Tag curTag in objectTags) {
					if (curTag.Equals (tagToCheckFor)) {
							return true;
					}
			}
			return false;
	}
}
