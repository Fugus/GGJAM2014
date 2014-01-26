using UnityEngine;
using System.Collections;

public class TileRScript : TileScript {
	
	// Update is called once per frame
	new void Update () {
		if(hasAppliedTriggerAction)
		{
			hasAppliedTriggerAction = false;
			UIManager_.mainTextString = "<3<3 Together at last! <3<3";
			GameObject.Find ("/Player").GetComponent<PlayerControl>().PlaySound(PlayerControl.RecordableSounds.victory);
		}

		// this tile should never have fog of war
		if(spawnedFOW) RemoveFOW();

        base.Update();
	}

}
