using UnityEngine;
using System.Collections;

public class TileTextScript : TileScript {
	
	// Update is called once per frame
	void Update () {
		if(hasAppliedTriggerAction)
		{
			hasAppliedTriggerAction = false;
			UIManager_.mainTextString = metadata;
		}
	}

}
