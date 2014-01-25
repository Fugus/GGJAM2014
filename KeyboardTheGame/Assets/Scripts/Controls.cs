using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

	public static Controls instance;

	public string TopLeft;
	public string TopRight;
	public string Left;
	public string Right;
	public string BottomLeft;
	public string BottomRight;
	public string CurrentTile;

	public static Controls Instance{
		get{
			if(instance == null){
				GameObject g = new GameObject ("Controls");
				instance = g.AddComponent<Controls>();
				DontDestroyOnLoad(g);
			}
			return instance;
		}
	}
}
