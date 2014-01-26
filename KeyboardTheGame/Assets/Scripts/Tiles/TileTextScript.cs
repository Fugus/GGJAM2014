using UnityEngine;
using System.Collections;

public class TileTextScript : TileScript {
	
	// Update is called once per frame
	new void Update () {
		if(hasAppliedTriggerAction)
		{
			hasAppliedTriggerAction = false;
			UIManager_.mainTextString = metadata;
			GameObject.Find ("/Player").GetComponent<PlayerControl>().PlaySound(PlayerControl.RecordableSounds.reading);
		}

        base.Update();
	}

}
