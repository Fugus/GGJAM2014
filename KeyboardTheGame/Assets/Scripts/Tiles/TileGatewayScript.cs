﻿using UnityEngine;
using System.Collections;

public class TileGatewayScript : TileScript {

	new void Start () {
		base.Start();
		LevelManager_ = GameObject.Find("/Managers").GetComponent<LevelManager>();
		TileManager_ = GameObject.Find("/Managers").GetComponent<TileManager>();
	}

	// Update is called once per frame
	new void Update () {
		if(hasAppliedTriggerAction)
		{
			hasAppliedTriggerAction = false;
			UIManager_.mainTextString = "";
			if(LevelManager_.levels.ContainsKey(metadata) && TileManager_.currentLevel != metadata) {
				// trash current tiles
				TileManager_.LoadLevel(metadata);
				// show the nice name
				UIManager_.mainTextString = LevelManager_.levels[metadata].nice_name;
				// play sound!
				GameObject.Find ("/Player").GetComponent<PlayerControl>().PlaySound(PlayerControl.RecordableSounds.teleporting);
			}
		}

        base.Update();
	}

}
