using UnityEngine;
using System.Collections;

public class TileLabyrinthScript : TileScript {

	new void Start () {
		base.Start();
		// set arrow direction to metadata degrees or random by default
		if(metadata.Length > 0) transform.Rotate(new Vector3(0,0,int.Parse(metadata)));
	}

	// Update is called once per frame
	new void Update () {
		if(hasAppliedTriggerAction)
		{
			hasAppliedTriggerAction = false;
			UIManager_.mainTextString = "";
			// set arrow direction to random other position
			transform.Rotate(new Vector3(0,0,Mathf.Round(Random.Range(60, 300)/60)*60));
			GameObject.Find ("/Player").GetComponent<PlayerControl>().PlaySound(PlayerControl.RecordableSounds.confused);
		}

        base.Update();
	}

}
